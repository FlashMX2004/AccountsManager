using FMX.AccountsManager.Core.Data;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Record (item) of account record
    /// </summary>
    public class AccountRecordFieldViewModel : ViewModelBase
    {
        private string _label = "";

        /// <summary>
        /// Label of record
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                // TODO:
                // I don't really know why FodyWeavers doesn't invoke PropertyChanged,
                // but properties are updeted when ui is updated
                if (_label != value)
                {
                    _label = value;
                    InvokePropertyChanged(nameof(Label));
                }
            }
        }

        /// <summary>
        /// Value of record
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// Copies value to clipboard when executed
        /// </summary>
        public ICommand CopyToClipboardCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRecordFieldViewModel(IClipboardService clipboardService)
        {
            CopyToClipboardCommand = new RelayCommand(() => clipboardService.CopyToClipboard(Value));
        }

        /// <summary>
        /// Returns an instance of this view model assigned with
        /// </summary>
        public AccountRecordField ToData() => new() { Label = Label, Value = Value };
    }
}
