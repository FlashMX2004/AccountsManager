namespace FMX.AccountsManager.DesignModels
{
    public class MainDesignModel
    {
        public static MainViewModel Instance => new(null)
        {
            SearchFilter = "Search  Filter",
            Records = new()
            {
                AccountRecordDesignModel.Instance,
                AccountRecordDesignModel.Instance,
                AccountRecordDesignModel.Instance,
            },
        };
    }
}
