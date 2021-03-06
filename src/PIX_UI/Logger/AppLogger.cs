﻿using System;
using System.IO;

namespace PIX_UI.Logger
{
    public class AppLogger
    {
        private static StreamWriter Writer_Main;
        private static StreamWriter Writer_Error;
        private static StreamWriter Writer_Asset;
        private static long MaxFileSize = 800000;
        public bool UseConsole { get; set; } = false;
        public bool BackupFullLogs { get; set; } = true;

        public AppLogger(bool _UseConsole = false, bool _BackupFullLogs = true)
        {
            UseConsole = _UseConsole;
            BackupFullLogs = _BackupFullLogs;
            string LogDirectory = Path.Combine(Environment.CurrentDirectory, "PIX_UI_LOGS");
            string LogBackupDirectory = Path.Combine(Environment.CurrentDirectory, "PIX_UI_LOGS", "BACKUPS");
            string MainLogFile = Path.Combine(LogDirectory, "main_log.txt");
            string ErrorLogFile = Path.Combine(LogDirectory, "error_log.txt");
            string AssetLogFile = Path.Combine(LogDirectory, "asset_log.txt");
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
            if (!Directory.Exists(LogBackupDirectory))
            {
                Directory.CreateDirectory(LogBackupDirectory);
            }
            ClearLogFileIfNeeded(MainLogFile, LogBackupDirectory);
            ClearLogFileIfNeeded(ErrorLogFile, LogBackupDirectory);
            ClearLogFileIfNeeded(AssetLogFile, LogBackupDirectory);
            Writer_Main = new StreamWriter(MainLogFile, true);
            Writer_Error = new StreamWriter(ErrorLogFile, true);
            Writer_Asset = new StreamWriter(AssetLogFile, true);
            Print("---------- APP STARTED ----------");
        }

        public void LoggerDispose()
        {
            Print("---------------------------------------");
            Print("");
            Print("");
            Writer_Main.Dispose();
            Writer_Error.Dispose();
            Writer_Asset.Dispose();
        }

        public void Print(string text, LoggerType TYPE = LoggerType.MAIN)
        {
            DateTime dateTime = DateTime.Now;
            if (TYPE == LoggerType.MAIN)
            {
                if (UseConsole)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("MAIN Log: " + text);
                }
                Writer_Main.WriteLine("[" + dateTime + "]MAIN Log: " + text);
                Writer_Main.Flush();
            }
            else if (TYPE == LoggerType.ASSET)
            {
                if (UseConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("ASSET Log: " + text);
                }
                Writer_Asset.WriteLine("[" + dateTime + "]ASSET Log: " + text);
                Writer_Asset.Flush();
            }
            else
            {
                if (UseConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR Log: " + text);
                }
                Writer_Error.WriteLine("[" + dateTime + "]ERROR Log: " + text);
                Writer_Error.Flush();
            }
            if (UseConsole)
            {
                Console.ResetColor();
            }
        }

        private void ClearLogFileIfNeeded(string file, string backUpDir)
        {
            if (File.Exists(file))
            {
                if (GetFileSize(file) >= MaxFileSize)
                {
                    if (BackupFullLogs)
                    {
                        int directorySize = Directory.GetFiles(backUpDir).Length;
                        string backUpStartName = (file.Contains("main")) ? "main" : (file.Contains("error")) ? "error" : "asset";
                        File.Copy(file, Path.Combine(backUpDir, backUpStartName + "_BACKUP_" + directorySize + ".txt"));
                    }
                    File.Delete(file);
                    Console.WriteLine("Main Log: Cleared Log File: " + file);
                }
            }
        }

        private long GetFileSize(string filename)
        {
            FileInfo file = new FileInfo(filename);
            return file.Length;
        }
    }
}
