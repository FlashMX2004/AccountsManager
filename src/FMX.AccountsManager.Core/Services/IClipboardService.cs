namespace FMX.AccountsManager.Core
{
    public interface IClipboardService
    {
        /// <summary>
        /// Copies some data to clipboard
        /// </summary>
        /// <param name="toCopy">Data that will be copied to clipboard</param>
        void CopyToClipboard(string toCopy);
    }
}
