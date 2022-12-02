namespace FMX.PasswordManager.DesignModels
{
    public static class AccountRecordItemDesignModel
    {
        public static AccountRecordItemViewModel Instance => new ()
        {
            Label = "Test Label",
            Value = "Test Value"
        };
    }
}
