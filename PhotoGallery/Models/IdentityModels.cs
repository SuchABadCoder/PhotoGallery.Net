using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoGallery.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public IList<Photo> PhotosLiked { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void CreateAdmin(ApplicationUser user)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(this));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this));

            //var adminRole = new IdentityRole { Name = "admin" };
            //var userRole = new IdentityRole { Name = "user" };

            //roleManager.Create(userRole);
            //roleManager.Create(adminRole);

            //var admin = new ApplicationUser { Email = "Gayday57@gmail.com", UserName = "IamRick" };
           // var result = userManager.Create(admin, "Y@rik2000");

            //if (result.Succeeded)
                userManager.AddToRole(user.Id, "admin");
        }
    }

}