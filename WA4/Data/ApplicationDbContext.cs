using WA4.Models;
using Microsoft.EntityFrameworkCore;

namespace WA4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories1 { get; set; }
    }
}
