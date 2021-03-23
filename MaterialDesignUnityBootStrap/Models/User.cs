using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignUnityBootStrap.Models
{
    public class User
    {
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
