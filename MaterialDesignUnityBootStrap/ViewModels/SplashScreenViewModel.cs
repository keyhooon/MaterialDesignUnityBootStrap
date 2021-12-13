using System;
using System.Windows;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class SplashScreenViewModel: BindableBase, IDialogAware
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = "Initialize...\r\n";
            var splashReport = parameters.GetValue<Progress<string>>("report");
            splashReport.ProgressChanged += (sender, s) =>
            {
                if (s == string.Empty)
                    RequestClose(new DialogResult(ButtonResult.OK));
                else
                    Message += s;
            };
        }

        public string Title => nameof(SplashScreen);
        public event Action<IDialogResult> RequestClose;
    }
}
