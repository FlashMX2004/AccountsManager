using System.Windows;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase<MainViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (_, _) =>
            {
                ViewModel!.FocusRequested += ViewModel_FocusRequested;
                ViewModel!.FocusSearchInput.Execute(this);
            };
            Activated += (_, _) =>
            {
                ViewModel!.FocusSearchInput.Execute(this);
            };
        }

        /// <summary>
        /// Fires when view model's focus requested event was fired
        /// </summary>
        private void ViewModel_FocusRequested(object? sender, RequestFocusEventArgs e)
        {
            if (e.FocusedElement == RequestFocusEventArgs.FOCUSED_SEARCH_TEXT)
                SearchTextBox.Focus();
        }

        /// <summary>
        /// Opens context menu of edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OpenEditContextMenu(object? sender, MouseButtonEventArgs args)
        {
            var menu = ((FrameworkElement)sender!).ContextMenu;
            menu.PlacementTarget = sender as UIElement;
            menu.IsOpen = true;
        }
    }
}
