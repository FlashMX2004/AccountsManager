using System;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Base view model for all dialogs
    /// </summary>
    public class DialogViewModelBase : ViewModelBase, IRequestClose
    {
        /// <summary>
        /// Fires when some view requests for close it
        /// </summary>
        public event EventHandler<RequestCloseEventArgs> CloseRequested = (_, _) => { };

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

    }
}
