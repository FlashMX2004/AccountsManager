using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Interaction logic for AddAccountRecordDialog.xaml.
    /// Also here is design logic for adding new fields
    /// </summary>
    public partial class AddAccountRecordDialog : DialogWindowBase<AddAccountRecordViewModel>
    {
        public AddAccountRecordDialog()
        {
            InitializeComponent();
            NewLabelText.Focus();

            ViewModelSet += vm =>
            {
                // Subscribe on new fields.
                //
                // When label of new field was changed,
                // then add new empty field
                vm.AccountRecord.Fields.CollectionChanged += Fields_CollectionChanged;

                // Clear last empty field when close
                vm.CloseRequested += ViewModel_CloseRequested;

                // Add first empty field
                vm.AddFieldCommand.Execute(new AccountRecordFieldViewModel());
            };
        }

        private void ViewModel_CloseRequested(object? sender, RequestCloseEventArgs e)
        {
            //Clean from empty fields
            ViewModel.RemoveFieldCommand.Execute(ViewModel.AccountRecord.Fields.Last());
        }

        private void Fields_CollectionChanged(object? sender,NotifyCollectionChangedEventArgs e)
        {
            // Subscribe when new field is added
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var field = (AccountRecordFieldViewModel)e.NewItems![0]!;

                // Add new field when label of last empty field was changed
                field.PropertyChanged += Field_PropertyChanged;
            }

            // Unsubscribe when field was removed
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var field = (AccountRecordFieldViewModel)e.OldItems![0]!;
                field.PropertyChanged -= Field_PropertyChanged;
            }
        }

        private void Field_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // If label was changed
            if (e.PropertyName == nameof(AccountRecordFieldViewModel.Label) &&
                sender is AccountRecordFieldViewModel field)
            {
                // Remove field if label and value are empty
                if (string.IsNullOrEmpty(field.Label) &&
                    string.IsNullOrEmpty(field.Value) &&
                    // We don't want to remove first empty field
                    ViewModel.AccountRecord.Fields.Count > 1)
                {
                    // Remove
                    ViewModel.RemoveFieldCommand.Execute(field);
                }
                
                // Add new empty field when empty label was changed
                if (ViewModel.AccountRecord.Fields.Last().Label != "")
                {
                    ViewModel.AddFieldCommand.Execute(new AccountRecordFieldViewModel());
                }
            }
        }
    }
}
