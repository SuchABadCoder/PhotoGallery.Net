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
    }
}