using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Valheim_Online_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static MainWindow Instance;
        private readonly DoubleAnimation _mainOA;
        public static Mutex GLOBAL_MUTEX;
        
        public const int VERSION = 1;

        private static BackgroundWorker _worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            GLOBAL_MUTEX = new Mutex(false, "KG_LAUNCHER_Valheim-Online", out bool firstInstance);
            if (!firstInstance)
            {
                MessageBox.Show("Лаунчер уже запущен", "Launcher"); 
                this.Close();
            } 
            _mainOA = new DoubleAnimation();   
            _mainOA.From = 0;  
            _mainOA.To = 1;
            _mainOA.Duration = new Duration(TimeSpan.FromMilliseconds(800d));    
            BeginAnimation(OpacityProperty, _mainOA);
            LostFocus += (_,__) => isDragging = false;
            Default();
        }

        private void Default()
        {
            string updateInfoFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "update.info");
            if (File.Exists(updateInfoFile))
                File.Delete(updateInfoFile);
            
            progressbar.Visibility = Visibility.Hidden;
            totalpercent.Visibility = Visibility.Hidden;
            currenttask.Visibility = Visibility.Hidden;
            NewsGrid.Visibility = Visibility.Hidden;
            Task.Run(() =>
            {
                using WebClient getUpdates = new WebClient();
                string news = getUpdates.DownloadString("https://valheim-online.com/launcher/news.info");
                Dispatcher.Invoke(() =>
                {
                    News.Text = news;
                    NewsGrid.Visibility = Visibility.Visible;
                    NewsGrid.Opacity = 0;
                    DoubleAnimation newsOA = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(TimeSpan.FromMilliseconds(800d))
                    };
                    NewsGrid.BeginAnimation(OpacityProperty, newsOA);
                });
            });

            Task.Run(() =>
            {
                try
                {
                    using WebClient getVersion = new WebClient();
                    string version = getVersion.DownloadString("https://valheim-online.com/launcher/version.info");
                    Dispatcher.Invoke(() =>
                    {
                        if (int.Parse(version) != VERSION)
                        {
                            MessageBox.Show("Новая версия лаунчера доступна. Пожалуйста скачайте лаунчер по ссылке: https://valheim-online.com/launcher/", "Launcher");
                            Environment.Exit(0);
                        }
                    });
                }
                catch
                {
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Ошибка версии лаунчера", "Launcher");
                        Environment.Exit(0);
                    });
                }
            });

        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            StartGameBtn.IsEnabled = false;
            FullCheck.IsEnabled = false;
            var _startbuttonOA = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(800d))
            };
            _startbuttonOA.Completed += (_, __) => StartButtonAction_OA(false, true);
            StartButtonGrid.BeginAnimation(OpacityProperty, _startbuttonOA);
        }
        
        private void StartGameBtn_Click_Full(object sender, RoutedEventArgs e)
        {
            StartGameBtn.IsEnabled = false;
            FullCheck.IsEnabled = false;
            var _startbuttonOA = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(800d))
            };
            _startbuttonOA.Completed += (_, __) => StartButtonAction_OA(true, false);
            StartButtonGrid.BeginAnimation(OpacityProperty, _startbuttonOA);
        }

        private void StartButtonAction_OA(bool full, bool start)
        { 
            if (_worker is { IsBusy: true }) return;
            _worker?.Dispose();
            _worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            FileDownloader.Start_Update(_worker, full, start);
        }

        public void Button_Close(object sender, MouseButtonEventArgs e)
        {
            if (_worker != null)
            {
                _worker.CancelAsync();
                _worker.Dispose();
            }
          
            _mainOA.From = 1;
            _mainOA.To = 0; 
            _mainOA.Completed += (send, ea) => { Environment.Exit(0); }; 
            _mainOA.Duration = new Duration(TimeSpan.FromMilliseconds(800d)); BeginAnimation(OpacityProperty, _mainOA);
            
        }
        
        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
         
        private bool isDragging = false;
        private Point startPoint;
        

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton != MouseButton.Left) return;
            isDragging = true;
            startPoint = e.GetPosition(this);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)  
            {
                Point mousePos = e.GetPosition(this);
                double newLeft = this.Left + mousePos.X - startPoint.X; 
                double newTop = this.Top + mousePos.Y - startPoint.Y;
                Rect workingArea = SystemParameters.WorkArea;
                newLeft = Math.Max(workingArea.Left, Math.Min(workingArea.Right - this.Width, newLeft));
                newTop = Math.Max(workingArea.Top, Math.Min(workingArea.Bottom - this.Height, newTop));

                this.Left = newLeft;
                this.Top = newTop;
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void DiscordButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://valheim-online.com/discord");
        }

        private void SiteButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://valheim-online.com");
        }
    }
}