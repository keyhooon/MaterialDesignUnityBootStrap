using System;
using MaterialDesignUnityBootStrap.Config;
using Microsoft.Extensions.Options;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class AboutViewModel : BindableBase, IDialogAware
    {
        public AboutViewModel(IOptions<MainWindowOptions> mainWindowOptions)
        {
            Header = $"{mainWindowOptions.Value.Name} Application";
        }

        public string Header { get; }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public string Title => "About";
        public event Action<IDialogResult> RequestClose;


    }
}
