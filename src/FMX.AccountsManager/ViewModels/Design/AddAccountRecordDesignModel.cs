namespace FMX.AccountsManager.Design
{
    public class AddAccountRecordDesignModel
    {
        public static AddAccountRecordViewModel Instance => new()
        {
            AccountRecord = AccountRecordDesignModel.Instance
        };
    }
}
