using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// View model for main application view
    /// </summary>
    public class MainViewModel : ViewModelBase, IRequestClose, IRequestFocus, IDisposable
    {
        #region Privates

        private readonly IRecordService _recordService;
        private readonly IDialogService _dialogService;
        private readonly ISerializationService _serializationService;
        private readonly IBinarySerializator _binarySerializator;
        private readonly IXMLSerializator _xmlSerializator;

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

        /// <summary>
        /// Filters records when executed
        /// </summary>
        public ICommand FilterCommand { get; set; }

        /// <summary>
        /// Saves to binary file format
        /// </summary>
        public ICommand SaveToBinaryCommand { get; set; }

        /// <summary>
        /// Saves to XML file format
        /// </summary>
        public ICommand SaveToXMLCommand { get; set; }

        /// <summary>
        /// Imports binary or xml etc... backup
        /// </summary>
        public ICommand ImportBackupCommand { get; set; }

        /// <summary>
        /// Removes all data from device
        /// </summary>
        public ICommand DeleteAllDataCommand { get; set; }

        /// <summary>
        /// Focuses user on search input
        /// </summary>
        public ICommand FocusSearchInput { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="recordService">Service for managing records</param>
        /// <param name="dialogService">Service for managing dialogs</param>
        public MainViewModel(IRecordService recordService,
                             IDialogService dialogService,
                             ISerializationService serializationService,
                             IBinarySerializator binarySerializator,
                             IXMLSerializator xmlSerializator)
        {
            _recordService = recordService;
            _dialogService = dialogService;
            _serializationService = serializationService;
            _binarySerializator = binarySerializator;
            _xmlSerializator = xmlSerializator;

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
                        _recordService.AddRecord(newRecord);
                        AddRecord(newRecord);
                    }
                }
            });

            // TODO: Tests for filter
            FilterCommand = new RelayCommand(() =>
            {
                UpdateRecords(_recordService.FilterRecords(SearchFilter));
            });

            SaveToBinaryCommand = new RelayCommand(() => 
            {
                var path = _dialogService.SaveDialog(_binarySerializator);
                if (path is not null)
                {
                    serializationService.SerializeBy(_binarySerializator, Records, path);
                    _dialogService.MessageBox("Account records backup as binary was saved successfully.").ShowDialog();
                }
            });

            SaveToXMLCommand    = new RelayCommand(() =>
            {
                var path = _dialogService.SaveDialog(_xmlSerializator);
                if (path is not null)
                {
                    serializationService.SerializeBy(_xmlSerializator, Records, path);
                    _dialogService.MessageBox("Account records backup as xml was saved successfully.").ShowDialog();
                }
            });

            ImportBackupCommand = new RelayCommand(() =>
            {
                var path = _dialogService.OpenDialog();
                if (path is not null)
                {
                    _recordService.UpdateAllRecords(_serializationService.Deserialize(path));
                    UpdateRecords(_recordService.FilterRecords(SearchFilter));
                    _dialogService.MessageBox("Account records backup was successfully imported.").ShowDialog();
                }
            });

            DeleteAllDataCommand = new RelayCommand(() =>
            {
                if (_dialogService.MessageBox("Are you sure? All data will be deleted from this device!", "Warning", DialogButton.OK | DialogButton.Cancel).ShowDialog() ?? false)
                {
                    _recordService.ClearAllData();
                    UpdateRecords(_recordService.FilterRecords(SearchFilter));
                    _dialogService.MessageBox("All account records data successfully deleted from this device.").ShowDialog();
                }
            });

            CloseCommand = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(true)));
            FocusSearchInput = new RelayCommand(() => FocusRequested(this, new RequestFocusEventArgs(RequestFocusEventArgs.FOCUSED_SEARCH_TEXT)));
        }

        private void NewRecord_Removed(object? sender, RemovedEventArgs e)
        {
            if (sender is AccountRecordViewModel vm)
            {
                _recordService.DeleteRecord(vm.Label);
                RemoveRecord(vm);
            }
        }

        private void NewRecord_Edited(object? sender, EditedEventArgs e)
        {
            if (sender is AccountRecordViewModel vm)
            {
                _recordService.DeleteRecord(e.OldLabel);
                _recordService.AddRecord(vm);
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
            record.Edited  += NewRecord_Edited;
        }

        /// <summary>
        /// Removes record and unsubscribes from its remove event
        /// </summary>
        /// <param name="record">Record that will be added</param>
        private void RemoveRecord(AccountRecordViewModel record)
        {
            Records.Remove(record);
            record.Removed -= NewRecord_Removed;
            record.Edited  -= NewRecord_Edited;
        }

        private void UnsubscribeAll()
        {
            for (int i = 0; i < Records.Count; i++)
            {
                Records[i].Removed -= NewRecord_Removed;
                Records[i].Edited  -= NewRecord_Edited;
            }
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

        #region IRequestFocus Members

        /// <summary>
        /// Fires when some view requests for focus it
        /// </summary>
        public event EventHandler<RequestFocusEventArgs> FocusRequested = (_, _) => { };

        #endregion
    }
}
