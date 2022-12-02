namespace FMX.AccountsManager.DesignModels
{
    public class AccountRecordDesignModel
    {
        public static AccountRecordViewModel Instance => new()
        {
            Label = "Test Label",
            Fields = new()
            {
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
                AccountRecordFieldDesignModel.Instance,
            }
        };
    }
}
