
using CompositeContentNavigator.Services;
using CompositeContentNavigator.Services.MapItems;
using CompositeContentNavigator.Services.MapItems.Data;
using Demo.Views;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            Container.Resolve<IRegionManager>().RegisterViewWithRegion("Tools", typeof(ToolView));
            Container.Resolve<IRegionManager>().RegisterViewWithRegion("ToolBar", typeof(ToolBarView));

            return base.CreateShell();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var compositeMapNavigatorService = Container.Resolve<CompositeMapNavigatorService>();

            compositeMapNavigatorService.RegisterItem("Cardio", MapItemBuilder.CreateDefaultBuilder("Cardio").WithImagePackIcon(PackIconKind.Heart).WithChild(new Collection<MapItem> {
                    compositeMapNavigatorService.RegisterItem("CardioSignal",MapItemBuilder.CreateDefaultBuilder("Signal").WithToolbars(new[]{ typeof(ToolBarView)}).WithView(typeof(Content1View)).WithImagePackIcon(PackIconKind.Signal)),
                    compositeMapNavigatorService.RegisterItem("CardioAnalysis",MapItemBuilder.CreateDefaultBuilder("Analysis").WithView(typeof(Content2View)).WithImagePackIcon(PackIconKind.Analog))
                }));
        }
    }
    
}
