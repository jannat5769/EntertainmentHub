using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string key)
        {
            //string query = "Select * from Movies order by Movies.Mid desc";
            string query = @"SELECT Movies.Mid, Movies.Name, Movies.Category, Movies.ReleaseYear, Movies.Description, Movies.NoOfView, Movies.Ratings, 
                 Movies.PosterURL, Movies.Language, Movies.Runtime, Movies.StudioName,
              Movies.NoOfReview from Movies where name like '%" + key + "%' or Category like '%" + key + "%' or ReleaseYear like '%" + key + "%' or Description like '%" + key + "%' or Ratings like '%" + key + "%' or Language like '%" + key + "%' " +
                "or Runtime like '%" + key + "%' or StudioName like '%" + key + "%' or Mid like '%" + key + "%'";
            var movielist = db.Database.SqlQuery<Movies>(query).ToList();
            if (movielist.Count > 0)
            {
                List<Movies> list = new List<Movies>();
                foreach (var p in movielist)
                {
                    Movies list_p = new Movies();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.Category = p.Category;
                    list_p.Description = p.Description;
                    list_p.ReleaseYear = p.ReleaseYear;
                    list_p.NoOfView = p.NoOfView;
                    list_p.Ratings = p.Ratings;
                    list_p.PosterURL = p.PosterURL;
                    list_p.Language = p.Language;
                    list_p.StudioName = p.StudioName;
                    list_p.NoOfReview = p.NoOfReview;


                    list.Add(list_p);
                }
                ViewBag.Searchlist = list;
                return View(list);
            }
            else
            {
                ViewBag.Searchlist = null;
                return View();
            }


                
        }
    }
}