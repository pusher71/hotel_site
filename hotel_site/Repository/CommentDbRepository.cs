using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class CommentDbRepository : IRepository<Comment>
    {
        private DataContext _context;

        public CommentDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Comment> GetEntityList()
        {
            return _context.Comment.ToList();
        }

        public Comment GetEntity(int id)
        {
            return _context.Comment.FirstOrDefault(k => k.Id == id);
        }

        public void Create(Comment entity)
        {
            if (_context.Comment.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.Comment.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Comment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Comment entity = GetEntity(id);
            if (!_context.Comment.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.Comment.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
