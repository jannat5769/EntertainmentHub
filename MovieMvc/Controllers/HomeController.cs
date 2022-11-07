using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            

            List<Movies> MovieSliderlist = new List<Movies>();
            List<Movies> PolularMovielist = new List<Movies>();
            List<Movies> LatestMovielist = new List<Movies>();
            List<Movies> MostViewMovielist = new List<Movies>();
            List<Reviews> LatestReviewMovielist = new List<Reviews>();

          
            string query = "Select * from Movies order by Movies.Mid desc";
            var movielist = db.Database.SqlQuery<Movies>(query).ToList();
            if (movielist.Count > 0)
            {
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

                    MovieSliderlist.Add(list_p);
                }
                ViewBag.MovieSliderlist = MovieSliderlist;
            }
            else
            {
                ViewBag.MovieSliderlist = null;
            }

            //--- Polular Movie List ---//

            string query1 = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.Ratings,Movies.PhotoURL,Movies.TrailerURL from Movies order by Movies.Ratings desc";
            var popularmovielis = db.Database.SqlQuery<Movies>(query1).ToList();
            if (popularmovielis.Count > 0)
            {
                foreach (var p in popularmovielis)
                {
                    Movies list_p = new Movies();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.Category = p.Category;
                    list_p.Description = p.Description;
                    list_p.PosterURL = p.PosterURL;
                    list_p.PhotoURL = p.PhotoURL;
                    list_p.ReleaseYear = p.ReleaseYear;
                    list_p.Ratings = p.Ratings;
                    list_p.TrailerURL = p.TrailerURL;
                    list_p.MovieGenre = load_popular_every_genre_byId(p.Mid.ToString());

                    PolularMovielist.Add(list_p);
                }
                ViewBag.PolularMovielist = PolularMovielist;
            }
            else
            {
                ViewBag.PolularMovielist = null;
            }

            //--------- Latest Movie List ----//
            string query2 = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.Ratings,Movies.PhotoURL,Movies.TrailerURL from Movies order by Movies.Mid desc";
            var Latestmovielist = db.Database.SqlQuery<Movies>(query2).ToList();
            if (Latestmovielist.Count > 0)
            {
                foreach (var p in Latestmovielist)
                {
                    Movies list_p = new Movies();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.Category = p.Category;
                    list_p.Description = p.Description;
                    list_p.PosterURL = p.PosterURL;
                    list_p.PhotoURL = p.PhotoURL;
                    list_p.ReleaseYear = p.ReleaseYear;
                    list_p.Ratings = p.Ratings;
                    list_p.TrailerURL = p.TrailerURL;
                    list_p.MovieGenre = load_popular_every_genre_byId(p.Mid.ToString());

                    LatestMovielist.Add(list_p);
                }
                ViewBag.LatestMovielist = LatestMovielist;
            }
            else
            {
                ViewBag.LatestMovielist = null;
            }

            //---------Most View Movie List -------//
            
            string query3 = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.Ratings,Movies.PhotoURL,Movies.TrailerURL from Movies order by Movies.NoOfView desc";
            var mostviewmovielist = db.Database.SqlQuery<Movies>(query3).ToList();
            if (mostviewmovielist.Count > 0)
            {
                foreach (var p in mostviewmovielist)
                {
                    Movies list_p = new Movies();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.Category = p.Category;
                    list_p.Description = p.Description;
                    list_p.PosterURL = p.PosterURL;
                    list_p.PhotoURL = p.PhotoURL;
                    list_p.ReleaseYear = p.ReleaseYear;
                    list_p.Ratings = p.Ratings;
                    list_p.TrailerURL = p.TrailerURL;
                    list_p.MovieGenre = load_popular_every_genre_byId(p.Mid.ToString());

                    MostViewMovielist.Add(list_p);
                }
                ViewBag.MostViewMovielist = MostViewMovielist;
            }
            else
            {
                ViewBag.MostViewMovielist = null;
            }

            //---------Lates reView Movie List -------//

            string query4 = @"select top 5 Movies.Mid,Movies.Name as MovieName,Movies.ReleaseYear,Movies.PosterURL,Movies.PhotoURL,[User].Uid,[User].Name,
    Review.Rid,Review.Message,Review.Rating,Review.ReviewDate from Movies inner join Review on Movies.Mid =
    Review.Mid inner join [User] on [User].Uid = Review.Uid order by Review.Rid desc";

            var LatestReviewlist = db.Database.SqlQuery<Reviews>(query4).ToList();
            if (LatestReviewlist.Count > 0)
            {
                foreach (var p in LatestReviewlist)
                {
                    Reviews list_p = new Reviews();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.MovieName = p.MovieName;
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

                    LatestReviewMovielist.Add(list_p);
                }
                ViewBag.LatestReviewMovielist = LatestReviewMovielist;
            }
            else
            {
                ViewBag.LatestReviewMovielist = null;
            }
            return View();
        }
        public string load_popular_every_genre_byId(string Mid)
        {
            string strSql;
            try
            {
                string tempGenre = "";
                string query = "Select Genre.Title from MovieGenre inner join Genre on MovieGenre.Gid = Genre.Gid where MovieGenre.Mid = " + Mid + "";
                var movielist = db.Database.SqlQuery<MoviesGen>(query).ToList();
                List<MoviesGen> list = new List<MoviesGen>();
                foreach (var p in movielist)
                {
                    tempGenre += p.Title.ToString() + ", ";
                }
                tempGenre = tempGenre.Remove(tempGenre.Length - 2);

                return tempGenre;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
               // return "success";
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index", "home");
        }
    }
}