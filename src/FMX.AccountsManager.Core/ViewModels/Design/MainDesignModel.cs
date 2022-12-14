using System.Runtime.Serialization;

namespace FMX.AccountsManager.Core.Design
{
    public class MainDesignModel
    {
        public static MainViewModel Instance => new(new RecordDesignService(),
                                                    new DialogDesignService(),
                                                    new SerializationServiceDesign(),
                                                    (IBinarySerializator)new SerializatorDesign(),
                                                    (IXMLSerializator)new SerializatorDesign())
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
