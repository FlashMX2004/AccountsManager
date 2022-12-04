namespace FMX.AccountsManager
{
    public interface IDialog<VM>
        where VM: ViewModelBase
    {
        VM ViewModel { get; set; }
        bool? DialogResult { get; set; }
        bool? ShowDialog();
        void Close();
    }
}
