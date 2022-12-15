using FMX.AccountsManager.Core.Data;

namespace FMX.AccountsManager.Core
{
    public interface IAccountRecordViewModelFactory
    {
        AccountRecordViewModel Create();
        AccountRecordViewModel Create(AccountRecord record);
        AccountRecordViewModel Create(string label, IEnumerable<AccountRecordFieldViewModel> fields);
    }
}
