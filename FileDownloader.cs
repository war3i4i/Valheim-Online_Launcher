using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Valheim_Online_Launcher;

public static class FileDownloader
{
    private static HashSet<string> ExcludeFiles = new()
    {
        "update.info", "HashCreator.exe", "full.info", "version.info", "news.info", "Valheim_Online_Launcher.exe", "Valheim Online Launcher.exe", "LogOutput.log"
    };

    private static bool CanStartGame;
    private static string CurrentFolder;
    private static bool FullCheck;
    private static bool StartAfter;

    private static readonly HashSet<string> ExcludedFolderFromNonFull = ["valheim_Data", "MonoBleedingEdge"];

    public static void Start_Update(BackgroundWorker worker, bool full, bool startGame)
    {
        ExcludeFiles.Add(Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        CurrentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        CanStartGame = true;
        FullCheck = full;
        StartAfter = startGame;

        try
        {
            using WebClient client = new WebClient();
            client.DownloadFile(new Uri("https://valheim-online.com/launcher/update.info"), "update.info");
        }
        catch (Exception ex)
        {
            if (File.Exists("update.info"))
                File.Delete("update.info");

            Process.Start("valheim.exe");
            MainWindow.Instance.Button_Close(null, null);
            return;
        }

        MainWindow.Instance.progressbar.Visibility = Visibility.Visible;
        MainWindow.Instance.totalpercent.Visibility = Visibility.Visible;
        MainWindow.Instance.currenttask.Visibility = Visibility.Visible;

        foreach (var dir in ExcludedFolderFromNonFull)
        {
            if (!Directory.Exists(Path.Combine(CurrentFolder, dir)))
            {
                FullCheck = true;
                break;
            }
        }

        worker.DoWork += Work;
        worker.RunWorkerAsync();
    }

    private struct FileToDownload
    {
        public string WebDir;
        public string LocalDir;
        public int FileSize;
    }

    private static void Work(object sender, DoWorkEventArgs e)
    {
        try
        {
            FileStream clientInfo = new FileStream("update.info", FileMode.Open);
            BinaryReader binRead = new BinaryReader(clientInfo);
            int totalFilesizeToDownload = 0;
            HashSet<string> allFiles = [];
            List<FileToDownload> filesToDownload = [];
            int totalFiles = binRead.ReadInt32();
            SetStateLabel("Подготовка...");
            for (int i = 0; i < totalFiles; ++i)
            {
                if (e.Cancel) return;
                string fileDir = binRead.ReadString();
                string fileHash = binRead.ReadString();
                int fileSize = binRead.ReadInt32();

                allFiles.Add(fileDir);
                string fileDirFull = Path.Combine(CurrentFolder, fileDir);
                if (!Directory.Exists(Path.GetDirectoryName(fileDirFull)))
                    Directory.CreateDirectory(Path.GetDirectoryName(fileDirFull));

                if (!FullCheck && ExcludedFolderFromNonFull.Contains(fileDir.Split('/')[0])) continue;

                try
                {
                    SetStateLabel("Проверка " + fileDir);
                    string hash = null;
                    if (File.Exists(fileDirFull))
                    {
                        byte[] fs = File.ReadAllBytes(fileDirFull);
                        hash = MD5(fs);
                    }

                    if (hash != fileHash)
                    {
                        CanStartGame = false;
                        totalFilesizeToDownload += fileSize;
                        filesToDownload.Add(new FileToDownload()
                        {
                            WebDir = fileDir,
                            LocalDir = fileDirFull,
                            FileSize = fileSize
                        });
                    }
                }
                catch
                {
                }

                SetTotalPercent((double)i / totalFiles * 100);
            }
            binRead.Close();
            clientInfo.Close();
            File.Delete("update.info");
            /*
            SetStateLabel("Удаление лишних файлов");
            var en = System.IO.Directory.GetFiles(CurrentFolder, "*.*", System.IO.SearchOption.AllDirectories);
            List<string> files = en.Where(f => !ExcludeFiles.Contains(Path.GetFileName(f))).ToList();
            files.Sort((a, b) => b.Split('\\').Length.CompareTo(a.Split('\\').Length));
            for (int i = 0; i < files.Count; ++i)
            {
                if (e.Cancel) return; 
                string file = files[i];
                string fileDir = file.Replace(CurrentFolder, "").Replace("\\", "/").Trim('/');
                if (allFiles.Contains(fileDir)) continue;
                if (!FullCheck && ExcludedFolderFromNonFull.Contains(fileDir.Split('/')[0])) continue;
                try
                {
                    SetStateLabel("Удаление " + fileDir);
                    File.Delete(file);

                    string directory = Path.GetDirectoryName(file);
                    if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                        Directory.Delete(directory);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error"); 
                }

                SetTotalPercent((double)i / files.Count * 100);
            }
            */
            
            SetTotalPercent(100);
            if (CanStartGame)
            {
                OnComplete();
                return;
            }

            for (int i = 0; i < filesToDownload.Count; ++i)
            {
                if (e.Cancel) return;
                SetStateLabel("Скачивание " + filesToDownload[i].WebDir);
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        double percentage = (double)e.BytesReceived / e.TotalBytesToReceive * 100;
                        string msg = $"{((int)e.BytesReceived).SizeSuffix()} / {((int)e.TotalBytesToReceive).SizeSuffix()}";
                        SetTotalPercent_Text(percentage, msg);
                    };
                    client.DownloadFileCompleted += HandleDownloadComplete;
                    var syncObject = new object();
                    lock (syncObject)
                    {
                        client.DownloadFileAsync(new Uri("https://valheim-online.com/launcher/" + filesToDownload[i].WebDir), filesToDownload[i].LocalDir, syncObject);
                        Monitor.Wait(syncObject);
                    }
                }
            }

            OnComplete();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }
    }

    private static void OnComplete()
    {
        if (StartAfter)
        {
            HideControls();
            MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Button_Close(null, null));
            Process.Start("valheim.exe");
        }
        else
        {
            HideControls();
            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                MainWindow.Instance.StartGameBtn.IsEnabled = true;
                MainWindow.Instance.FullCheck.IsEnabled = true;
                var _startbuttonOA = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromMilliseconds(800d))
                };
                MainWindow.Instance.StartButtonGrid.BeginAnimation(UIElement.OpacityProperty, _startbuttonOA);
            });
        }
    }

    private static void HandleDownloadComplete(object sender, AsyncCompletedEventArgs e)
    {
        lock (e.UserState)
        {
            Monitor.Pulse(e.UserState);
        }
    }

    private static void SetStateLabel(string msg) => MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.currenttask.Text = msg);

    private static void SetTotalPercent(double percent)
    {
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.totalpercent.Content = (int)percent + "%");
        //MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.progressbar.Width = Clamp(percent, 0, 100) * 5.87);
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.progressbar.Value = Clamp(percent, 0, 100) * (MainWindow.Instance.progressbar.Maximum / 100.0));
    }

    private static void SetTotalPercent_Text(double percent, string msg)
    {
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.totalpercent.Content = msg);
        //MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.progressbar.Width = Clamp(percent, 0, 100) * 5.87);
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.progressbar.Value = Clamp(percent, 0, 100) * (MainWindow.Instance.progressbar.Maximum / 100.0));
    }

    private static void HideControls()
    {
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.progressbar.Visibility = Visibility.Hidden);
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.totalpercent.Visibility = Visibility.Hidden);
        MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.currenttask.Visibility = Visibility.Hidden);
    }

    private static double Clamp(double value, double min, double max) => value < min ? min : value > max ? max : value;

    private static string MD5(byte[] file) => BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(file)).Replace("-", "").ToLower();

    public static string SizeSuffix(this int value, int decimalPlaces = 1)
    {
        switch (value)
        {
            case < 0:
                return "-" + SizeSuffix(-value, decimalPlaces);
            case 0:
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
        }

        int mag = (int)Math.Log(value, 1024);
        decimal adjustedSize = (decimal)value / (1L << (mag * 10));
        if (Math.Round(adjustedSize, decimalPlaces) < 1000)
            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        mag += 1;
        adjustedSize /= 1024;
        return string.Format("{0:n" + decimalPlaces + "} {1}",
            adjustedSize,
            SizeSuffixes[mag]);
    }

    private static readonly string[] SizeSuffixes =
        { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
}