using PhotoGallery.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            ViewBag.photos = db.Photos.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Filter(string categoryName)
        {
            if (categoryName == "All")
                ViewBag.photos = db.Photos.ToList();
            else if (categoryName == "Rate")
                ViewBag.photos = Rate(db.Photos.ToList());
            else
                ViewBag.photos = db.Photos.Where(p => p.Category.Name == categoryName).ToList();

            var categories = db.Categories.ToList();

            return View("Index", categories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Photo> Rate(List<Photo> photos)
        {
            photos.Sort(new PhotosComperer());

            return photos;
        }
    }
}