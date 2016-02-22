using System.Data.Entity;
using Models;

namespace Data
{
    public class RoleContext : DbContext
    {
        public RoleContext() : base("RoleDbContext")
        {
            Database.SetInitializer(new RoleContextInitializer());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
