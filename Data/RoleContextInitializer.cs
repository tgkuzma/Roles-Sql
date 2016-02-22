using System.Collections.Generic;
using System.Data.Entity;
using Models;

namespace Data
{
    internal class RoleContextInitializer : DropCreateDatabaseAlways<RoleContext>
    {
        protected override void Seed(RoleContext context)
        {
            context.Users.Add(new User
            {
                UserName = "testuser",
                //Password is SilveCloud
                Password = "1000:LL5k/1CznkWNdLo7dOM74TmEbD1rIdRC:J4cyH9BjIvil2F+Pq5tk22SuT6TQtPA/",
                Roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Admin"
                    }
                }
            });

            context.Roles.Add(new Role
            {
                Name = "Manager"
            });

            context.SaveChanges();
        }
    }
}