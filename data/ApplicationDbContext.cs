using E_commerce.Server.Migrations;
using E_commerce.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace E_commerce.Server.data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //    if (!context.Database.GetService<IRelationalDatabaseCreator>().Exists())
            //    {
            //        context.Database.Migrate();
            //    }
            //}

        }

        DbSet<Books> Books { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<BookImg> BookImgs { get; set; }

    }
}
