using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (_context.HotelBuilding.Any()) return;

            //добавление корпусов отеля
            HotelBuilding hotelBuilding1 = new HotelBuilding(1,
                "Корпус 1",
                "",
                "ул. Калужская, д. 13",
                "8-909-998-87-01",
                "eastwater1@yandex.ru");
            HotelBuilding hotelBuilding2 = new HotelBuilding(2,
                "Корпус 2",
                "",
                "ул. Калужская, д. 13",
                "8-909-998-87-02",
                "eastwater2@yandex.ru");
            HotelBuilding hotelBuilding3 = new HotelBuilding(3,
                "Корпус 3",
                "",
                "ул. Калужская, д. 13",
                "8-909-998-87-03",
                "eastwater3@yandex.ru");

            _context.HotelBuilding.Add(hotelBuilding1);
            _context.HotelBuilding.Add(hotelBuilding2);
            _context.HotelBuilding.Add(hotelBuilding3);

            _context.SaveChanges();
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
            return GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
