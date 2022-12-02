namespace FMX.PasswordManager.DesignModels
{
    public class PasswordBlockDesignModel
    {
        public static PasswordBlockViewModel Instance => new()
        {
            Label = "Test Label",
            Records = new()
            {
                PasswordBlockRecordDesignModel.Instance,
                PasswordBlockRecordDesignModel.Instance,
                PasswordBlockRecordDesignModel.Instance,
            }
        };
    }
}
