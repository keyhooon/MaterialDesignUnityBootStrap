using System.Windows;
using Prism.Commands;

namespace MaterialDesignUnityBootStrap.Infrastructure
{
    public static class WindowCommands
    {


        private static DelegateCommand<Window> _closeWindowCommand;
        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public static DelegateCommand<Window> CloseWindowCommand =>
            _closeWindowCommand ??= new DelegateCommand<Window>(window =>
            {
                window.Close();
            });

        private static DelegateCommand<Window> _maximizedWindowCommand;
        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public static DelegateCommand<Window> MaximizedWindowCommand =>
            _maximizedWindowCommand ??= new DelegateCommand<Window>(window =>
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            });

        private static DelegateCommand<Window> _minimizedWindowCommand;
        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public static DelegateCommand<Window> MinimizedWindowCommand =>
            _minimizedWindowCommand ??= new DelegateCommand<Window>(window =>
            {
                window.WindowState = WindowState.Minimized;
            });
    }
}
