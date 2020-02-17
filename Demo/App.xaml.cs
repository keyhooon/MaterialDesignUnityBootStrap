using CompositeContentNavigatorServiceModule.Services;
using CompositeContentNavigatorServiceModule.Services.MapItems;
using CompositeContentNavigatorServiceModule.Services.MapItems.Data;
using Demo.Views;
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

            compositeMapNavigatorService.RegisterItem("Cardio", MapItemBuilder.CreateDefaultBuilder("Cardio").WithChild(new Collection<MapItem> {
                    compositeMapNavigatorService.RegisterItem("CardioSignal",MapItemBuilder.CreateDefaultBuilder("Signal").WithView(typeof(Content1View))),
                    compositeMapNavigatorService.RegisterItem("CardioAnalysis",MapItemBuilder.CreateDefaultBuilder("Analysis").WithView(typeof(Content2View))),
                }));
        }
    }
    
}
