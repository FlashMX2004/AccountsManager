namespace FMX.AccountsManager.Core.Design
{
    public static class AccountRecordFieldDesignModel
    {
        public static AccountRecordFieldViewModel Instance => new (new ClipboardDesignService())
        {
            Label = "Design Field Label",
            Value = "Design Field Value"
        };
    }
}
