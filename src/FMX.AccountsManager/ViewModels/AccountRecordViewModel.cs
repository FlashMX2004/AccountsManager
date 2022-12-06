using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Account record, which is in records collection and stores some account data
    /// </summary>
    public class AccountRecordViewModel : ViewModelBase, IRemovable
    {
        /// <summary>
        /// Accound record label
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// Accound record records (items)
        /// </summary>
        public ObservableCollection<AccountRecordFieldViewModel> Fields { get; set; } = new();

        #region IRemovable Members

        /// <summary>
        /// Removes this record when executed
        /// </summary>
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Fires when record is removed
        /// </summary>
        public event EventHandler<RemovedEventArgs> Removed = (_, _) => { };

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRecordViewModel()
        {
            RemoveCommand = new RelayCommand(() => Removed(this, new RemovedEventArgs()));
        }
    }
}
