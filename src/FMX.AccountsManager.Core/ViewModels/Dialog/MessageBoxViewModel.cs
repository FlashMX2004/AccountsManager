namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// View model for message box view
    /// </summary>
    public class MessageBoxViewModel : DialogViewModelBase
    {
        /// <summary>
        /// Message box title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Message box text
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Determines if "OK" button is visible
        /// </summary>
        public bool IsOkVisible { get; set; } = true;

        /// <summary>
        /// Determines if "No" button is visible
        /// </summary>
        public bool IsNoVisible { get; set; }

        /// <summary>
        /// Determines if "Cancel" button is visible
        /// </summary>
        public bool IsCancelVisible { get; set; }
    }
}
