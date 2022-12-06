namespace FMX.AccountsManager
{
    public class DialogWindowBase<VM> : WindowBase<VM>, IDialog<VM>
        where VM : DialogViewModelBase, IRequestClose
    {
        public DialogWindowBase()
        {
            KeyDown += (_, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape)
                    ViewModel!.CancelCommand.Execute(null);
            };
        }

        protected override void OnCloseRequested(object? sender, RequestCloseEventArgs e)
        {
            base.OnCloseRequested(sender, e);
            Closing += (_, _) => DialogResult = e.Result;
        }
    }
}
