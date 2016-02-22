using System;
using Business.Interfaces;
using Models;
using Models.Interfaces;

namespace Business
{
    public class UserManager : IUserManager  
    {
        private readonly IUserRepository _useRepository;
        private readonly IHashManager _hashManager;

        public UserManager(IUserRepository useRepository, IHashManager hashManager)
        {
            _useRepository = useRepository;
            _hashManager = hashManager;
        }

        public User GetUser()
        {
            return _useRepository.GetFirstUser();
        }

        public User GetUserByName(string userName)
        {
            //Obviously this should be implemented differently
            return _useRepository.GetFirstUser();
        }

        public User LoginUser(string userName, string password)
        {
            User user = null;
            try
            {
                user = GetValidatedUser(userName, password);
            }
            catch (Exception)
            {
                //this is where we might do something if "GetValidatedUser" threw an exception
            }

            return user;
        }

        private User GetValidatedUser(string userName, string password)
        {
            //Obviously we need a different implementation here
            //We're assumning that we're logging in correctly.
            var user = _useRepository.GetFirstUser();

            //This is just to show how to validate the password
            if (!_hashManager.ValidateHash(password, user.Password) || user == null)
            {
                user = null;
            }

            return user;
        }
    }
}
