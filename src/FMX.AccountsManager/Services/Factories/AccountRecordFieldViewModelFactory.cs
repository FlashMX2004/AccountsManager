using FMX.AccountsManager.Core.Data;

namespace FMX.AccountsManager
{
    public class AccountRecordFieldViewModelFactory : IAccountRecordFieldViewModelFactory
    {
        private readonly IClipboardService _clipboardService;

        public AccountRecordFieldViewModelFactory(IClipboardService clipboardService)
        {
            _clipboardService = clipboardService;
        }

        public AccountRecordFieldViewModel Create() => new(_clipboardService)
        {
            Label = "",
            Value = ""
        };

        public AccountRecordFieldViewModel Create(AccountRecordField field) => new(_clipboardService)
        {
            Label = field.Label,
            Value = field.Value,
        };

        public AccountRecordFieldViewModel Create(string label, string value) => new(_clipboardService)
        {
            Label = label,
            Value = value,
        };
    }
}
