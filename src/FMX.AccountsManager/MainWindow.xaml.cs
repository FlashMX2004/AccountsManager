using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SearchTextBox.Focus();
            KeyDown += (_, e) => { if (e.Key == Key.Escape) Close(); };

            SetWindowPosition();
            UpdatePasswordBlocks("");

            /*List<AccountRecordViewModel> records = new()
            {
                new AccountRecordViewModel
                {
                    Label = "Account1",
                    Fields = new()
                    {
                        new AccountRecordFieldViewModel { Label = "Username", Value = "Username1" },
                        new AccountRecordFieldViewModel { Label = "Email", Value = "Email1" },
                        new AccountRecordFieldViewModel { Label = "Password", Value = "Password1" },
                    }
                },
                new AccountRecordViewModel
                {
                    Label = "Account2",
                    Fields = new()
                    {
                        new AccountRecordFieldViewModel { Label = "Email", Value = "Email2" },
                        new AccountRecordFieldViewModel { Label = "Password", Value = "Password2" },
                    }
                }
            };

            App.GetService<IRecordService>().UpdateAllRecords(records);*/
        }

        private void UpdatePasswordBlocks(string searchFilter)
        {

        }

        private void SetWindowPosition()
        {
            var screen = WpfScreen.GetScreenFrom(this);

            // I don't know why i need to add these 8px for correct view...
            Top = screen.WorkingArea.Bottom - Height + 8;
            Left = screen.WorkingArea.Right / 2 - Width / 2;
        }
    }
}
