namespace Models.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //This sample app only has one user, so there isn't any need to 
        //go hog wild with methods
        User GetFirstUser();
    }
}