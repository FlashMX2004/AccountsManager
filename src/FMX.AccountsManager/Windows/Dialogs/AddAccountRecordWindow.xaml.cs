namespace FMX.AccountsManager
{
    /// <summary>
    /// Interaction logic for AddAccountRecordDialog.xaml
    /// </summary>
    public partial class AddAccountRecordDialog : DialogWindowBase<AddAccountRecordViewModel>
    {
        public AddAccountRecordDialog()
        {
            InitializeComponent();
            NewLabelText.Focus();
        }
    }
}
