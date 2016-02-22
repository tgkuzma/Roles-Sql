using System.Collections.Generic;
using System.Linq;
using Models.Interfaces;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RoleContext _context;

        public Repository(RoleContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Add(T entityToAdd)
        {
            _context.Set<T>().Add(entityToAdd);
        }

        public void Delete(T entityToDelete)
        {
            _context.Set<T>().Remove(entityToDelete);
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}