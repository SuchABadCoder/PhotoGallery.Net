using PhotoGallery.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class ViewPhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int photoId)
        {
            var photo = db.Photos.Where(p => p.Id == photoId).FirstOrDefault();
            return View(photo);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Like(int id)
        {
            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.Email == userName).FirstOrDefault();
            var photo = db.Photos.Where(p => p.Id == id).FirstOrDefault();
            var userId = user.Id;

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                if (photo.IsLiked(userName))
                {
                    SqlCommand command = new SqlCommand($"DELETE FROM ApplicationUserPhotoes WHERE ApplicationUser_Id = '{userId}' AND Photo_Id = '{id}'", connection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    db.Photos.Where(p => p.Id == id).FirstOrDefault().Likes.Add(user);
                    db.SaveChanges();
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int photoId)
        {
            var photo = db.Photos.Where(p => p.Id == photoId).FirstOrDefault();
            db.Photos.Remove(photo);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}