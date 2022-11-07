using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Loginuser(string Email, string password)
        {
            string query = @"select * from [User] where Email = '" + Email + "' and Password = '" + password + "'";
            var UserInfo = db.Database.SqlQuery<User>(query).ToList();
            List<User> list = new List<User>();
            if (UserInfo.Count > 0)
            {
                foreach (var p in UserInfo)
                {
                    User list_p = new User();
                    // Session["UserID"]
                   
                    if(p.Status== "Disable")
                    {
                        TempData["msg"] = "disable";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Session["userName"] = p.Name;
                        Session["user"] = p.Name;
                        Session["id"] = p.Uid;
                        Session["email"] = p.Email;
                        Session["status"] = p.Status;
                        var s = Session["user"].ToString();
                        ViewBag.name = p.Name;
                        // ViewBag.Status = p.Status;
                        
                       
                    }

                }
                Session["userName"] = ViewBag.name;
                //return RedirectToAction("/Home/index");
                return Redirect("/Home/index");
                // return View();
                // Response.Redirect("/Home/index");
            }
            else
            {
                TempData["msg"] = "invalid";
                return RedirectToAction("Index");
            }
            
        }
    }
}