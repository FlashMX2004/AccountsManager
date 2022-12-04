using System;
using System.Windows;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Base class for all windows in WPF app
    /// </summary>
    /// <typeparam name="VM">View model type</typeparam>
    public class WindowBase<VM> : Window, IDisposable
        where VM : ViewModelBase, IRequestClose
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowBase()
        {
            // Correct window state
            Initialized += (_, e) =>
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
            }
        }

        /// <summary>
        /// Fires when view model requests close
        /// </summary>
        protected virtual void OnCloseRequested(object? sender, RequestCloseEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Update window position for design
        /// </summary>
        private void SetWindowPosition()
        {
            var screen = WpfScreen.GetScreenFrom(this);

            // TODO: I don't know why i need to add these 8px for correct view...
            Top = screen.WorkingArea.Bottom - Height + 8;
            Left = screen.WorkingArea.Right / 2 - Width / 2;
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
