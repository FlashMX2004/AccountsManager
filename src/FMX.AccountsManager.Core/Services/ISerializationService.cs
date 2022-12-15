namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Service for serializing / deserializing data by some serializators
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Serializing <paramref name="viewModels"/> collection by <paramref name="serializator"/>
        /// </summary>
        /// <param name="serializator">Serializator that will serialize <paramref name="viewModels"/> collection</param>
        /// <param name="viewModels">Account record view models collection</param>
        void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels);

        /// <summary>
        /// Deserializing view models collection
        /// </summary>
        IEnumerable<AccountRecordViewModel> Deserialize();
    }
}
