using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMvc.Models
{
    //public class Connection
    //{
    //}
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DBConnection")
        {
        }

    }
}