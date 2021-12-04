using System;
using System.Windows;
using System.Windows.Threading;
using CompositeContentNavigator;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Views;
using Microsoft.Extensions.Options;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IOptions<ContentNavigatorOptions> _ContentNavigatorOptions;
        private readonly IDialogService _dialogService;
        private readonly IOptions<MainWindowOptions> _mainWindowOptions;

        public MainWindowViewModel(IDialogService dialogService, IOptions<ContentNavigatorOptions> contentNavigatorOptions, IOptions<MainWindowOptions> mainWindowOptions)
        {
            _mainWindowOptions = mainWindowOptions;
            _ContentNavigatorOptions = contentNavigatorOptions;
            _dialogService = dialogService;

            ToolbarRegionName = _ContentNavigatorOptions.Value.ToolbarRegionName;
            Header = _mainWindowOptions.Value.Name;
            PaletteSelectorVisibility = _mainWindowOptions.Value.PaletteSelectorVisibility ? Visibility.Visible : Visibility.Collapsed;
            NavigationButtonVisibility = _mainWindowOptions.Value.NavigationButtonVisibility ? Visibility.Visible : Visibility.Collapsed;
            ToolsRegionName = _mainWindowOptions.Value.ToolsRegionName;
            ContentRegionName = _ContentNavigatorOptions.Value.ContentRegionName;
            PopupToolBarRegionName = _mainWindowOptions.Value.PopupToolBarRegionName;
            ContentMapRegionName = _ContentNavigatorOptions.Value.ContentMapRegionName;
            HeaderRegionName = _ContentNavigatorOptions.Value.HeaderRegionName;
            var timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Background, (_, _) => RaisePropertyChanged(nameof(DateTimeNow)), Dispatcher.CurrentDispatcher);

        }


        public DateTime DateTimeNow => DateTime.Now;

        public string Header { get; }

        public Visibility PaletteSelectorVisibility { get; }

        public Visibility NavigationButtonVisibility { get; }

        public string ToolbarRegionName { get; }

        public string ToolsRegionName { get; }

        public string ContentRegionName { get; }

        public string PopupToolBarRegionName { get; }

        public string ContentMapRegionName { get; }

        public string HeaderRegionName { get; }

        private DelegateCommand _paletteSelectorShowCommand;
        private DelegateCommand _loginShowCommand;
        private DelegateCommand _aboutShowCommand;


        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public DelegateCommand PaletteSelectorShowCommand
        {
            get
            {
                return _paletteSelectorShowCommand ??= new DelegateCommand(
                    () =>
                    {
                        _dialogService.Show(typeof(PaletteSelector).FullName,new DialogParameters(),(o)=> { });
                    });
            }
        }

        public DelegateCommand LoginShowCommand
        {
            get
            {
                return _loginShowCommand ??= new DelegateCommand(
                    () =>
                    {
                        _dialogService.Show(typeof(LoginView).FullName, new DialogParameters(), (o) => { });
                    });
            }
        }

        public DelegateCommand AboutShowCommand
        {
            get
            {
                return _aboutShowCommand ??= new DelegateCommand(
                    () =>
                    {
                        _dialogService.Show(typeof(AboutView).FullName, new DialogParameters(), (o) => { });
                    });
            }
        }


    }
}
