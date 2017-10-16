using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace bank.Controllers
{
    public class UserController : Controller
    {
        private BankContext _context;

        public UserController(BankContext context) {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("showLogin")]
        public IActionResult ShowLogin()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUserModel newUser) {
            if (ModelState.IsValid) {
                //check for same password
                var existing = _context.Users.Where(u => u.EmailAddress == newUser.EmailAddress).SingleOrDefault();
                if (existing == null) {
                    //hash pw
                    PasswordHasher<RegisterUserModel> Hasher = new PasswordHasher<RegisterUserModel>();
                    string hashedp = Hasher.HashPassword(newUser, newUser.Password);

                    //create user object
                    User newU = new User {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        EmailAddress = newUser.EmailAddress,
                        Password = hashedp
                    };

                    //add new user to db
                    _context.Users.Add(newU);
                    _context.SaveChanges();

                    //set session variables
                    HttpContext.Session.SetInt32("UserId", newU.UserId);
                    HttpContext.Session.SetString("Username", newU.FirstName);
                    return RedirectToAction("ShowAccounts", "Account");
                }
                else {
                    ViewBag.errors = "A user with that email aready exists.";
                    return View("Index");
                }
            }
            return View("Index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password) {
            var existing = _context.Users.Where(u => u.EmailAddress == email).SingleOrDefault();
            if (existing != null) {
                var Hasher = new PasswordHasher<User>();
                if (0 != Hasher.VerifyHashedPassword(existing, existing.Password, password)) {
                    HttpContext.Session.SetInt32("UserId", existing.UserId);
                    HttpContext.Session.SetString("Username", existing.FirstName);
                    return RedirectToAction("ShowAccounts", "Account");
                }
                else {
                    ViewBag.pwerror = "Password is incorrect.";
                    return View("Login");
                }
            }
            else {
                ViewBag.emailerror = "This email has not been registered.";
                return View("Login");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("ShowLogin");
        }
    }
}
