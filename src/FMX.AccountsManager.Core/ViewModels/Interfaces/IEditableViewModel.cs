using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// View model that can be edited
    /// </summary>
    public interface IEditableViewModel
    {
        /// <summary>
        /// Edits this view model when executed
        /// </summary>
        ICommand EditCommand { get; }

        /// <summary>
        /// Fires when this view model is edited
        /// </summary>
        event EventHandler<EditedEventArgs> Edited;
    }

    /// <summary>
    /// Event arguments for edited event
    /// </summary>
    public class EditedEventArgs : EventArgs
    {
        public string OldLabel { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditedEventArgs(string oldLabel)
        {
            OldLabel = oldLabel;
        }
    }
}
