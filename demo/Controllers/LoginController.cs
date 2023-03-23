using Antlr.Runtime.Misc;
using demo.Classes;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class LoginController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User s)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s.UserName))
                {
                    ViewBag.ErrorMessage = "Please enter user name!";
                    return View();
                }
                if (string.IsNullOrWhiteSpace(s.Password))
                {
                    ViewBag.ErrorMessage = "Please enter password!";
                    return View();
                }
                if (s.Password.Length < 6)
                {
                    ViewBag.ErrorMessage = "Password length should be minimum 6 character!";
                    return View();
                }

                User user = db.Users.Where(a => a.UserName.ToLower() == s.UserName.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.ErrorMessage = "Invalid username or password!";
                    return View();
                }

                SHA256 sha256 = SHA256.Create();
                string salt = user.Salt;

                string hashedpwd = Functions.HashToString(sha256.ComputeHash(Encoding.Unicode.GetBytes(s.Password + salt)));
                if (!hashedpwd.SequenceEqual(user.Password))
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View();
                }

                //remove login sessions older than 30 days for this user
                DateTime date30DaysAgo = DateTime.UtcNow.AddDays(-30);
                db.UserTickets.RemoveRange(db.UserTickets.Where(a => a.UserId == user.Id && a.CreatedOn < date30DaysAgo));

                string ticket = Functions.GetRandomString(20);
                UserTicket userTicket = new UserTicket();

                userTicket.CreatedOn = DateTime.Now;
                userTicket.Ticket = ticket;
                userTicket.UserId = user.Id;
                db.UserTickets.Add(userTicket);
                db.SaveChanges();

                Session["username"] = user.UserName;
                Session["userid"] = user.Id;
                Session["ticket"] = userTicket.Ticket;
                Session["name"] = user.Name;

                return RedirectToAction("Jobs", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex;
                return View();
            }

        }

        public ActionResult logout()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    string ticket = Session["ticket"].ToString();
                    UserTicket userTicket = db.UserTickets.Where(a => a.Ticket == ticket).FirstOrDefault();
                    if (userTicket != null)
                    {
                        db.UserTickets.Remove(userTicket);
                        db.SaveChanges();
                    }
                    Session.Abandon();
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                return ViewBag.ErrorMessage(ex);
            }

        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUpUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                TempData["error"] = "Please enter Name!";
                return RedirectToAction("SignUp");
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                TempData["error"] = "Please enter email address!";
                return RedirectToAction("SignUp");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                TempData["error"] = "Please enter password!";
                return RedirectToAction("SignUp");
            }
            if (user.Password.Length < 6)
            {
                TempData["error"] = "Password length should be minimum 6 character!";
                return RedirectToAction("SignUp");
            }
            try
            {
                if (user.Id == 0)
                {
                    if(db.Users.Where(a => a.UserName.ToLower() == user.UserName.ToLower()).Count() > 0)
                    {
                        TempData["error"] = "User name have already exist, Please try another username!";
                        return RedirectToAction("SignUp");
                    }
                    string salt = Functions.GetRandomString(25);
                    SHA256 sha256 = SHA256.Create();
                    string hashedpwd = Functions.HashToString(sha256.ComputeHash(Encoding.Unicode.GetBytes(user.Password + salt)));
                    user.Salt = salt;
                    user.Password = hashedpwd;
                    user.CreatedOn = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    //TempData["success"] = "User successfully created !";
                    return RedirectToAction("Login", "Login");

                }
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("SignUp");
            }
            return View();
        }
    }
}