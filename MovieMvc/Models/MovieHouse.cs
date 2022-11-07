using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMvc.Models
{
    public class MovieHouse
    {
    }
    public class Movies
    {
        public int Mid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }
        public string PhotoURL { get; set; }
        public string ReleaseYear { get; set; }
        public int Runtime { get; set; }
        public string StudioName { get; set; }
        public string Ratings { get; set; }
        public int NoOfReview { get; set; }
        public int NoOfView { get; set; }
        public string TrailerURL { get; set; }
        public string StoryLine { get; set; }
        public string EmbedURL { get; set; }
        public string Language { get; set; }
        public string MovieGenre { get; set; }
    }

    public class MoviesGen
    {
        public int Mid { get; set; }
        public int Gid { get; set; }
        public string Title { get; set; }
        
    }
    public class Reviews
    {
        public int Mid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }
        public string PhotoURL { get; set; }
        public string ReleaseYear { get; set; }
        public int Runtime { get; set; }
       
        public string Ratings { get; set; }
        public int NoOfReview { get; set; }
        public int NoOfView { get; set; }
      
        public string Language { get; set; }
        public string MovieGenre { get; set; }

        public int Rid { get; set; }
        public string Rating { get; set; }
        public string ReviewDate { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string MovieName { get; set; }
    }

    public class Categorylist
    {
        public int Count { get; set; }
        public string Category { get; set; }
    }
    public class Genrelist
    {
        public int Gid { get; set; }
        public string Title { get; set; }
        public int MovieCount { get; set; }
    }
    public class Year
    {
        public string ReleaseYear { get; set; }
    }

    public class User
    {
        public int Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhotoURL { get; set; }
        public string CreationDate { get; set; }

       // Name,Email,Password,Gender,Phone,PhotoURL,CreationDate,Status

    }
}