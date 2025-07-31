using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace StockTrackingAutomation.AdminUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Account()
        {
            var users = _userService.List<UserAccount>().ToList();
            return View(users);
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount
                {
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    UserName = model.UserName.ToLower()
                };

                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                if (!_userService.IsUsernameUnique(model.UserName))
                {
                    ModelState.AddModelError("Username", "Username already exists. Please choose a different username.");
                    return View(model);
                }

                if (!_userService.IsEmailUnique(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists. Please choose a different email.");
                    return View(model);
                }

                try
                {
                    _userService.Insert(account);

                    ModelState.Clear();
                    ViewBag.Message = $"{account.Firstname}"+ "{account.Lastname} registered successfully. Please login.";
                }
                catch (DbUpdateException ex)
                {

                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = _userService.List<UserAccount>(x => (x.UserName == model.UserNameorEmail || x.Email == model.UserNameorEmail)).FirstOrDefault();

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                string userRole = (user.UserName == "admin") ? "Admin" : "User";
                HttpContext.Session.SetString("UserRole", userRole);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, userRole),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Username/Email or Password is not correct");
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = _userService.List<UserAccount>(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(int id, UserAccount user)
        {
            if (!ModelState.IsValid)
            {
                var existingUser = _userService.List<UserAccount>(u => u.Id == id).FirstOrDefault();
                if(existingUser == null)
                {
                    return NotFound();
                }

                if (!_userService.IsUsernameUnique(user.UserName.ToLower()) && existingUser.UserName != user.UserName.ToLower())
                {
                    ModelState.AddModelError("UserName", "Username already exists. Please choose a different username.");
                    return View(user);
                }
                if (!_userService.IsEmailUnique(user.Email) && existingUser.Email != user.Email)
                {
                    ModelState.AddModelError("Email", "Email already exists. Please choose a different email.");
                    return View(user);
                }
                existingUser.Firstname = user.Firstname;
                existingUser.Lastname = user.Lastname;
                existingUser.UserName = user.UserName.ToLower();
                existingUser.Email = user.Email;

                _userService.Update(existingUser);
                return RedirectToAction(nameof(Account));
            }
            return View(user);
        }
    } 
}
