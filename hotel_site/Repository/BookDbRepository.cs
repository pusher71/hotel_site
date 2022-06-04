using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class BookDbRepository : IRepository<Book>
    {
        private DataContext _context;

        public BookDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Book> GetEntityList()
        {
            return _context.Book.ToList();
        }

        public Book GetEntity(int id)
        {
            return _context.Book.FirstOrDefault(k => k.Id == id);
        }

        public void Create(Book entity)
        {
            if (_context.Book.Contains(entity))
                throw new Exception("Ошибка. Данная бронь уже существует.");
            _context.Book.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Book entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Book entity = GetEntity(id);
            if (!_context.Book.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.Book.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
