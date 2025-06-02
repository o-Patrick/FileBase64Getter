﻿using FileBase64Getter.Framework.LogManagement.Interfaces;
using System.Text;

namespace FileBase64Getter.Framework.LogManagement
{
    public class LogHandler : ILogHandler
    {
        #region | Fields |
        private readonly StringBuilder _fileContent;
        #endregion

        #region | Constructor |
        public LogHandler()
        {
            _fileContent = new StringBuilder();
        }
        #endregion

        #region | Methods |
        /// <summary>
        /// Logs messages into the console
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void ConsoleWrite(string message)
        {
            message = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} | {message}";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Writes a message and reads another on the console
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static string ConsoleRead(string? prompt = null)
        {
            if (prompt != null)
            {
                ConsoleWrite(prompt);
            }
            
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// StringBuilder for logging
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public void FileBuilder(string message)
        {
            _fileContent.AppendLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} | {message}");
        }

        /// <summary>
        /// Saves log file locally
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public async Task<bool> SaveFileLocallyAsync(string processName)
        {
            try
            {
                string fileName = $"{DateTime.Now:yyyMMddTHHmmss}_{processName}.txt";
                string fileFullPath = Path.Combine(Directory.GetCurrentDirectory(), "Log", fileName);

                using var streamWriter = new StreamWriter(fileFullPath, true, Encoding.UTF8, _fileContent.Length);
                await streamWriter.WriteAsync(_fileContent);
                await streamWriter.FlushAsync();

                ConsoleWrite($"{fileName} saved successfully");
                return true;
            }
            catch (Exception exc)
            {
                ConsoleWrite($"Exception {exc}");
            }

            return false;
        }
        #endregion
    }
}
