using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignUnityBootStrap.DataModels;


namespace MaterialDesignUnityBootStrap.Services.Authentication
{
    public class UserManager 
    {
        private readonly IAuthenticationService _authenticationService;

        public UserManager(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _authenticationService.LoggedIn += (sender, args) => { _currentUser = args.User; };
        }

        private User _currentUser;

        public async Task<IEnumerable<string>> GetAllUserName()
        {
            return await _authenticationService.GetUserListAsync();
        } 

        public async Task LoginAsync(string userName, string password)
        {
            if (_currentUser != null)
                Logout();
            await _authenticationService.LoginAsync(userName, password);
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsLoggedIn => _currentUser != null;
        public string Name => _currentUser?.Name ?? string.Empty;
        public string LastName => _currentUser?.LastName ?? string.Empty;
        public string UserName => _currentUser?.UserName ?? string.Empty;

    }
}
