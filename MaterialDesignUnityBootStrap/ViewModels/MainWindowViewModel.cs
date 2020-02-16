using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using Health_Monitoring.Views;
using WpfInfrastructure.Service.CompositeMapNavigator;

namespace Health_Monitoring.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly CompositeMapNavigatorService _compositeMapNavigatorService;


        public MainWindowViewModel(IRegionManager regionManager, CompositeMapNavigatorService compositeMapNavigatorService)
        {
            _regionManager = regionManager;
            _compositeMapNavigatorService = compositeMapNavigatorService;


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
