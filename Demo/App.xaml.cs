
using System;
using System.Collections.Generic;
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

            return base.CreateShell();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var compositeMapNavigatorService = Container.Resolve<CompositeMapNavigatorService>();


            compositeMapNavigatorService.RegisterItem("Cardio", MapItemBuilder.
                CreateDefaultBuilder("Cardio").
                WithImagePackIcon(PackIconKind.Heart).
                WithChild(new Collection<MapItem> {
                    compositeMapNavigatorService.RegisterItem("CardioSignal",MapItemBuilder.
                        CreateDefaultBuilder("Signal").
                        WithToolBars(new[]{ typeof(ToolBarView)}).
                        WithView(typeof(Content1View)).
                        WithImagePackIcon(PackIconKind.Signal)),
                    compositeMapNavigatorService.RegisterItem("CardioAnalysis",MapItemBuilder.
                        CreateDefaultBuilder("Analysis").
                        WithImagePackIcon(PackIconKind.Analog).
                        WithExtraView(new Dictionary<string, IEnumerable<Type>> {{"PopupToolBarRegion", new[] {typeof(ToolView)}}, { "ToolsRegion", new[] {typeof(Tool2View)}}}).
                        WithView(typeof(Content2View)))
                }));
        }
    }
    
}
