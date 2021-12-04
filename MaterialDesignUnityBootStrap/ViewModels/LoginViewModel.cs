using MaterialDesignUnityBootStrap.Services.Authentication;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly UserManager _userManager;
        private string _userName;
        private string _password;
        private DelegateCommand _loginCommand;
        private string _message;
        private DelegateCommand _cancelCommand;
        private IEnumerable<string> _userNameList;

        public LoginViewModel(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<string> UserNameList
        {
            get => _userNameList;
            private set => SetProperty(ref _userNameList, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }


        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand LoginCommand =>
            _loginCommand ??= new DelegateCommand(
                async () =>
                {
                    try
                    {
                        await _userManager.LoginAsync(UserName, Password);
                        if (_userManager.IsLoggedIn)
                            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                    }
                    catch (Exception e)
                    {
                        Message = e.Message;
                    }
                });

        public DelegateCommand CancelCommand =>
            _cancelCommand ??= new DelegateCommand(
                () =>
                {
                    RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
                    Application.Current.Shutdown();
                });

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            UserNameList = await _userManager.GetAllUserName();
            UserName = UserNameList.FirstOrDefault();
        }

        public string Title => "Authentication";
        public event Action<IDialogResult> RequestClose;
    }
}
