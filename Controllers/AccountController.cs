using System.Collections.Generic;
using System.Linq;
using bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank.Controllers
{
    public class AccountController : Controller
    {
        private BankContext _context;

        public AccountController(BankContext context) {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("showAccounts")]
        public IActionResult ShowAccounts()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("ShowLogin", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            List<Account> Accounts = _context.Accounts.Where(a => a.UserId == userId).ToList();
            ViewBag.accounts = Accounts;
            return View();
        }

        [HttpGet]
        [Route("AddAccount")]
        public IActionResult AddAccount() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("ShowLogin", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View();
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(Account acc) {
            if (ModelState.IsValid) {
                _context.Accounts.Add(acc);
                _context.SaveChanges();
                return RedirectToAction("ShowAccounts");
            }
            else {
                return View("AddAccount");
            }
        }

        [HttpPost]
        [Route("ShowAccount")]

        public IActionResult ShowAccount(int selectedacct) {
            return RedirectToAction("ViewAccount", new {id = selectedacct});
        }

        [HttpGet]
        [Route("ViewAccount/{id}")]
        public IActionResult ViewAccount(int id) {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("ShowLogin", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");

            ViewBag.account = _context.Accounts.Where(a => a.AccountId == id).SingleOrDefault();
            ViewBag.transactions = _context.Transactions.Where(t => t.AccountId == id).OrderByDescending(t => t.CreatedAt).ToList();
            return View();
        }

        [HttpPost]
        [Route("addTransaction")]
        public IActionResult AddTransaction(Transaction trans) {
            Account acc = _context.Accounts.Where(a => a.AccountId == trans.AccountId).SingleOrDefault();
            if (ModelState.IsValid) {
                if (acc.Balance + trans.Amount < 0) {
                    TempData["transError"] = "Cannot withdraw more than your balance!";
                    return RedirectToAction("ViewAccount", new {id = trans.AccountId});
                }
                acc.Balance += trans.Amount;
                _context.Transactions.Add(trans);
                _context.SaveChanges();
            }            
            else {
                TempData["transError"] = "Invalid entry.";
            }
            return RedirectToAction("ViewAccount", new {id = trans.AccountId});
        }
    }
}