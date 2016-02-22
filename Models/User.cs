using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "User Name:")]
        public string UserName { get; set; }

        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }
}
