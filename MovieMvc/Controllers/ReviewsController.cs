using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string query = "Select Movies.Mid,Movies.Name,Movies.NoOfView,Movies.PosterURL,Movies.Category,Movies.Ratings,Movies.ReleaseYear,Movies.Language,Movies.Runtime," +
                "Review.Rid,Review.Rating,Review.ReviewDate,Review.Message,[User].Name as UserName from Movies " +
                "inner join Review on Review.Mid = Movies.Mid inner join [User] on Review.Uid = [User].Uid order by Review.Rid desc";
            var movielist = db.Database.SqlQuery<Reviews>(query).ToList();
            List<Reviews> list = new List<Reviews>();
            foreach (var p in movielist)
            {
                Reviews list_p = new Reviews();
                list_p.Mid = p.Mid;
                list_p.Name = p.Name;
                list_p.NoOfView = p.NoOfView;
                list_p.Category = p.Category;
                list_p.Description = p.Description;
                list_p.PosterURL = p.PosterURL;
                list_p.PhotoURL = p.PhotoURL;
                list_p.ReleaseYear = p.ReleaseYear;
                list_p.Ratings = p.Ratings;
                list_p.Language = p.Language;
                list_p.Runtime = p.Runtime;
                list_p.Rid = p.Rid;
                list_p.Rating = p.Rating;
                list_p.ReviewDate = p.ReviewDate;
                list_p.Message = p.Message;
                list_p.UserName = p.UserName;

                list.Add(list_p);
            }
            return View(list);
        }
    }
}