using System.ComponentModel;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// A base view model that fires Property Changed events when it's neccessary
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Fires PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
