using PhotoGallery.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class HomeController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static string category = "All";

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            ViewBag.photos = db.Photos.ToList(); ;
            return View(categories);
        }

        [HttpGet]
        public ActionResult Filter(string categoryName)
        {
            var owner = User.Identity.Name;

            if (categoryName == "All")
            {
                ViewBag.photos = db.Photos.ToList(); 
                category = categoryName;
            }
            else if (categoryName == "Rate")
            {
                if (category == "All")
                    ViewBag.photos = Rate(db.Photos.ToList());
                else if (category == "My")
                    ViewBag.photos = Rate(db.Photos.Where(p => p.OwnerName == owner).ToList());
                else
                    ViewBag.photos = Rate(db.Photos.Where(p => p.Category.Name == category).ToList());
            }
            else if (categoryName == "My")
            {
                ViewBag.photos = db.Photos.Where(p => p.OwnerName == owner).ToList();
                category = categoryName;
            }
            else
            {
                ViewBag.photos = db.Photos.Where(p => p.Category.Name == categoryName).ToList();
                category = categoryName;
            }

            var categories = db.Categories.ToList();

            return View("Index", categories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This website allows you to publish images and estimate other people publications.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can share ideas and suggestions for improving the site here:";

            return View();
        }

        private List<Photo> Rate(List<Photo> photos)
        {
            photos.Sort(new PhotosComperer());

            return photos;
        }

        public ActionResult Admin()
        {
            var photos = db.Photos.ToList();
            return View(photos);
        }

        public JsonResult Delete(int id)
        {
            var bd = new ApplicationDbContext();
            var photo = bd.Photos.Where(p => p.Id == id).FirstOrDefault();
            bd.Photos.Remove(photo);
            bd.SaveChanges();
             
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}