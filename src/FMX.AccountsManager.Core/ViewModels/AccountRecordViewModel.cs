using FMX.AccountsManager.Core.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Account record, which is in records collection and stores some account data
    /// </summary>
    public class AccountRecordViewModel : ViewModelBase, IRemovable
    {
        /// <summary>
        /// Accound record label
        /// </summary>
        public required string Label { get; set; }

        /// <summary>
        /// Accound record records (items)
        /// </summary>
        public required ObservableCollection<AccountRecordFieldViewModel> Fields { get; set; }

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
        public AccountRecordViewModel(IDialogService dialogService)
        {
            RemoveCommand = new RelayCommand(() => 
            {
                // TODO: Localize
                if (dialogService.MessageBox("Are you sure to delete this account record?",
                                             "Warning",
                                             DialogButton.OK | DialogButton.Cancel)
                                 .ShowDialog() ?? false)
                {
                    Removed(this, new RemovedEventArgs());
                }
            });
        }

        /// <summary>
        /// Returns an instance of this view model assigned with
        /// </summary>
        public AccountRecord ToData() => new AccountRecord { Label = Label, Fields = Fields.Select(vm => vm.ToData()).ToList() };
    }
}
