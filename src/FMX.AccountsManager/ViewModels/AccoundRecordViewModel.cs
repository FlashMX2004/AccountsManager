using System.Collections.ObjectModel;

namespace FMX.PasswordManager
{
    /// <summary>
    /// Password block, which is in passwords collection
    /// </summary>
    public class AccoundRecordViewModel : ViewModelBase
    {
        /// <summary>
        /// Password block label
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// Password records collection
        /// </summary>
        public ObservableCollection<AccountRecordItemViewModel> Records { get; set; } = new();
    }
}
