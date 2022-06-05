using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class MessageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Message> _messageDb;

        public MessageController(UserManager<User> userManager, MessageDbRepository messageDb)
        {
            _userManager = userManager;
            _messageDb = messageDb;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "admin"))
                return View("UserList", new UserListInfo() {
                    Users = await GetUsers(),
                    AdminId = _userManager.GetUserId(User)
                });
            else
                return RedirectToAction("Messages", "Message", new { userFromId = _userManager.GetUserId(User), userToId = (await _userManager.GetUsersInRoleAsync("admin")).First().Id });
        }

        public IActionResult Messages(string userFromId, string userToId)
        {
            return View(new MessagesInfo()
            {
                UserFromId = userFromId,
                UserToId = userToId,
                Messages = GetMessagesByUserIds(userFromId, userToId),
            });
        }

        public IEnumerable<Message> GetMessagesByUserIds(string userFromId, string userToId)
        {
            return _messageDb.GetEntityList().Where(k => 
            (k.UserFromId == userFromId && k.UserToId == userToId) ||
            (k.UserFromId == userToId && k.UserToId == userFromId));
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            List<string> userIds = new List<string>();
            foreach (Message message in _messageDb.GetEntityList())
            {
                if (!userIds.Contains(message.UserFromId))
                    userIds.Add(message.UserFromId);
            }

            List<User> users = new List<User>();
            foreach (string userId in userIds)
            {
                
                User user = await _userManager.FindByIdAsync(userId);
                if (!await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "admin"))
                    users.Add(user);
            }

            return users;
        }

        [Authorize]
        public IActionResult AddMessage(string userToId)
        {
            return View("AddMessage", userToId);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMessage(string text, string userToId)
        {
            try
            {
                Message message = new Message(_messageDb.GetNewId(), text, DateTime.Now);
                User userFrom = await _userManager.GetUserAsync(User);
                User userTo = await _userManager.FindByIdAsync(userToId);
                message.SetUsers(userFrom, userTo);
                _messageDb.Create(message);
                return RedirectToAction("Messages", new
                {
                    UserFromId = userFrom.Id,
                    UserToId = userToId,
                    Messages = GetMessagesByUserIds(userFrom.Id, userToId),
                });
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
