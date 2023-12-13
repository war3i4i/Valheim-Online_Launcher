using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HashCreator
{
    internal class Program
    {
        private static HashSet<string> ExcludeFiles = new()
        {
            "update.info", "HashCreator.exe", "full.info", "version.info", "news.info"
        };
        
        
        public static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            var en = Directory.GetFiles(currentDir, "*.*", SearchOption.AllDirectories);
            List<string> files = en.Where(f => !ExcludeFiles.Contains(Path.GetFileName(f))).ToList();
            files.Sort((a, b) => a.Split('\\').Length.CompareTo(b.Split('\\').Length));
            using (FileStream fs = new FileStream("update.info", FileMode.Create))
            {
                using (BinaryWriter binWrite = new BinaryWriter(fs))
                {
                    binWrite.Write(files.Count);
                    foreach (string file in files)
                    {
                        string hash = MD5(File.ReadAllBytes(file));
                        string path = file.Replace(currentDir, "").Replace('\\', '/').Trim('/');
                        binWrite.Write(path);
                        binWrite.Write(hash);
                        binWrite.Write(File.ReadAllBytes(file).Length);
                        Console.WriteLine(path + " " + hash);
                    }
                }
            }
        }
        
        
        private static string MD5(byte[] file) => BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(file)).Replace("-", "").ToLower();
    }
}