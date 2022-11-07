using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class MovieShowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string info,string Category,string Gid,string year)
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.GenreList = GetGenre();
            ViewBag.YearList = GetYear();

            ViewBag.loadmovie = LoadMovieInfo(info, Category, Gid, year);
            return View();
        }

        public List<Categorylist> GetCategory()
        {
            List<Categorylist> category = new List<Categorylist>();
            string query = "Select Category,COUNT(Category) as Count from Movies group by Category order by COUNT(Category) desc";
            var Categorylist = db.Database.SqlQuery<Categorylist>(query).ToList();
            if (Categorylist.Count > 0)
            {
                foreach (var p in Categorylist)
                {
                    Categorylist list_p = new Categorylist();
                    list_p.Category = p.Category;
                    list_p.Count = p.Count;
                    category.Add(list_p);
                }
            }
            return category;
        }
        public List<Genrelist> GetGenre()
        {
            List<Genrelist> genre = new List<Genrelist>();
            string query = "Select distinct MovieGenre.Gid,Genre.Title,COUNT(MovieGenre.Gid) as MovieCount from MovieGenre inner join Genre on MovieGenre.Gid = Genre.Gid group by MovieGenre.Gid,Genre.Title";
            var Genrelist = db.Database.SqlQuery<Genrelist>(query).ToList();
            if (Genrelist.Count > 0)
            {
                foreach (var p in Genrelist)
                {
                    Genrelist list_p = new Genrelist();
                    list_p.Gid = p.Gid;
                    list_p.Title = p.Title;
                    list_p.MovieCount = p.MovieCount;
                    genre.Add(list_p);
                }
            }
            return genre;
        }
        public List<SelectListItem> GetYear()
        {
            List<Year> year = new List<Year>();
            string query = "Select distinct ReleaseYear from Movies order by ReleaseYear desc";
            var yeralist = db.Database.SqlQuery<Year>(query).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            if (yeralist.Count > 0)
            {
                foreach (var p in yeralist)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = p.ReleaseYear.ToString(),
                        Text = p.ReleaseYear.ToString(),
                    });
                }
            }
            list.Insert(0, new SelectListItem { Text = "Please Select", Value = "" });
            ViewBag.Userlist = list;
            return ViewBag.Userlist;
        }

        public List<Movies> LoadMovieInfo(string info, string Category, string Gid, string year)
        {
            string query = "";
            List<Movies> LoadMovie = new List<Movies>();
            if(Category != null)
            {
                query = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies where Category = '" + Category + "' order by Ratings desc";
            }
            else if(Gid != null)
            {
                query = @" Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies inner join 
 MovieGenre on Movies.Mid = MovieGenre.Mid where MovieGenre.Gid = " + Gid;
            }
            else if(year != null)
            {
                query = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by ReleaseYear desc, Mid desc";
            }
            else if(info != null)
            {
                if (info == "Most-Recent")
                {
                    query = @"Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by ReleaseYear desc, Mid desc";
                }
                else if (info == "Most-Popular")
                {
                    query = @"Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by Ratings desc";
                }
                else if (info == "Most-Viewed")
                {
                    query = @"Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by NoOfView desc";
                }
                else if (info == "Suggested-For-you")
                {
                    if (Session["userName"] == null || Session["userName"].ToString() == "")
                    {
                        query = @"Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by ReleaseYear desc, Mid desc";
                    }
                    else
                    {
                        query = @"select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings 
                        from Movies where Mid in ((select distinct Mid from MovieGenre where MovieGenre.Gid in(
                        select Gid from UserChoice where Uid = " + Session["id"].ToString() + " )) except (select Mid from ViewHistory where ViewHistory.Uid = " + Session["id"].ToString() + ")) order by Movies.ReleaseYear desc";

                    }

                }
            }
            else
            {
                query = "Select Movies.Mid,Movies.Name,Movies.Category,Movies.PosterURL,Movies.ReleaseYear,Movies.Ratings from Movies order by ReleaseYear desc, Mid desc";
            }
           

            var Movielist = db.Database.SqlQuery<Movies>(query).ToList();
            if (Movielist.Count > 0)
            {
                foreach (var p in Movielist)
                {
                    Movies list_p = new Movies();
                    list_p.Mid = p.Mid;
                    list_p.Name = p.Name;
                    list_p.PosterURL = p.PosterURL;
                    list_p.Category = p.Category;
                    list_p.ReleaseYear = p.ReleaseYear;
                    list_p.Ratings = p.Ratings;
                    list_p.MovieGenre = load_popular_every_genre_byId(p.Mid.ToString());
                    LoadMovie.Add(list_p);
                }
            }
            return LoadMovie;
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
    }
}