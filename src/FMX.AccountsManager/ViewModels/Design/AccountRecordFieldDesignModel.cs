namespace FMX.AccountsManager.DesignModels
{
    public static class AccountRecordFieldDesignModel
    {
        public static AccountRecordFieldViewModel Instance => new ()
        {
            Label = "Test Label",
            Value = "Test Value"
        };
    }
}
