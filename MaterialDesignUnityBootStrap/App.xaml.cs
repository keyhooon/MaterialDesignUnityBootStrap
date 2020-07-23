using Prism.Ioc;
using MaterialDesignUnityBootStrap.Views;
using System.Windows;
using WpfInfrastructure.RegionAdapter;
using System.Windows.Controls;
using CommonServiceLocator;
using Prism.Regions;
using System.IO;
using Microsoft.Extensions.Configuration;
using Prism.Modularity;
using Prism.Unity.Ioc;
using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.ViewModels;
using Unity;

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

            return ServiceLocator.Current.TryResolve<MainWindow>();
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings?.RegisterMapping(typeof(ToolBarTray), Container.Resolve<ToolbarRegionAdapter>());
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry
                .Register<MainWindow>()
                .RegisterInstance(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppConfig.json", optional: true, true).Build());

            containerRegistry.RegisterDialog<PaletteSelector, PaletteSelectorViewModel>(typeof(PaletteSelector).FullName);
            containerRegistry.RegisterDialogWindow<DialogWindow>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<CompositeContentNavigator.ContentNavigatorModule>();

        }
        private void LoadTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = (Theme)paletteHelper.GetTheme();
            theme.SetBaseTheme(Settings.Default.IsDark ? Theme.Dark : Theme.Light);
            if(Settings.Default.PrimaryColor.HasValue)
                theme.SetPrimaryColor(Settings.Default.PrimaryColor.Value);
            if (Settings.Default.SecondaryColor.HasValue)
                theme.SetSecondaryColor(Settings.Default.SecondaryColor.Value);
            paletteHelper.SetTheme(theme);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();
            base.OnExit(e);
        }
    }
}
