namespace Service.FileBase64Getter
{
    public class FileBase64GetterService
    {
        public void Execute()
        {
            try
            {
                ConsoleWrite("Start process? (y/n)");
                var answer = ConsoleRead();
                bool startProcess = answer != null && answer.Equals("y", StringComparison.InvariantCultureIgnoreCase);

                if (startProcess)
                {
                    ConsoleWrite("Insert the complete file path:");
                    var filePath = ConsoleRead()?.Replace("\"", string.Empty);

                    if (filePath != null)
                    {
                        var base64Content = ReadFile(filePath);

                        if (base64Content.Length <= 0)
                        {
                            throw new FileNotFoundException("Inserted file could not be found");
                        }

                        ConsoleWrite($"File Base64 content: {base64Content}");
                    }
                }
            }
            catch (Exception exc)
            {
                ConsoleWrite($"Exception: {exc}.");
            }
        }

        private string ReadFile(string filePath)
        {
            string base64Content = string.Empty;

            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                base64Content = Convert.ToBase64String(fileBytes);
            }
            catch (Exception exc)
            {
                ConsoleWrite($"Exception: {exc}");
            }

            return base64Content;
        }

        private static void ConsoleWrite(string message)
        {
            message = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} | {message} |";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static string? ConsoleRead()
        {
            return Console.ReadLine();
        }
    }
}
