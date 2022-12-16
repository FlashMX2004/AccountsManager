using FMX.AccountsManager.Core.Data;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Record (item) of account record
    /// </summary>
    public class AccountRecordFieldViewModel : ViewModelBase, ICloneable
    {
        private readonly IClipboardService _clipboardService;

        /// <summary>
        /// Label of record
        /// </summary>
        public string Label { get; set; } = "";

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
            _clipboardService = clipboardService;

            CopyToClipboardCommand = new RelayCommand(() => _clipboardService.CopyToClipboard(Value));
        }

        /// <summary>
        /// Returns an instance of this view model assigned with
        /// </summary>
        public AccountRecordField ToData() => new() { Label = Label, Value = Value };

        /// <summary>
        /// Clones current view model, so cloned will be another instance
        /// </summary>
        /// <returns>Cloned view model</returns>
        public object Clone() => new AccountRecordFieldViewModel(_clipboardService)
        {
            Label = Label,
            Value = Value
        };
    }
}
