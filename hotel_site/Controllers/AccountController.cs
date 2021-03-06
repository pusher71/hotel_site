using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using hotel_site.Models;
using hotel_site.Models.ViewModels;

namespace hotel_site.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Profile(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
                return View(user);
            else
                return View("ErrorPage", "Ошибка. Пользователь не найден.");
        }

        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user =
                await _userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var sr = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Неправильное имя или пароль.");
            return View(loginModel);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Agree)
                {
                    ModelState.AddModelError("Ошибка регистрации.", "Вы должны дать согласие на обработку персональных данных.");
                    return View(model);
                }

                if (model.Password != model.PasswordConfirm)
                {
                    ModelState.AddModelError("Ошибка регистрации.", "Пароли должны совпадать.");
                    return View(model);
                }

                if (!PhoneNumberIsValid(model.PhoneNumber))
                {
                    ModelState.AddModelError("Ошибка регистрации.", "Номер телефона должен начинаться с плюса и иметь 11 цифр.");
                    return View(model);
                }

                User user = new User {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    var sr = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return View("ErrorPage", "Не удалось выполнить вход в сисему");
                }
                ModelState.AddModelError("", "Неизвестная ошибка авторизации. Попробуйте использовать более сложный пароль, либо другой логин.");
                return View(model);
            }
            return View(model);
        }

        private bool PhoneNumberIsValid(string phoneNumber)
        {
            Regex regex = new Regex(@"\+\d{11}");
            return phoneNumber.Length == 12 && regex.IsMatch(phoneNumber);
        }
    }
}
