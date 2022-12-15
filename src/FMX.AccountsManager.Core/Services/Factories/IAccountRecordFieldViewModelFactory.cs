using FMX.AccountsManager.Core.Data;

namespace FMX.AccountsManager.Core
{
    public interface IAccountRecordFieldViewModelFactory
    {
        AccountRecordFieldViewModel Create();
        AccountRecordFieldViewModel Create(AccountRecordField field);
        AccountRecordFieldViewModel Create(string label, string value);
    }
}
