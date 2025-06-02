using E_commerce.Server.Migrations;
using E_commerce.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Server.data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        DbSet<Books> Books { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<BookImg> BookImgs { get; set; }

    }
}
