using BackEndTesteABnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndTesteABnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment> Assignments { get; set; }
    }
}
