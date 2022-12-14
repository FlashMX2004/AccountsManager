using System.Windows;

namespace FMX.AccountsManager
{
    public class ClipboardService : IClipboardService
    {
        public void CopyToClipboard(string toCopy)
        {
            Clipboard.SetText(toCopy);
        }
    }
}
