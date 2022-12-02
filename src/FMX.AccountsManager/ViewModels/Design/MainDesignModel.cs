namespace FMX.PasswordManager.DesignModels
{
    public class MainDesignModel
    {
        public static MainViewModel Instance => new(null)
        {
            SearchFilter = "Search  Filter",
            Records = new()
            {
                AccounrRecordDesignModel.Instance,
                AccounrRecordDesignModel.Instance,
                AccounrRecordDesignModel.Instance,
            },
        };
    }
}
