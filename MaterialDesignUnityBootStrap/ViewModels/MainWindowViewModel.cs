using System;
using System.Windows;
using System.Windows.Threading;
using CompositeContentNavigator;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IOptions<ContentNavigatorOption> _compositeContentNavigatorOptions;
        private readonly IDialogService _dialogService;
        private readonly IOptions<MainWindowOptions> _mainWindowOptions;

        public MainWindowViewModel(IDialogService dialogService, IOptions<ContentNavigatorOption> compositeContentNavigatorOptions, IOptions<MainWindowOptions> mainWindowOptions)
        {
            _compositeContentNavigatorOptions = compositeContentNavigatorOptions;
            _dialogService = dialogService;
            _mainWindowOptions = mainWindowOptions;

            var timer = new DispatcherTimer(DispatcherPriority.Background,Dispatcher.CurrentDispatcher);
            timer.Tick += (sender, args) => RaisePropertyChanged(nameof(DateTimeNow));
            timer.Start();
        }


        public DateTime DateTimeNow => DateTime.Now;

        public string Header => _mainWindowOptions.Value.Name;

        public Visibility PaletteSelectorVisibility =>
            _mainWindowOptions.Value.PaletteSelectorVisibility ? Visibility.Visible : Visibility.Collapsed;
        public Visibility NavigationButtonVisibility =>
            _mainWindowOptions.Value.NavigationButtonVisibility ? Visibility.Visible : Visibility.Collapsed;

        public string ToolbarRegionName => _compositeContentNavigatorOptions.Value.ToolbarRegionName;
        public string ToolsRegionName => _mainWindowOptions.Value.ToolsRegionName;

        public string ContentRegionName => _compositeContentNavigatorOptions.Value.ContentRegionName;

        public string PopupToolBarRegionName => _mainWindowOptions.Value.PopupToolBarRegionName;
        public string ContentMapRegionName => _compositeContentNavigatorOptions.Value.ContentMapRegionName;

        public string HeaderRegionName => _compositeContentNavigatorOptions.Value.HeaderRegionName;

        private DelegateCommand _paletteSelectorShowCommand;


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
                        var view = new PaletteSelector();

                        //var result = await DialogHost.Show(view);
                        //show the dialog
                        _dialogService.ShowDialog(typeof(PaletteSelector).FullName,new DialogParameters("title=Themes"),(o)=> { });
                    });
            }
        }


    }
}
