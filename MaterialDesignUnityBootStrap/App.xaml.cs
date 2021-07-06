    using System;
using Prism.Ioc;
using MaterialDesignUnityBootStrap.Views;
using System.Windows;
using System.Windows.Controls;

using Prism.Regions;
using System.IO;
using Microsoft.Extensions.Configuration;
using Prism.Modularity;
using CompositeContentNavigator.Infrastructure;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.ViewModels;
using Unity;
using CommonServiceLocator;
    using CompositeContentNavigator;
    using MaterialDesignThemes.Wpf.Transitions;
    using MaterialDesignUnityBootStrap.Config;
    using MaterialDesignUnityBootStrap.Services.Logging;
    using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
    using Prism.Unity;
using Prism.Microsoft.DependencyInjection;

namespace MaterialDesignUnityBootStrap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {

            LoadTheme();
            var mainWindow = ((UnityContainerExtension) Container).Resolve<MainWindow>();

            Container.Resolve<IRegionManager>().RegisterViewWithRegion("PopupToolBarRegion", typeof(LogView));
            return mainWindow;
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings?.RegisterMapping(typeof(ToolBarTray), Container.Resolve<ToolBarTrayRegionAdapter>());


        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppConfig.json", optional: true, true).Build();

            containerRegistry
                .Register<MainWindow>()
                .RegisterInstance(configurationRoot)
                .RegisterServices(s =>
                {
                    s.AddOptions<ContentNavigatorOptions>()
                        .Bind(configurationRoot.GetSection(nameof(ContentNavigatorOptions))).ValidateDataAnnotations();
                    ; s.AddOptions<MainWindowOptions>()
                        .Bind(configurationRoot.GetSection(nameof(MainWindowOptions))).ValidateDataAnnotations();
                    ;
                  
                    s.AddLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddProvider(Container.Resolve<PubSubEventLoggerProvider>());
                        logging.AddConfiguration(configurationRoot);
                    });
                });
            containerRegistry.RegisterDialog<PaletteSelector, PaletteSelectorViewModel>(typeof(PaletteSelector).FullName);
            containerRegistry.RegisterDialogWindow<DialogWindow>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<ContentNavigatorModule>();

        }
        private void LoadTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = (Theme)paletteHelper.GetTheme();
            var themeSettings = ThemeSettings.Default;
            theme.SetBaseTheme(themeSettings.IsDark ? Theme.Dark : Theme.Light);
            if(themeSettings.PrimaryColor.HasValue)
                theme.SetPrimaryColor(themeSettings.PrimaryColor.Value);
            if (themeSettings.SecondaryColor.HasValue)
                theme.SetSecondaryColor(themeSettings.SecondaryColor.Value);
            paletteHelper.SetTheme(theme);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            ThemeSettings.Default.Save();
            base.OnExit(e);
        }
        
    }
}
