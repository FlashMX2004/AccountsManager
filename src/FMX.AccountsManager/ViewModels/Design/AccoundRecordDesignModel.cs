namespace FMX.PasswordManager.DesignModels
{
    public class AccounrRecordDesignModel
    {
        public static AccoundRecordViewModel Instance => new()
        {
            Label = "Test Label",
            Records = new()
            {
                AccountRecordItemDesignModel.Instance,
                AccountRecordItemDesignModel.Instance,
                AccountRecordItemDesignModel.Instance,
            }
        };
    }
}
