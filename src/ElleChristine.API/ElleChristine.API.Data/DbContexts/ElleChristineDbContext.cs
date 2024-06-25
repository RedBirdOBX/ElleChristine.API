using ElleChristine.API.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace ElleChristine.API.Data.DbContexts
{
    public class ElleChristineDbContext : DbContext
    {
        public ElleChristineDbContext(DbContextOptions<ElleChristineDbContext> options) : base(options)
        {
        }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Photo> Photos { get; set; }
    }
}
