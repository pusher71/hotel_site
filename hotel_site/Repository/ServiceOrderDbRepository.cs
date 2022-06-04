using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class ServiceOrderDbRepository : IRepository<ServiceOrder>
    {
        private DataContext _context;

        public ServiceOrderDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<ServiceOrder> GetEntityList()
        {
            return _context.ServiceOrder.ToList();
        }

        public ServiceOrder GetEntity(int id)
        {
            return _context.ServiceOrder.FirstOrDefault(k => k.Id == id);
        }

        public void Create(ServiceOrder entity)
        {
            if (_context.ServiceOrder.Contains(entity))
                throw new Exception("Ошибка. Данный заказ услуги уже существует.");
            _context.ServiceOrder.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ServiceOrder entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            ServiceOrder entity = GetEntity(id);
            if (!_context.ServiceOrder.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.ServiceOrder.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
