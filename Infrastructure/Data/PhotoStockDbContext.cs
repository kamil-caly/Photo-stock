using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PhotoStockDbContext : DbContext
    {
        public PhotoStockDbContext(DbContextOptions<PhotoStockDbContext> options) : base(options)   
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
