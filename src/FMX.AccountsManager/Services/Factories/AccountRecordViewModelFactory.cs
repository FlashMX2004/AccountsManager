using FMX.AccountsManager.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace FMX.AccountsManager
{
    public class AccountRecordViewModelFactory : IAccountRecordViewModelFactory
    {
        private readonly IDialogService _dialogService;
        private readonly IAccountRecordFieldViewModelFactory _fieldFactory;

        public AccountRecordViewModelFactory(IDialogService dialogService,
                                             IAccountRecordFieldViewModelFactory fieldFactory)
        {
            _dialogService = dialogService;
            _fieldFactory = fieldFactory;
        }

        public AccountRecordViewModel Create() => new(_dialogService)
        {
            Label = "",
            Fields = new()
        };

        public AccountRecordViewModel Create(AccountRecord record) => new(_dialogService)
        {
            Label = record.Label,
            Fields = new(record.Fields.Select(_fieldFactory.Create))
        };

        public AccountRecordViewModel Create(string label, IEnumerable<AccountRecordFieldViewModel> fields) => new(_dialogService)
        {
            Label = label,
            Fields = new(fields)
        };
    }
}
