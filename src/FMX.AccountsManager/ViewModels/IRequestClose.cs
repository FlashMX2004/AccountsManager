using System;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Requestable for close
    /// </summary>
    public interface IRequestClose
    {
        /// <summary>
        /// Fires when some view requests for close it
        /// </summary>
        event EventHandler<RequestCloseEventArgs> CloseRequested;
    }

    /// <summary>
    /// Event arguments for close request
    /// </summary>
    public class RequestCloseEventArgs : EventArgs
    {
        /// <summary>
        /// Close result
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="result">Close result</param>
        public RequestCloseEventArgs(bool? result)
        {
            Result = result;
        }
    }
}
