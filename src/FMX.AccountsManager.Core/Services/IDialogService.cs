namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Contract for all dialog services
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Returns dialog by <typeparamref name="VM"/> view model
        /// </summary>
        /// <typeparam name="VM">View model type</typeparam>
        IDialog<VM> Get<VM>() where VM : DialogViewModelBase, IRequestClose;

        /// <summary>
        /// Shows message box to user
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="title">Title</param>
        /// <param name="buttons">Buttons (DialogButton.OK | DialogButton.Cancel)</param>
        IDialog<MessageBoxViewModel> MessageBox(string message, string title = "Information", DialogButton buttons = DialogButton.OK)
        {
            var mbox = Get<MessageBoxViewModel>();
            mbox.ViewModel.Message = message;
            mbox.ViewModel.Title = title;
            mbox.ViewModel.IsOkVisible =     (buttons & DialogButton.OK) == DialogButton.OK;
            mbox.ViewModel.IsNoVisible =     (buttons & DialogButton.No) == DialogButton.No;
            mbox.ViewModel.IsCancelVisible = (buttons & DialogButton.Cancel) == DialogButton.Cancel;
            return mbox;
        }

        string? SaveDialog(ISerializator serializator);
        string? OpenDialog();
    }
}
