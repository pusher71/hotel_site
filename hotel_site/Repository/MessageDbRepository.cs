using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class MessageDbRepository : IRepository<Message>
    {
        private DataContext _context;

        public MessageDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Message> GetEntityList()
        {
            return _context.Message.ToList();
        }

        public Message GetEntity(int id)
        {
            return _context.Message.FirstOrDefault(k => k.Id == id);
        }

        public void Create(Message entity)
        {
            if (_context.Message.Contains(entity))
                throw new Exception("Ошибка. Данное сообщение уже существует.");
            _context.Message.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Message entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Message entity = GetEntity(id);
            if (!_context.Message.Contains(entity))
                throw new Exception("Ошибка. Данное сообщение не существует.");
            _context.Message.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
