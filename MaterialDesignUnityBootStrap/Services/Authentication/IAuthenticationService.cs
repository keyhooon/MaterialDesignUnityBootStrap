using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignUnityBootStrap.DataModels;

namespace MaterialDesignUnityBootStrap.Services.Authentication
{
    public  class LoggedInEventArgs : EventArgs
    {
        public User User { get;  }

        public LoggedInEventArgs(User user)
        {
            User = user;
        }
    }
    public interface IAuthenticationService
    {
        event EventHandler<LoggedInEventArgs> LoggedIn;
        Task LoginAsync(string userName, string password);
        Task<IEnumerable<string>> GetUserListAsync();
        Task<(string name, string lastName)> GetUserInfoAsync(string userName);
    }
}
