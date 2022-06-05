using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using hotel_site.Models;
using hotel_site.Models.ViewModels;
using hotel_site.Repository;

using Microsoft.AspNetCore.Authorization;

namespace hotel_site.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Book> _bookDb;
        private readonly IRepository<Comment> _commentDb;

        public CommentController(UserManager<User> userManager,
            BookDbRepository bookDb,
            CommentDbRepository commentDb)
        {
            _userManager = userManager;
            _bookDb = bookDb;
            _commentDb = commentDb;
        }

        public IActionResult Index()
        {
            return View(new CommentsViewData()
            {
                Comments = _commentDb.GetEntityList(),
                AddCommentEnabled = UserIsResident()
            });
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
            if (text == "")
                return View("ErrorPage", "Текст комментария не должен быть пустым.");

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
            string userId = _userManager.GetUserId(User);
            bool activeBookExists = false;
            foreach (Book book in _bookDb.GetEntityList())
                if (book.UserId == userId && book.IsActive())
                    activeBookExists = true;
            return activeBookExists;
        }
    }
}
