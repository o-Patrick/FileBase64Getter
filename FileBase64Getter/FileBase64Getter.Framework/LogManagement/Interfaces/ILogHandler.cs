namespace FileBase64Getter.Framework.LogManagement.Interfaces
{
    public interface ILogHandler
    {
        void FileBuilder(string message);
        Task<bool> SaveFileLocallyAsync(string processName);
    }
}
