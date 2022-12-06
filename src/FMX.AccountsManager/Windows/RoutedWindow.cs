using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Layer between <see cref="Window"/> and <see cref="WindowBase{VM}"/>
    /// for routed events for animation
    /// </summary>
    public class RoutedWindow : Window
    {
        /// <summary>
        /// Window closing duration in milliseconds
        /// </summary>
        public const int CLOSING_DURATION = 500;

        /// <summary>
        /// Routed event fired when window starts to close
        /// </summary>
        public static readonly RoutedEvent RoutedClosingEvent;

        /// <summary>
        /// Static constructor for registering routed events
        /// </summary>
        static RoutedWindow()
        {
            RoutedClosingEvent = EventManager.RegisterRoutedEvent(nameof(RoutedClosing),
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RoutedWindow));
        }

        /// <summary>
        /// Routed event for closing window instance
        /// </summary>
        public event RoutedEventHandler RoutedClosing
        {
            add => base.AddHandler(RoutedClosingEvent, value);
            remove => base.AddHandler(RoutedClosingEvent, value);
        }

        /// <summary>
        /// Close window asynchronously for <see cref="CLOSING_DURATION"/> duration
        /// </summary>
        public async Task CloseAsync()
        {
            Dispatcher.Invoke(() => RaiseEvent(new RoutedEventArgs(RoutedClosingEvent)));
            await Task.Delay(CLOSING_DURATION);
            Dispatcher.Invoke(() => Close());
        }

        public void FocusGrid(object? sender, MouseButtonEventArgs args)
        {
            ((Grid)sender!).Focus();
        }
    }
}
