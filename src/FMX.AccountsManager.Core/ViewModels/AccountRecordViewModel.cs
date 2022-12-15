using FMX.AccountsManager.Core.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Account record, which is in records collection and stores some account data
    /// </summary>
    public class AccountRecordViewModel : ViewModelBase, IRemovableViewModel, IEditableViewModel, ICloneable
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Accound record label
        /// </summary>
        public required string Label { get; set; }

        /// <summary>
        /// Accound record records (items)
        /// </summary>

        public required ObservableCollection<AccountRecordFieldViewModel> Fields { get; set; }

        #region IRemovableViewModel Members

        /// <summary>
        /// Removes this record when executed
        /// </summary>
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Fires when record is removed
        /// </summary>
        public event EventHandler<RemovedEventArgs> Removed = (_, _) => { };

        #endregion

        #region IEditableViewModel Members

        /// <summary>
        /// Edits this view model when executed
        /// </summary>
        public ICommand EditCommand { get; }

        /// <summary>
        /// Fires when this view model is edited
        /// </summary>
        public event EventHandler<EditedEventArgs> Edited = (_, _) => { };

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRecordViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            RemoveCommand = new RelayCommand(() => 
            {
                // TODO: Localize all MessageBox(
                if (dialogService.MessageBox("Are you sure to delete this account record?",
                                             "Warning",
                                             DialogButton.OK | DialogButton.Cancel)
                                 .ShowDialog() ?? false)
                {
                    Removed(this, new RemovedEventArgs());
                }
            });

            EditCommand = new RelayCommand(() =>
            {
                var dialog = dialogService.Get<AddAccountRecordViewModel>();
                dialog.ViewModel = new AddAccountRecordViewModel((AccountRecordViewModel)Clone());
                if (dialog.ShowDialog() ?? false)
                {
                    var oldLabel = Label!;
                    CopyFrom(dialog.ViewModel.AccountRecord);
                    Edited(this, new EditedEventArgs(oldLabel));
                }
            });
        }

        /// <summary>
        /// Returns an instance of this view model assigned with
        /// </summary>
        public AccountRecord ToData() => new AccountRecord { Label = Label, Fields = Fields.Select(vm => vm.ToData()).ToList() };

        /// <summary>
        /// Clones current view model, so cloned will be another instance
        /// </summary>
        /// <returns>Cloned view model</returns>
        public object Clone() => new AccountRecordViewModel(_dialogService)
        {
            Label = Label,
            Fields = new(Fields.Select(fvm => (AccountRecordFieldViewModel)fvm.Clone()))
        };

        /// <summary>
        /// Copies data from <paramref name="viewModel"/> view model
        /// </summary>
        /// <param name="viewModel">View model to copy from</param>
        public void CopyFrom(AccountRecordViewModel viewModel)
        {
            Label = viewModel.Label;
            Fields = viewModel.Fields;
        }
    }
}
