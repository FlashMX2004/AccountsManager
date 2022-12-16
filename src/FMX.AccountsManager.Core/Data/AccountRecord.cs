namespace FMX.AccountsManager.Core.Data
{
    /// <summary>
    /// Data model for account record
    /// </summary>
    [Serializable]
    public class AccountRecord
    {
        /// <summary>
        /// Accound record label
        /// </summary>
        public required string Label { get; set; }

        /// <summary>
        /// Accound record records (items)
        /// </summary>
        public required List<AccountRecordField> Fields { get; set; }
    }
}
