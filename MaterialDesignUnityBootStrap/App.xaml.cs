using Prism.Ioc;
using MaterialDesignUnityBootStrap.Views;
using System.Windows;
using CompositeContentNavigatorServiceModule.Views;
using WpfInfrastructure.RegionAdapter;
using System.Windows.Controls;
using CommonServiceLocator;
using CompositeContentNavigatorServiceModule.Services;
using Prism.Regions;
using System.IO;
using Microsoft.Extensions.Configuration;
using Prism.Modularity;
using Prism.Unity.Ioc;
using System.Collections.Generic;
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
            Container.Resolve<IRegionViewRegistry>().RegisterViewWithRegion("Header", typeof(ActiveViewCollectionView));
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
                .Register<IContainerRegistry, UnityContainerExtension>()
                .RegisterInstance(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppConfig.json", optional: true).Build())
                ;
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            var compositeContentNavigatorServiceModuleType = typeof(CompositeContentNavigatorServiceModule.CompositeContentNavigatorServiceModule);
            var compositeContentNavigatorServiceModuleInfo = new ModuleInfo()
            {
                ModuleName = compositeContentNavigatorServiceModuleType.Name,
                ModuleType = compositeContentNavigatorServiceModuleType.AssemblyQualifiedName,
            };

            var HierarchicalContentNavigatorModuleType = typeof(HierarchicalContentNavigatorModule.HierarchicalContentNavigatorModule);
            var HierarchicalContentNavigatorModuleInfo = new ModuleInfo()
            {
                ModuleName = HierarchicalContentNavigatorModuleType.Name,
                ModuleType = HierarchicalContentNavigatorModuleType.AssemblyQualifiedName,
                DependsOn = new Collection<string>(new string[] { compositeContentNavigatorServiceModuleInfo.ModuleName }) 
            };

            moduleCatalog.AddModule(compositeContentNavigatorServiceModuleInfo);
            moduleCatalog.AddModule(HierarchicalContentNavigatorModuleInfo);

        }
    }
}
