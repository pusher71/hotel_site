using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using hotel_site.Models;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRepository<HotelBuilding> _hotelBuildingDb;
        private readonly IRepository<Room> _roomDb;

        public RoomController(HotelBuildingDbRepository hotelBuildingDb, RoomDbRepository roomDb)
        {
            _hotelBuildingDb = hotelBuildingDb;
            _roomDb = roomDb;
        }

        public IActionResult Index(int id)
        {
            return View(_roomDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddRoom(int hotelBuildingId)
        {
            return View(hotelBuildingId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddRoom(int hotelBuildingId, string number, string floor, float square, float price, int maxPersonCount, string isAvailable)
        {
            if (number == null)
                return View("ErrorPage", "Название номера не должно быть пустым.");
            if (floor == null)
                return View("ErrorPage", "Этаж не должен быть пустым.");

            try
            {
                Room room = new Room(_roomDb.GetNewId(), number, floor, square, price, maxPersonCount, isAvailable == "on"); //созданный номер
                HotelBuilding hotelBuilding = _hotelBuildingDb.GetEntity(hotelBuildingId); //связанный корпус отеля

                room.SetHotel(hotelBuilding);
                _roomDb.Create(room);
                return RedirectToAction("Index", "HotelBuilding", new { id = room.HotelBuildingId });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditRoom(int id)
        {
            return View(_roomDb.GetEntity(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditRoom(int id, string number, string floor, float square, float price, int maxPersonCount, string isAvailable)
        {
            if (number == null)
                return View("ErrorPage", "Название номера не должно быть пустым.");
            if (floor == null)
                return View("ErrorPage", "Этаж не должен быть пустым.");

            try
            {
                Room room = _roomDb.GetEntity(id);
                room.Number = number;
                room.Floor = floor;
                room.Square = square;
                room.Price = price;
                room.MaxPersonCount = maxPersonCount;
                room.IsAvailable = isAvailable == "on";
                _roomDb.Update(room);
                return RedirectToAction("Index", new { id = room.Id });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult RoomAvailableChange(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult RoomAvailableChange(int id, bool confirm)
        {
            try
            {
                Room room = _roomDb.GetEntity(id);
                room.IsAvailable = !room.IsAvailable;
                _roomDb.Update(room);
                return RedirectToAction("Index", "HotelBuilding", new { id = room.HotelBuildingId });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteRoom(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteRoom(int id, bool confirm)
        {
            try
            {
                Room room = _roomDb.GetEntity(id);
                _roomDb.Delete(id);
                return RedirectToAction("Index", "HotelBuilding", new { id = room.HotelBuildingId });
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
