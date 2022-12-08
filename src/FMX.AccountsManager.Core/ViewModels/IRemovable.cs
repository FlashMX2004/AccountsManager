using System;
using System.Windows.Input;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Object that can be removed
    /// </summary>
    public interface IRemovable
    {
        /// <summary>
        /// Removes this object when executed
        /// </summary>
        ICommand RemoveCommand { get; }

        /// <summary>
        /// Fires when this object is removed
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
