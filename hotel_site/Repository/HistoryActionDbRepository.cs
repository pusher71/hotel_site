using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class HistoryActionDbRepository : IRepository<HistoryAction>
    {
        private DataContext _context;

        public HistoryActionDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<HistoryAction> GetEntityList()
        {
            return _context.HistoryAction.ToList();
        }

        public HistoryAction GetEntity(int id)
        {
            return _context.HistoryAction.FirstOrDefault(k => k.Id == id);
        }

        public void Create(HistoryAction entity)
        {
            if (_context.HistoryAction.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.HistoryAction.Add(entity);
            _context.SaveChanges();
        }

        public void Update(HistoryAction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            HistoryAction entity = GetEntity(id);
            if (!_context.HistoryAction.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.HistoryAction.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
