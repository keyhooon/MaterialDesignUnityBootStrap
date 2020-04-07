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

namespace MaterialDesignUnityBootStrap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
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
                    .AddJsonFile("CompositeContentNavigatorConfig.json", optional: true, true).Build());
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<CompositeContentNavigator.ContentNavigatorModule>();

        }
    }
}
