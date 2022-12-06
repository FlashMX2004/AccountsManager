namespace FMX.AccountsManager
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
    }
}
