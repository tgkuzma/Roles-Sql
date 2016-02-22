using System.Linq;
using Models;
using Models.Interfaces;

namespace Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly RoleContext _context;

        public UserRepository(RoleContext context) : base(context)
        {
            _context = context;
        }

        public User GetFirstUser()
        {
            return _context.Users.Include("Roles").FirstOrDefault();
        }
    }
}