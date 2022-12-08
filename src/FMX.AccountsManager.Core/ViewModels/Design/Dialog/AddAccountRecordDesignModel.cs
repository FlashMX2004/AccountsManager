namespace FMX.AccountsManager.Core.Design
{
    public class AddAccountRecordDesignModel
    {
        public static AddAccountRecordViewModel Instance => new(AccountRecordDesignModel.Instance);
    }
}
