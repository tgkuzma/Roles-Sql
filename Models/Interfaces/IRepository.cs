using System.Collections.Generic;

namespace Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entityToAdd);
        void Delete(T entityToDelete);
        void Update();
    }
}