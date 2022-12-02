namespace FMX.PasswordManager.DesignModels
{
    public static class PasswordBlockRecordDesignModel
    {
        public static PasswordBlockRecordViewModel Instance => new ()
        {
            Label = "Test Label",
            Value = "Test Value"
        };
    }
}
