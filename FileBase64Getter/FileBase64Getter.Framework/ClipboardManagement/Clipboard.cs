using System.Runtime.InteropServices;

namespace FileBase64Getter.Framework.ClipboardManagement
{
    public static class Clipboard
    {
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        static extern bool SetClipboardData(uint uFormat, IntPtr hMem);

        public static void SetText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                throw new Exception("Cannot open clipboard.");
            }

            try
            {
                var hGlobal = Marshal.StringToHGlobalUni(text);
                SetClipboardData(13, hGlobal);
            }
            finally
            {
                CloseClipboard();
            }
        }
    }
}
