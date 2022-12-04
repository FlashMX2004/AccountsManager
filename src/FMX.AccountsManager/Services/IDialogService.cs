namespace FMX.AccountsManager
{
    public interface IDialogService
    {
        /// <summary>
        /// Returns dialog by <typeparamref name="VM"/> view model
        /// </summary>
        /// <typeparam name="VM">View model type</typeparam>
        IDialog<VM> Get<VM>() where VM : ViewModelBase, IRequestClose;
    }
}
