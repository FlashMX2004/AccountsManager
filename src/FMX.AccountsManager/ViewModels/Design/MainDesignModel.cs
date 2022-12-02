namespace FMX.PasswordManager.DesignModels
{
    public class MainDesignModel
    {
        public static MainViewModel Instance => new()
        {
            SearchFilter = "Search  Filter",
            Blocks = new()
            {
                PasswordBlockDesignModel.Instance,
                PasswordBlockDesignModel.Instance,
                PasswordBlockDesignModel.Instance,
            },
        };
    }
}
