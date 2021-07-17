using PhotoGallery.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class AddPhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Photo model, HttpPostedFileBase uploadImage)
        {
            byte[] img = null;
            if (uploadImage != null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    img = binaryReader.ReadBytes(uploadImage.ContentLength);
                    model.Image = img;
                }
                var ct = Request.Form["category"];
                if (Request.Form["category"] != "none")
                {
                    var category_name = Request.Form["category"];
                    model.Category = db.Categories.Where(c => c.Name == category_name).FirstOrDefault();
                }
                else if (Request.Form["new_category"] != "")
                {
                    var category = new Category { Name = Request.Form["new_category"] };
                    model.Category = category;
                    db.Categories.Add(category);
                }
                else
                    model.Category = db.Categories.Where(c => c.Name == "None").FirstOrDefault();

                db.Photos.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}