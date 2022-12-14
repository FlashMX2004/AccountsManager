namespace FMX.AccountsManager.Core
{
    public interface ISerializationService
    {
        void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels);
    }
}
