using System.Net.Sockets;
using Models;

namespace Business.Interfaces
{
    public interface IUserManager
    {
        User GetUser();
        User GetUserByName(string userName);
        User LoginUser(string userName, string password);
    }
}