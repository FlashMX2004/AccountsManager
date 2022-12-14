namespace FMX.AccountsManager.Core.Design
{
    public class DialogDesignService : IDialogService
    {
        public IDialog<VM> Get<VM>() where VM : DialogViewModelBase, IRequestClose
        {
            return null;
        }
    }
}
