namespace FMX.AccountsManager
{
    /// <summary>
    /// Base for all dialog windows
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class DialogWindowBase<VM> : WindowBase<VM>, IDialog<VM>
        where VM : DialogViewModelBase, IRequestClose
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogWindowBase()
        {
            KeyDown += (_, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Escape)
                    ViewModel!.CancelCommand.Execute(null);
            };
        }

        /// <summary>
        /// Occurs when close was requested
        /// </summary>
        protected override void OnCloseRequested(object? sender, RequestCloseEventArgs e)
        {
            base.OnCloseRequested(sender, e);

            // Subscribe on closing event
            Closing += (_, _) => DialogResult = e.Result;
        }
    }
}
