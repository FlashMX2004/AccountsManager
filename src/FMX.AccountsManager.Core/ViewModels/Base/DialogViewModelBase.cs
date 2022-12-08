using System;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Represents one of dialog buttons
    /// </summary>
    public enum DialogButton
    {
        OK = 0b1,
        No = 0b10,
        Cancel = 0b100
    }

    /// <summary>
    /// Base view model for all dialogs
    /// </summary>
    public class DialogViewModelBase : ViewModelBase, IRequestClose
    {
        /// <summary>
        /// Dialog "OK" command, executes when user agree with dialog
        /// </summary>
        public ICommand OkCommand { get; set; }

        /// <summary>
        /// Dialog "No" command, executes when user disagree with dialog
        /// </summary>
        public ICommand NoCommand { get; set; }

        /// <summary>
        /// Dialog "Cancel" command, executes when user cancels dialog
        /// </summary>
        public ICommand CancelCommand { get; set; }

        public DialogViewModelBase()
        {
            // Set commands
            OkCommand     = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(true)));
            NoCommand     = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(false)));
            CancelCommand = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(null)));
        }

        #region IRequestClose Implementation

        /// <summary>
        /// Fires when some view requests for close it
        /// </summary>
        public event EventHandler<RequestCloseEventArgs> CloseRequested = (_, _) => { };

        #endregion

    }
}
