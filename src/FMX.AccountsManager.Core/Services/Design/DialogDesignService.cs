namespace FMX.AccountsManager.Core.Design
{
    public class DialogDesignService : IDialogService
    {
        public IDialog<VM> Get<VM>() where VM : DialogViewModelBase, IRequestClose
        {
            return null;
        }

        public string? OpenDialog()
        {
            throw new NotImplementedException();
        }

        public string? SaveDialog(ISerializator serializator)
        {
            throw new NotImplementedException();
        }
    }
}
