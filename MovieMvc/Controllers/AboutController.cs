using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class AboutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: About
        public ActionResult Index()
        {
            string query = "Select * from Movies order by Movies.Mid desc";
            var movielist = db.Database.SqlQuery<Movies>(query).ToList();
            List<Movies> list = new List<Movies>();
            foreach (var p in movielist)
            {
                Movies list_p = new Movies();
                list_p.Mid = p.Mid;
                list_p.Name = p.Name;
                list_p.Category = p.Category;
                list_p.Description = p.Description;
                list_p.PosterURL = p.PosterURL;
                list_p.PhotoURL = p.PhotoURL;
                list_p.ReleaseYear = p.ReleaseYear;

                list.Add(list_p);
            }
            return View(list);
        }
    }
}