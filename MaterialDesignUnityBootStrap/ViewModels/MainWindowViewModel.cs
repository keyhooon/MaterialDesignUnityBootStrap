using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Views;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly MainWindowConfig _mainWindowConfig;
        private readonly CompositeContentNavigator.ModuleConfig _compositeContentNavigatorConfig;

        public MainWindowViewModel(IConfigurationRoot configurationRoot)
        {
            var section = configurationRoot.GetSection(MainWindowConfig.SectionName);
            if (section.Exists())
                _mainWindowConfig = ConfigurationBinder.Get<MainWindowConfig>(section);
            else
                _mainWindowConfig = new MainWindowConfig();

            section = configurationRoot.GetSection(CompositeContentNavigator.ModuleConfig.SectionName);
            if (section.Exists())
                _compositeContentNavigatorConfig = ConfigurationBinder.Get<CompositeContentNavigator.ModuleConfig>(section);
            else
                _compositeContentNavigatorConfig = new CompositeContentNavigator.ModuleConfig();
        }


  
        public string Header
        {
            get { return _mainWindowConfig.Name; }
        }

        public string ToolbarRegionName
        {
            get { return _compositeContentNavigatorConfig.ToolbarRegionName; }
        }

        public string ContentRegionName
        {
            get { return _compositeContentNavigatorConfig.ContentRegionName; }
        }

        public string ContentMapRegionName
        {
            get { return _compositeContentNavigatorConfig.ContentMapRegionName; }
        }

        public string HeaderRegionName
        {
            get { return _compositeContentNavigatorConfig.HeaderRegionName; }
        }

        private DelegateCommand _paletteSelectorShowCommand;


        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public DelegateCommand PaletteSelectorShowCommand
        {
            get
            {
                return _paletteSelectorShowCommand ??= new DelegateCommand(
                    async () =>
                    {
                        var view = new PaletteSelector();


                        //show the dialog
                        var result = await DialogHost.Show(view);
                    });
            }
        }


    }
}
