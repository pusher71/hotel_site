using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class ServiceDbRepository : IRepository<Service>
    {
        private DataContext _context;

        public ServiceDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<Service> GetEntityList()
        {
            return _context.Service.ToList();
        }

        public Service GetEntity(int id)
        {
            return _context.Service.FirstOrDefault(k => k.Id == id);
        }

        public void Create(Service entity)
        {
            if (_context.Service.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.Service.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Service entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Service entity = GetEntity(id);
            if (!_context.Service.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.Service.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
