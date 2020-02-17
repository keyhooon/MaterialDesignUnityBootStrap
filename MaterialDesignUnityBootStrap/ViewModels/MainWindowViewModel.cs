using CompositeContentNavigatorServiceModule.Services;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Views;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly MainWindowConfig _mainWindowConfig;
        public MainWindowViewModel(IConfigurationRoot configurationRoot)
        {
            var section = configurationRoot.GetSection("MainWindowConfig");
            if (section.Exists())
                _mainWindowConfig = ConfigurationBinder.Get<MainWindowConfig>(section);
        }


  
        public string Header
        {
            get { return _mainWindowConfig.Name; }
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
