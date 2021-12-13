using System.ComponentModel.DataAnnotations;

namespace MaterialDesignUnityBootStrap.DataModels
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
    }

}
