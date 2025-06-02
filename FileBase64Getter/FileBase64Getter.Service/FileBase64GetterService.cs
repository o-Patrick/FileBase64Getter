using static FileBase64Getter.Framework.ClipboardManagement.Clipboard;
using static FileBase64Getter.Framework.LogManagement.LogHandler;
using FileBase64Getter.Framework.LogManagement.Interfaces;

namespace FileBase64Getter.Service
{
    public class FileBase64GetterService
    {
        #region | Fields |
        private readonly ILogHandler _logger;
        private readonly string _processName;
        #endregion

        #region | Constructors |
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        public FileBase64GetterService(ILogHandler logger)
        {
            _logger = logger;
            _processName = GetType().Name;
        }
        #endregion

        #region | Methods |
        /// <summary>
        /// Main method
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            _logger.FileBuilder("Program started.");
            bool restartProgram;
            
            try
            {
                do
                {
                    Console.Clear();
                    bool startProcess = ConsoleRead("Start process? (y/n)").Equals("y", StringComparison.InvariantCultureIgnoreCase);

                    if (startProcess)
                    {
                        var filePath = ConsoleRead("Insert the complete file path:").Trim().Replace("\"", string.Empty).Replace("\'", string.Empty);

                        if (!File.Exists(filePath))
                        {
                            _logger.FileBuilder($"File not found at {filePath}.");
                        }
                        else
                        {
                            _logger.FileBuilder("File found.");
                            var base64Content = await ReadFileAsync(filePath);

                            if (base64Content.Length <= 0)
                            {
                                _logger.FileBuilder($"File {Path.GetFileName(filePath)} could not be read.");
                            }
                            else
                            {
                                ConsoleWrite($"File Base64 content: {base64Content}");
                                _logger.FileBuilder("File Base64 generated successfully.");
                                SetText(base64Content);
                                ConsoleWrite("Base64 copied to clipboard.");
                            }
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }

                    restartProgram = ConsoleRead("Restart program? (y/n)").Equals("y", StringComparison.InvariantCultureIgnoreCase);
                } while (restartProgram);
            }
            catch (Exception e)
            {
                _logger.FileBuilder($"Exception: {e}.");
            }
            
            _logger.FileBuilder("Program finished.");
            await _logger.SaveFileLocallyAsync(_processName);
        }

        #region | Private methods |
        /// <summary>
        /// Reads file asynchronously
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static async Task<string> ReadFileAsync(string filePath)
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(fileBytes);
        }
        #endregion
        #endregion
    }
}
