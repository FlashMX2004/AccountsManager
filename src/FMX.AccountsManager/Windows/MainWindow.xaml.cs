namespace FMX.AccountsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            SearchTextBox.Focus();
        }
    }
}
