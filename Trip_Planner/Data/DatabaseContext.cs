using Microsoft.EntityFrameworkCore;
using Trip_Planner.Models;
namespace Trip_Planner.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
