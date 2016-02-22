namespace Business.Interfaces
{
    public interface IHashManager
    {
        string CreateHashFromString(string password);
        bool ValidateHash(string password, string correctHash);
    }
}