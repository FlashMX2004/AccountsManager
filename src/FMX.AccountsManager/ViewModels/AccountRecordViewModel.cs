using System.Collections.ObjectModel;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Account record, which is in records collection and stores some account data
    /// </summary>
    public class AccountRecordViewModel : ViewModelBase
    {
        /// <summary>
        /// Accound record label
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// Accound record records (items)
        /// </summary>
        public ObservableCollection<AccountRecordFieldViewModel> Fields { get; set; } = new();
    }
}
