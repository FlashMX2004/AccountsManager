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
