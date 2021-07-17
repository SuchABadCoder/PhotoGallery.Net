using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace PhotoGallery.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<Photo> Photos { get; set; }

        public Category()
        {
            Photos = new List<Photo>();
        }
    }

    public class Photo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string OwnerName { get; set; }
        public IList<ApplicationUser> Likes { get; set; }

        public Photo()
        {
            Likes = new List<ApplicationUser>();
        }

        public int LikesCount()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM dbo.ApplicationUserPhotoes WHERE Photo_Id = '{Id}'", connection);
                
                return (int)command.ExecuteScalar();
            }
        }

        public bool IsLiked(string userName)
        {
            if (userName != "")
            {
                var db = new ApplicationDbContext();
                var user = db.Users.Where(u => u.Email == userName).FirstOrDefault();
                var userId = user.Id;

                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM dbo.ApplicationUserPhotoes WHERE ApplicationUser_Id = '{userId}' AND Photo_Id = '{Id}'", connection);
                    var res = (int)command.ExecuteScalar();

                    if (res > 0)
                        return true;
                }
            }

            return false;
        }
    }
    
    public class PhotosComperer : IComparer<Photo>
    {
        public int Compare(Photo p1, Photo p2)
        {
            if (p1.LikesCount() > p2.LikesCount())
                return -1;
            else if (p1.LikesCount() < p2.LikesCount())
                return 1;
            else
                return 0;
        }
    }
}