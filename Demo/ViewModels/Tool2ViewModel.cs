using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;

namespace Demo.ViewModels
{
    public class Tool2ViewModel: BindableBase, INavigationAware
    {
        private bool _loaded;



        public Tool2ViewModel()
        {
            Loaded = false;

        }

        public bool Loaded
        {
            get => _loaded;
            set => SetProperty(ref _loaded, value);
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Task.Delay(2000);
            Loaded = true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
