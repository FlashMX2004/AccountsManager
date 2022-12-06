using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    public class MainViewModel : ViewModelBase, IRequestClose
    {
        /// <summary>
        /// Search filter value
        /// </summary>
        public string SearchFilter { get; set; } = "";

        /// <summary>
        /// Account records collection
        /// </summary>
        public ObservableCollection<AccountRecordViewModel> Records { get; set; }

        /// <summary>
        /// Adds account record when executed
        /// </summary>
        public ICommand AddAccountRecordCommand { get; set; }

        /// <summary>
        /// Closes application when executed
        /// </summary>
        public ICommand CloseCommand { get; set; }


        public event EventHandler<RequestCloseEventArgs> CloseRequested = (_, _) => { };

        public MainViewModel(IRecordService recordService, IDialogService dialogService)
        {
            Records = new(recordService.GetAllRecords());
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
                    Records.Add(newRecord);
                }
            });

            CloseCommand = new RelayCommand(() => CloseRequested(this, new RequestCloseEventArgs(true)));
        }

    }
}
