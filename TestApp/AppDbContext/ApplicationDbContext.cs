using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<R_CURRENCY> r_currency { get; set; }
    }
}
