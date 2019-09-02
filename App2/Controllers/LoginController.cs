using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorise(User user)
        {
            using(DBModel db = new DBModel())
            {
                var userDetail = db.Users.Where(w => w.Username == user.Username && w.Password == user.Password).FirstOrDefault();
                if(userDetail == null)
                {

                    user.LoginErrormsg = "invalid username or password";
                    return View("Index", user);
                }
                else
                {
                    Session["username"] = user.Username;
                    return RedirectToAction("Index","Home");
                }
            }
            
        }
        
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}