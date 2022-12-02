using System.Collections.ObjectModel;

namespace FMX.AccountsManager
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Search filter value
        /// </summary>
        public string SearchFilter { get; set; } = "";

        /// <summary>
        /// Account records collection
        /// </summary>
        public ObservableCollection<AccountRecordViewModel>? Records { get; set; }

        public MainViewModel(IRecordService recordService)
        {

        }
    }
}
