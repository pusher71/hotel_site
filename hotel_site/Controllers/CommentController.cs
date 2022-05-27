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
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IRepository<HotelInfo> _hotelInfoDb;
        private IRepository<HotelBuilding> _hotelBuildingDb;
        private IRepository<HotelPhoto> _hotelPhotoDb;
        private IRepository<Room> _roomDb;
        private IRepository<RoomPhoto> _roomPhotoDb;
        private IRepository<User> _userDb;
        private IRepository<Comment> _commentDb;
        private IRepository<Message> _messageDb;
        private IRepository<Service> _serviceDb;
        private IRepository<HistoryAction> _historyActionDb;
        private IRepository<HistoryRecord> _historyRecordDb;

        public CommentController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            HotelInfoDbRepository hotelInfoDb,
            HotelBuildingDbRepository hotelBuildingDb,
            HotelPhotoDbRepository hotelPhotoDb,
            RoomDbRepository roomDb,
            RoomPhotoDbRepository roomPhotoDb,
            UserDbRepository userDb,
            CommentDbRepository commentDb,
            MessageDbRepository messageDb,
            ServiceDbRepository serviceDb,
            HistoryActionDbRepository historyActionDb,
            HistoryRecordDbRepository historyRecordDb)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _hotelInfoDb = hotelInfoDb;
            _hotelBuildingDb = hotelBuildingDb;
            _hotelPhotoDb = hotelPhotoDb;
            _roomDb = roomDb;
            _roomPhotoDb = roomPhotoDb;
            _userDb = userDb;
            _commentDb = commentDb;
            _messageDb = messageDb;
            _serviceDb = serviceDb;
            _historyActionDb = historyActionDb;
            _historyRecordDb = historyRecordDb;
        }

        public IActionResult Index()
        {
            return View(_commentDb.GetEntityList());
        }

        public IActionResult AddComment()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(string text, int rating)
        {
            try
            {
                Comment comment = new Comment(_commentDb.GetNewId(), text, rating, DateTime.Now.Ticks);
                comment.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _commentDb.Create(comment);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        public IActionResult DeleteComment(int id)
        {
            return View(id);
        }

        [Authorize]
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
    }
}
