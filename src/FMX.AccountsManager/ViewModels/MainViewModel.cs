using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// View model for main application view
    /// </summary>
    public class MainViewModel : ViewModelBase, IRequestClose, IDisposable
    {
        #region Privates

        private readonly IRecordService _recordService;
        private readonly IDialogService _dialogService;

        #endregion

        #region Publix

        /// <summary>
        /// Search filter value
        /// </summary>
        public string SearchFilter { get; set; } = "";

        /// <summary>
        /// Account records collection
        /// </summary>
        public ObservableCollection<AccountRecordViewModel> Records { get; set; } = new();

        #endregion

        #region Commands

        /// <summary>
        /// Adds account record when executed
        /// </summary>
        public ICommand AddAccountRecordCommand { get; set; }

        /// <summary>
        /// Closes application when executed
        /// </summary>
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="recordService">Service for managing records</param>
        /// <param name="dialogService">Service for managing dialogs</param>
        public MainViewModel(IRecordService recordService, IDialogService dialogService)
        {
            _recordService = recordService;
            _dialogService = dialogService;

            foreach (var rec in recordService.GetAllRecords())
                AddRecord(rec);

            AddAccountRecordCommand = new RelayCommand(() =>
            {
                var dialog = dialogService.Get<AddAccountRecordViewModel>();
                // dialog.ViewModel.CloseRequested += () => { /*Something*/ };
                dialog.ShowDialog();
                var newRecord = dialog.ViewModel.AccountRecord;

                // TODO: Update IRecordService
                // TODO: Make validation logic in AddAccountRecordView

                if (!string.IsNullOrEmpty(newRecord.Label) &&
                    (dialog.DialogResult ?? false))
                {
                    recordService.AddRecord(newRecord.Label);
                    foreach (var field in newRecord.Fields)
                    {
                        recordService.AddRecordField(newRecord.Label, field.Label);
                        recordService.UpdateRecordFieldValue(newRecord.Label, field.Label, field.Value);
                    }

                    AddRecord(newRecord);
                }
            });

            CloseCommand = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(true)));
        }

        private void NewRecord_Removed(object? sender, RemovedEventArgs e)
        {
            if (sender is AccountRecordViewModel vm)
            {
                _recordService.DeleteRecord(vm.Label);
                RemoveRecord(vm);
            }
        }

        #endregion

        #region Helpers

        private void AddRecord(AccountRecordViewModel record)
        {
            Records.Add(record);
            record.Removed += NewRecord_Removed;
        }

        private void RemoveRecord(AccountRecordViewModel record)
        {
            Records.Remove(record);
            record.Removed -= NewRecord_Removed;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var r in Records)
            {
                r.Removed -= NewRecord_Removed;
            }
        }

        #endregion

        #region IRequestClose Members

        /// <summary>
        /// Fires when some view requests for close it
        /// </summary>
        public event EventHandler<RequestCloseEventArgs> CloseRequested = (_, _) => { };

        #endregion
    }
}
