namespace FMX.AccountsManager.Design
{
    public class AccountRecordDesignModel
    {
        public static AccountRecordViewModel Instance => new()
        {
            Label = "Design Record Label",
            Fields = new()
            {
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
            }
        };
    }
}
