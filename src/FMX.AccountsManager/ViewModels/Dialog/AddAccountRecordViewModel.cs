using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// View model for add account record dialog
    /// </summary>
    public class AddAccountRecordViewModel : DialogViewModelBase
    {
        /// <summary>
        /// Record that will be added
        /// </summary>
        public AccountRecordViewModel AccountRecord { get; set; } = new AccountRecordViewModel();

        #region Commands

        /// <summary>
        /// Adds field to <see cref="AccountRecord"/> when executed
        /// </summary>
        public ICommand AddFieldCommand { get; set; }

        /// <summary>
        /// Removes field from <see cref="AccountRecord"/> when executed
        /// </summary>
        public ICommand RemoveFieldCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
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

        #endregion
    }
}
