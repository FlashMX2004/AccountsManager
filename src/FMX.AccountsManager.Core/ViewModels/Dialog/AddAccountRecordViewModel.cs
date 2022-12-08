using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// View model for add account record dialog
    /// </summary>
    public class AddAccountRecordViewModel : DialogViewModelBase
    {
        /// <summary>
        /// Record that will be added
        /// </summary>
        public AccountRecordViewModel AccountRecord { get; set; }

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
        public AddAccountRecordViewModel(AccountRecordViewModel recordViewModel)
        {
            AccountRecord = recordViewModel;

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
