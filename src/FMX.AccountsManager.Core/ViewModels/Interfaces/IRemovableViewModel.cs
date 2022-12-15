using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// View model that can be removed
    /// </summary>
    public interface IRemovableViewModel
    {
        /// <summary>
        /// Removes this view model when executed
        /// </summary>
        ICommand RemoveCommand { get; }

        /// <summary>
        /// Fires when this view model is removed
        /// </summary>
        event EventHandler<RemovedEventArgs> Removed;
    }

    /// <summary>
    /// Event arguments for removed event
    /// </summary>
    public class RemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RemovedEventArgs()
        {

        }
    }
}
