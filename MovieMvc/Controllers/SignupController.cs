using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMvc.Controllers
{
    public class SignupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        public string getCurrentDate()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            return dateTime.ToString("dd/MM/yyyy");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string name, string email, string gender, string Phone, string password)
        {
            try
            {
                string photourl = "";
               

                string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string querycircle = "SELECT * from [User] where Email='" + email + "'";
                    var user = db.Database.SqlQuery<User>(querycircle).ToList();
                    if (user.Count == 0)
                    {
                        if (file.ContentLength > 0)
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string path = Path.Combine(Server.MapPath("/content/images/user/"), Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            photourl = "images/User/" + _FileName;


                            string query = "INSERT INTO [User](Name,Email,Password,Gender,Phone,PhotoURL,CreationDate,Status) values(@Name,@Email,@Password,@Gender,@Phone,@PhotoURL,@CreationDate,@Status)";
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Name", name);
                                cmd.Parameters.AddWithValue("@Email", email);
                                cmd.Parameters.AddWithValue("@Password", password);
                                cmd.Parameters.AddWithValue("@Gender", gender);
                                cmd.Parameters.AddWithValue("@Phone", Phone);
                                cmd.Parameters.AddWithValue("@PhotoURL", photourl);
                                cmd.Parameters.AddWithValue("@CreationDate", getCurrentDate());
                                cmd.Parameters.AddWithValue("@Status", "Pending");
                                cmd.ExecuteScalar();
                            }

                            con.Close();
                        }
                        TempData["msg"] = "success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["msg"] = "email";
                        return RedirectToAction("Index");
                    }

                   
                }


                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}