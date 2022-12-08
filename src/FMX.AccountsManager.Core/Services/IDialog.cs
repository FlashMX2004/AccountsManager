namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Cantract for all dialogs for users
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public interface IDialog<VM>
        where VM: DialogViewModelBase
    {
        /// <summary>
        /// Dialog's view model
        /// </summary>
        VM ViewModel { get; set; }

        /// <summary>
        /// User's choosing result
        /// </summary>
        bool? DialogResult { get; set; }

        /// <summary>
        /// Show this dialog to user
        /// </summary>
        bool? ShowDialog();

        /// <summary>
        /// Close dialog
        /// </summary>
        void Close();
    }
}
