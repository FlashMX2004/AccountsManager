using System;
using System.Collections.Generic;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace FMX.PasswordManager
{
    /// <summary>
    /// WPF wrapper for winforms screen
    /// </summary>
    public class WpfScreen
    {
        #region Static Members

        /// <summary>
        /// Return all screens that user uses
        /// </summary>
        public static IEnumerable<WpfScreen> AllScreens() => Screen.AllScreens.Select(s => new WpfScreen(s));

        /// <summary>
        /// Return screen, where you can see <paramref name="window"/>
        /// </summary>
        /// <param name="window">Window in screen</param>
        public static WpfScreen GetScreenFrom(Window window)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
            Screen screen = Screen.FromHandle(windowInteropHelper.Handle);
            WpfScreen wpfScreen = new (screen);
            return wpfScreen;
        }

        /// <summary>
        /// Return screen, where you can see <paramref name="point"/> in window
        /// </summary>
        /// <param name="point">Point in window, that screen shows</param>
        public static WpfScreen GetScreenFrom(System.Windows.Point point)
        {
            int x = (int)Math.Round(point.X);
            int y = (int)Math.Round(point.Y);

            // are x,y device-independent-pixels ??
            System.Drawing.Point drawingPoint = new (x, y);
            Screen screen = Screen.FromPoint(drawingPoint);
            WpfScreen wpfScreen = new (screen);

            return wpfScreen;
        }

        public static WpfScreen Primary
        {
            get { return new WpfScreen(Screen.PrimaryScreen); }
        }

        private static Rect GetRect(Rectangle value) => new()
        {
            X = value.X,
            Y = value.Y,
            Width = value.Width,
            Height = value.Height
        };

        #endregion

        /// <summary>
        /// Incapsulated winforms screen
        /// </summary>
        private readonly Screen screen;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="screen">Screen</param>
        internal WpfScreen(Screen screen)
        {
            this.screen = screen;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Bounds with taskbar
        /// </summary>
        public Rect DeviceBounds => GetRect(screen.Bounds);

        /// <summary>
        /// Bounds without taskbar
        /// </summary>
        public Rect WorkingArea => GetRect(screen.WorkingArea);

        /// <summary>
        /// Gets a value indicating whether a particular display is the primary device.
        /// </summary>
        public bool IsPrimary => screen.Primary;

        /// <summary>
        /// Gets the device name associated with a display.
        /// </summary>
        public string DeviceName => screen.DeviceName;

        #endregion
    }
}
