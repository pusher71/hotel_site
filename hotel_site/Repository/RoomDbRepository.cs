using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class RoomDbRepository : IRepository<Room>
    {
        private DataContext _context;

        public RoomDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Room> GetEntityList()
        {
            return _context.Room.ToList();
        }

        public Room GetEntity(int id)
        {
            return _context.Room.FirstOrDefault(k => k.Id == id);
        }

        public void Create(Room entity)
        {
            if (_context.Room.Contains(entity))
                throw new Exception("Ошибка. Данный номер уже существует.");
            _context.Room.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Room entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Room entity = GetEntity(id);
            if (!_context.Room.Contains(entity))
                throw new Exception("Ошибка. Данный номер не существует.");
            _context.Room.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
