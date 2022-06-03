using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using hotel_site.Models;
using hotel_site.Models.ViewModels;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<HotelInfo> _hotelInfoDb;
        private readonly IRepository<HotelBuilding> _hotelBuildingDb;
        private readonly IRepository<HotelPhoto> _hotelPhotoDb;
        private readonly IRepository<Room> _roomDb;
        private readonly IRepository<RoomPhoto> _roomPhotoDb;
        private readonly IRepository<Book> _bookDb;
        private readonly IRepository<Comment> _commentDb;
        private readonly IRepository<Message> _messageDb;
        private readonly IRepository<Service> _serviceDb;

        public BookController(ILogger<HomeController> logger,
            UserManager<User> userManager,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            BookDbRepository bookDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb)
        {
            _logger = logger;
            _userManager = userManager;
            _hotelInfoDb = hotelInfoDb;
            _hotelBuildingDb = hotelBuildingDb;
            _hotelPhotoDb = hotelPhotoDb;
            _roomDb = roomDb;
            _roomPhotoDb = roomPhotoDb;
            _bookDb = bookDb;
            _commentDb = commentDb;
            _messageDb = messageDb;
            _serviceDb = serviceDb;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "admin"))
                return View(_bookDb.GetEntityList());
            else
                return View(_bookDb.GetEntityList().Where(k => k.UserId == _userManager.GetUserId(User)));
        }

        [Authorize(Roles = "client")]
        public IActionResult SelectRoom()
        {
            return View();
        }

        [Authorize(Roles = "client")]
        public IActionResult AddBook(int roomId)
        {
            return View(roomId);
        }

        [Authorize(Roles = "client")]
        [HttpPost]
        public async Task<IActionResult> AddBook(int roomId, DateTime momentStart, DateTime momentEnd, int personCount)
        {
            Room room = _roomDb.GetEntity(roomId);

            //проверить промежуток времени
            if (momentStart >= momentEnd)
                return View("ErrorPage", "Дата начала должна быть меньше даты окончания.");

            //проверить доступность самой комнаты
            if (!room.IsAvailable)
                return View("ErrorPage", "Комната временно недоступна.");

            //проверить вместительность
            if (personCount > room.MaxPersonCount)
                return View("ErrorPage", "Комната не рассчитана на данное количество человек.");

            //проверить возможность забронировать
            foreach (Book existingBook in _bookDb.GetEntityList())
                if (existingBook.RoomId == room.Id && existingBook.MomentStart < momentEnd && existingBook.MomentEnd > momentStart)
                    return View("ErrorPage", "Выбранный промежуток времени пересекается с другой бронью. Выберите другое время или смените номер.");

            try
            {
                Book book = new Book(_bookDb.GetNewId(), momentStart, momentEnd, personCount, personCount * room.Price);
                book.Link(await _userManager.GetUserAsync(User), room);
                _bookDb.Create(book);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin,client")]
        public IActionResult DeleteBook(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin,client")]
        [HttpPost]
        public IActionResult DeleteBook(int id, bool confirm)
        {
            try
            {
                _bookDb.Delete(id);
                return RedirectToAction("Index", "Home");
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
