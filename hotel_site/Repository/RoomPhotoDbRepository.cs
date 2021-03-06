using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class RoomPhotoDbRepository : IRepository<RoomPhoto>
    {
        private DataContext _context;

        public RoomPhotoDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<RoomPhoto> GetEntityList()
        {
            return _context.RoomPhoto.ToList();
        }

        public RoomPhoto GetEntity(int id)
        {
            return _context.RoomPhoto.FirstOrDefault(k => k.Id == id);
        }

        public void Create(RoomPhoto entity)
        {
            if (_context.RoomPhoto.Contains(entity))
                throw new Exception("Ошибка. Данная фотография номера уже существует.");
            _context.RoomPhoto.Add(entity);
            _context.SaveChanges();
        }

        public void Update(RoomPhoto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            RoomPhoto entity = GetEntity(id);
            if (!_context.RoomPhoto.Contains(entity))
                throw new Exception("Ошибка. Данная фотография номера не существует.");
            _context.RoomPhoto.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
