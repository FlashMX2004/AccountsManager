namespace FMX.AccountsManager.Core.Data
{
    /// <summary>
    /// Data model for account record field
    /// </summary>
    [Serializable]
    public class AccountRecordField
    {
        /// <summary>
        /// Label of record
        /// </summary>
        public required string Label { get; set; }

        /// <summary>
        /// Value of record
        /// </summary>
        public required string Value { get; set; }
    }
}
