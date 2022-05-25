using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class UserDbRepository : IRepository<User>
    {
        private DataContext _context;

        public UserDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<User> GetEntityList()
        {
            return _context.User.ToList();
        }

        public User GetEntity(int id)
        {
            return _context.User.FirstOrDefault(k => k.Id == id);
        }

        public void Create(User entity)
        {
            if (_context.User.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.User.Add(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            User entity = GetEntity(id);
            if (!_context.User.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.User.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
