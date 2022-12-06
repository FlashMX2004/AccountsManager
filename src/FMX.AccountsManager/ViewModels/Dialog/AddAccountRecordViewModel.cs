using System.Windows.Input;

namespace FMX.AccountsManager
{
    public class AddAccountRecordViewModel : DialogViewModelBase
    {
        public AccountRecordViewModel AccountRecord { get; set; } = new AccountRecordViewModel();

        public ICommand AddFieldCommand { get; set; }
        public ICommand RemoveFieldCommand { get; set; }

        public AddAccountRecordViewModel()
        {
            AddFieldCommand = new RelayParameterizedCommand(o =>
            {
                if (o is AccountRecordFieldViewModel vm) AccountRecord.Fields.Add(vm);
            });

            RemoveFieldCommand = new RelayParameterizedCommand(o =>
            {
                if (o is AccountRecordFieldViewModel vm) AccountRecord.Fields.Remove(vm);
            });
        }
    }
}
