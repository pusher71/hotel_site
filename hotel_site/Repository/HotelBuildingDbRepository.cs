using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site.Repository
{
    public class HotelBuildingDbRepository : IRepository<HotelBuilding>
    {
        private DataContext _context;

        public HotelBuildingDbRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IEnumerable<HotelBuilding> GetEntityList()
        {
            return _context.HotelBuilding.ToList();
        }

        public HotelBuilding GetEntity(int id)
        {
            return _context.HotelBuilding.FirstOrDefault(k => k.Id == id);
        }

        public void Create(HotelBuilding entity)
        {
            if (_context.HotelBuilding.Contains(entity))
                throw new Exception("Ошибка. Данный корпус отеля уже существует.");
            _context.HotelBuilding.Add(entity);
            _context.SaveChanges();
        }

        public void Update(HotelBuilding entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            HotelBuilding entity = GetEntity(id);
            if (!_context.HotelBuilding.Contains(entity))
                throw new Exception("Ошибка. Данный корпус отеля не существует.");
            _context.HotelBuilding.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
