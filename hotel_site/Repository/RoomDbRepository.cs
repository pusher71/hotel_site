using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (_context.Room.Any()) return;

            Room room141 = new Room(6, "14-1", "14", 15, 15, 14, true);
            Room room142 = new Room(2, "14-2", "14", 30, 25, 14, false);
            Room room143 = new Room(5, "14-3", "14", 45, 40, 14, true);

            Room room131 = new Room(3, "13-1", "13", 15, 15, 13, false);
            Room room132 = new Room(1, "13-2", "13", 30, 25, 13, true);
            Room room133 = new Room(4, "13-3", "13", 45, 40, 13, false);
            Room room134 = new Room(7, "13-4", "13", 5, 10, 13, true);

            room141.HotelBuildingId = 1;
            room142.HotelBuildingId = 1;
            room143.HotelBuildingId = 1;
            room131.HotelBuildingId = 1;
            room132.HotelBuildingId = 1;
            room133.HotelBuildingId = 1;
            room134.HotelBuildingId = 1;

            _context.Room.AddRange(room132, room142, room131, room133, room143, room141, room134);
            _context.SaveChanges();
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
                throw new Exception("Ошибка. Данная фотография отеля уже существует.");
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
                throw new Exception("Ошибка. Данная фотография отеля не существует.");
            _context.Room.Remove(entity);
            _context.SaveChanges();
        }

        public int GetNewId()
        {
            return GetEntityList().Count() == 0 ? 1 : GetEntityList().OrderBy(k => k.Id).Last().Id + 1;
        }
    }
}
