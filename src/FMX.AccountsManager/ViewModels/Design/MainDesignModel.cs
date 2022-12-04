namespace FMX.AccountsManager.Design
{
    public class MainDesignModel
    {
        public static MainViewModel Instance => new(new RecordDesignService(), new DialogDesignService())
        {
            SearchFilter = "Dsign Search Filter Text",
            Records = new()
            {
                AccountRecordDesignModel.Instance,
                AccountRecordDesignModel.Instance,
                AccountRecordDesignModel.Instance,
            },
        };
    }
}
