﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
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

        public ICommand FilterCommand { get; set; }

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

            foreach (var rec in _recordService.GetAllRecords())
                AddRecord(rec);

            AddAccountRecordCommand = new RelayCommand(() =>
            {
                var dialog = _dialogService.Get<AddAccountRecordViewModel>();

                /*
                
                void AddAccountRecordViewModel_CloseRequested(object? _, RequestCloseEventArgs e)
                {
                    dialog.ViewModel.CloseRequested -= AddAccountRecordViewModel_CloseRequested;
                    var newRecord = dialog.ViewModel.AccountRecord;

                    if (!string.IsNullOrEmpty(newRecord.Label) &&
                        (e.Result ?? false))
                    {
                        _recordService.AddRecord(newRecord.Label);
                        foreach (var field in newRecord.Fields)
                        {
                            _recordService.AddRecordField(newRecord.Label, field.Label);
                            _recordService.UpdateRecordFieldValue(newRecord.Label, field.Label, field.Value);
                        }

                        AddRecord(newRecord);
                    }
                }
                dialog.ViewModel.CloseRequested += AddAccountRecordViewModel_CloseRequested;
                dialog.ShowDialog();

                */

                if (dialog.ShowDialog() ?? false)
                {
                    var newRecord = dialog.ViewModel.AccountRecord;
                    if (!string.IsNullOrEmpty(newRecord.Label))
                    {
                        _recordService.AddRecord(newRecord.Label);
                        foreach (var field in newRecord.Fields)
                        {
                            _recordService.AddRecordField(newRecord.Label, field.Label);
                            _recordService.UpdateRecordFieldValue(newRecord.Label, field.Label, field.Value);
                        }
                        AddRecord(newRecord);
                    }
                }
            });

            // TODO: Tests for filter
            FilterCommand = new RelayCommand(() =>
            {
                UpdateRecords(_recordService.FilterRecords(SearchFilter));
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

        /// <summary>
        /// Adds record and subscribes on its remove event
        /// </summary>
        /// <param name="record">Record that will be added</param>
        private void AddRecord(AccountRecordViewModel record)
        {
            Records.Add(record);
            record.Removed += NewRecord_Removed;
        }

        /// <summary>
        /// Removes record and unsubscribes from its remove event
        /// </summary>
        /// <param name="record">Record that will be added</param>
        private void RemoveRecord(AccountRecordViewModel record)
        {
            Records.Remove(record);
            record.Removed -= NewRecord_Removed;
        }

        private void UnsubscribeAll()
        {
            //foreach (var r in Records) r.Removed -= NewRecord_Removed;
            for (int i = 0; i < Records.Count; i++) Records[i].Removed -= NewRecord_Removed;
        }

        private void UpdateRecords(IEnumerable<AccountRecordViewModel> records)
        {
            UnsubscribeAll();
            Records.Clear();

            foreach (var rec in records) AddRecord(rec);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            UnsubscribeAll();
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