using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class HotelPhotoDbRepository : IRepository<HotelPhoto>
    {
        private DataContext _context;

        public HotelPhotoDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<HotelPhoto> GetEntityList()
        {
            return _context.HotelPhoto.ToList();
        }

        public HotelPhoto GetEntity(int id)
        {
            return _context.HotelPhoto.FirstOrDefault(k => k.Id == id);
        }

        public void Create(HotelPhoto entity)
        {
            if (_context.HotelPhoto.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.HotelPhoto.Add(entity);
            _context.SaveChanges();
        }

        public void Update(HotelPhoto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            HotelPhoto entity = GetEntity(id);
            if (!_context.HotelPhoto.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.HotelPhoto.Remove(entity);
            _context.SaveChanges();
        }
    }
}
