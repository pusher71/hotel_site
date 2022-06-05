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
        private readonly ILogger<BookController> _logger;
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
        private readonly IRepository<ServiceOrder> _serviceOrderDb;

        public BookController(ILogger<BookController> logger,
            UserManager<User> userManager,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            BookDbRepository bookDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb,
            ServiceOrderDbRepository serviceOrderDb)
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
            _serviceOrderDb = serviceOrderDb;
        }

        [Authorize]
        public IActionResult Index()
        {
            return UserIsAdmin()
                ? View(_bookDb.GetEntityList())
                : View(_bookDb.GetEntityList().Where(k => k.UserId == _userManager.GetUserId(User)));
        }

        [Authorize]
        public IActionResult SelectHotelBuilding()
        {
            return View(_hotelBuildingDb.GetEntityList());
        }

        [Authorize]
        public IActionResult SelectRequirements(int hotelBuildingId)
        {
            return View(hotelBuildingId);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SelectRequirements(int hotelBuildingId, DateTime momentStart, DateTime momentEnd, int personCount)
        {
            //проверить промежуток времени
            if (momentStart >= momentEnd)
                return View("ErrorPage", "Дата начала должна быть меньше даты окончания.");
            if (momentStart < DateTime.Now.Date)
                return View("ErrorPage", "Дата начала должна быть не раньше сегодняшнего дня.");

            return RedirectToAction("SelectRoom", new
            {
                hotelBuildingId,
                momentStart,
                momentEnd = momentEnd.AddDays(0.9993),
                personCount
            });
        }

        [Authorize]
        public IActionResult SelectRoom(int hotelBuildingId, DateTime momentStart, DateTime momentEnd, int personCount)
        {
            return View(new RoomsViewData(_hotelBuildingDb.GetEntity(hotelBuildingId), new BookRequirements(momentStart, momentEnd, personCount), _bookDb.GetEntityList()));
        }

        [Authorize]
        [HttpPost]
        public IActionResult SelectRoom(int roomId, DateTime momentStart, DateTime momentEnd, int personCount, bool confirm)
        {
            return RedirectToAction("AddBook", new
            {
                roomId,
                momentStart,
                momentEnd,
                personCount
            });
        }

        [Authorize]
        public IActionResult AddBook(int roomId, DateTime momentStart, DateTime momentEnd, int personCount)
        {
            return View(new BookNewModel()
            {
                Room = _roomDb.GetEntity(roomId),
                MomentStart = momentStart,
                MomentEnd = momentEnd,
                PersonCount = personCount
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook(int roomId, DateTime momentStart, DateTime momentEnd, int personCount, bool confirm)
        {
            if (UserIsAdmin())
                return View("ErrorPage", "Администратор не может забронировать номер.");
            if (BookExists())
                return View("ErrorPage", "Нельзя бронировать более одного номера.");

            try
            {
                Room room = _roomDb.GetEntity(roomId);
                Book book = new Book(_bookDb.GetNewId(), momentStart, momentEnd, personCount,
                    room.Price * (momentEnd - momentStart).Days * personCount);
                book.Link(await _userManager.GetUserAsync(User), room);
                _bookDb.Create(book);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize]
        public IActionResult MarkAsPaid(int id)
        {
            return View(id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult MarkAsPaid(int id, bool confirm)
        {
            try
            {
                Book book = _bookDb.GetEntity(id);
                book.Paid = true;
                _bookDb.Update(book);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize]
        public IActionResult DeleteBook(int id)
        {
            return View(id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteBook(int id, bool confirm)
        {
            try
            {
                Book book = _bookDb.GetEntity(id);
                if (book.IsActive() && !User.IsInRole("admin"))
                    return View("ErrorPage", "Ошибка. Действующую бронь нельзя удалить.");
                _bookDb.Delete(id);
                return RedirectToAction("Index");
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

        public bool UserIsAdmin()
        {
            return User.IsInRole("admin");
        }

        public bool UserIsResident()
        {
            string userId = _userManager.GetUserId(User);
            bool activeBookExists = false;
            foreach (Book book in _bookDb.GetEntityList())
                if (book.UserId == userId && book.IsActive())
                    activeBookExists = true;
            return activeBookExists;
        }

        public bool BookExists()
        {
            string userId = _userManager.GetUserId(User);
            bool activeBookExists = false;
            foreach (Book book in _bookDb.GetEntityList())
                if (book.UserId == userId)
                    activeBookExists = true;
            return activeBookExists;
        }
    }
}
