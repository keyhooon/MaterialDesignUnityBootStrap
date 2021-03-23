using System;
using System.Windows;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Views;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly MainWindowConfig _mainWindowConfig;
        private readonly CompositeContentNavigator.ModuleConfig _compositeContentNavigatorConfig;
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IConfigurationRoot configurationRoot,IDialogService dialogService)
        {
            var section = configurationRoot.GetSection(MainWindowConfig.SectionName);
            _mainWindowConfig = section.Exists() ? section.Get<MainWindowConfig>() : new MainWindowConfig();

            section = configurationRoot.GetSection(CompositeContentNavigator.ModuleConfig.SectionName);
            _compositeContentNavigatorConfig = section.Exists() ? section.Get<CompositeContentNavigator.ModuleConfig>() : new CompositeContentNavigator.ModuleConfig();
            
            this.dialogService = dialogService;

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background,Dispatcher.CurrentDispatcher);
            timer.Tick += (sender, args) => RaisePropertyChanged(nameof(DateTimeNow));
            timer.Start();
        }


        public DateTime DateTimeNow => DateTime.Now;

        public string Header => _mainWindowConfig.Name;

        public Visibility PaletteSelectorVisibility =>
            _mainWindowConfig.PaletteSelectorVisibility ? Visibility.Visible : Visibility.Collapsed;
        public Visibility NavigationButtonVisibility =>
            _mainWindowConfig.NavigationButtonVisibility ? Visibility.Visible : Visibility.Collapsed;

        public string ToolbarRegionName => _compositeContentNavigatorConfig.ToolbarRegionName;

        public string ContentRegionName => _compositeContentNavigatorConfig.ContentRegionName;

        public string PopupToolBarRegionName => _mainWindowConfig.PopupToolBarRegionName;
        public string ContentMapRegionName => _compositeContentNavigatorConfig.ContentMapRegionName;

        public string HeaderRegionName => _compositeContentNavigatorConfig.HeaderRegionName;

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
                        dialogService.ShowDialog(typeof(PaletteSelector).FullName,new DialogParameters("title=Themes"),(o)=> { });
                    });
            }
        }


    }
}
