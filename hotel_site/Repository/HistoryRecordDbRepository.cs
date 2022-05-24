using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class HistoryRecordDbRepository : IRepository<HistoryRecord>
    {
        private DataContext _context;

        public HistoryRecordDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<HistoryRecord> GetEntityList()
        {
            return _context.HistoryRecord.ToList();
        }

        public HistoryRecord GetEntity(int id)
        {
            return _context.HistoryRecord.FirstOrDefault(k => k.Id == id);
        }

        public void Create(HistoryRecord entity)
        {
            if (_context.HistoryRecord.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
            _context.HistoryRecord.Add(entity);
            _context.SaveChanges();
        }

        public void Update(HistoryRecord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            HistoryRecord entity = GetEntity(id);
            if (!_context.HistoryRecord.Contains(entity))
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.HistoryRecord.Remove(entity);
            _context.SaveChanges();
        }
    }
}
