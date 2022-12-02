namespace FMX.AccountsManager
{
    /// <summary>
    /// Record (item) of account record
    /// </summary>
    public class AccountRecordFieldViewModel : ViewModelBase
    {
        /// <summary>
        /// Label of record
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// Value of record
        /// </summary>
        public string Value { get; set; } = "";
    }
}
