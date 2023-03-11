namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Requestable for focus of some element
    /// </summary>
    public interface IRequestFocus
    {
        /// <summary>
        /// Fires when some view requests for focus it
        /// </summary>
        event EventHandler<RequestFocusEventArgs> FocusRequested;
    }

    /// <summary>
    /// Event arguments for focus request
    /// </summary>
    public class RequestFocusEventArgs : EventArgs
    {
        public const string FOCUSED_SEARCH_TEXT = "fcs_srch";

        /// <summary>
        /// Focused element as string
        /// </summary>
        public string? FocusedElement { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestFocusEventArgs()
        {
            FocusedElement = null;
        }

        /// <summary>
        /// Creates request focus event arguments with focused element
        /// </summary>
        /// <param name="focusedElement">Focused element as string</param>
        public RequestFocusEventArgs(string focusedElement)
        {
            FocusedElement = focusedElement;
        }
    }
}
