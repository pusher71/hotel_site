using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using hotel_site.Models;
using hotel_site.Models.ViewModels;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
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

        public CommentController(ILogger<CommentController> logger,
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

        public IActionResult Index()
        {
            return View(_commentDb.GetEntityList());
        }

        [Authorize]
        public IActionResult AddComment()
        {
            if (UserIsResident())
                return View();
            else
                return View("ErrorPage", "Отзывы могут оставлять только постояльцы.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(string text, int rating)
        {
            if (!UserIsResident())
                return View("ErrorPage", "Отзывы могут оставлять только постояльцы.");
            if (rating < 1 || rating > 5)
                return View("ErrorPage", "Ошибка. Оценка должна быть в интервале [1..5].");

            try
            {
                User user = await _userManager.GetUserAsync(User);
                Comment comment = new Comment(_commentDb.GetNewId(), text, rating, DateTime.Now);
                comment.SetUser(user);
                _commentDb.Create(comment);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteComment(int id)
        {
            return View(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteComment(int id, bool confirm)
        {
            try
            {
                _commentDb.Delete(id);
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
            bool activeBookExists = false;
            foreach (Book book in _bookDb.GetEntityList())
                if (book.UserId == _userManager.GetUserId(User) && book.IsActive())
                    activeBookExists = true;
            return activeBookExists;
        }
    }
}
