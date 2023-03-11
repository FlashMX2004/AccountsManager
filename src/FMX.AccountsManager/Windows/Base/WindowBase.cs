using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Base class for all windows in WPF app
    /// </summary>
    /// <typeparam name="VM">View model type</typeparam>
    public class WindowBase<VM> : RoutedWindow, IDisposable
        where VM : ViewModelBase, IRequestClose
    {
        /// <summary>
        /// Fires when <see cref="ViewModel"/> is not null (is set)
        /// </summary>
        protected event Action<VM> ViewModelSet = vm => { };

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowBase()
        {
            // Correct window state
            Loaded += (_, e) =>
            {
                SetWindowPosition();
            };

            // Release unused resources
            Closed += (_, _) => Dispose();
        }

        /// <summary>
        /// View model for the WPF window view
        /// </summary>
        public required VM ViewModel
        {
            get => (VM)DataContext;
            set 
            {
                // Unsubscribe if view model is changed
                if (DataContext is not null) ((VM)DataContext).CloseRequested -= OnCloseRequested;
                
                // Set view model for this window
                DataContext = value;

                // Subscribe on close request of view model
                value.CloseRequested += OnCloseRequested;

                // Fire event for not null view model
                ViewModelSet(value);
            }
        }

        /// <summary>
        /// Fires when view model requests close
        /// </summary>
        protected virtual void OnCloseRequested(object? sender, RequestCloseEventArgs e)
        {
            // Unable this window while closing
            IsEnabled = false;

            // Start closing
           Task.Run(base.CloseAsync);
        }

        /// <summary>
        /// Update window position for design
        /// </summary>
        private void SetWindowPosition()
        {
            Left = System.Windows.SystemParameters.WorkArea.Right / 2 - Width/2;
            Top = System.Windows.SystemParameters.WorkArea.Bottom - Height;// - (screen.DeviceBounds.Bottom - screen.WorkingArea.Bottom);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ViewModel.CloseRequested -= OnCloseRequested;
        }
    }
}
