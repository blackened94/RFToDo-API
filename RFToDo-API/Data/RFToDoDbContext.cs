using Microsoft.EntityFrameworkCore;
using RFToDo_API.Models;

namespace RFToDo_API.Data
{
    public class RFToDoDbContext(DbContextOptions<RFToDoDbContext> options) : DbContext(options)
    {
        public DbSet<Meta> Meta { get; set; }
        public DbSet<Tarea> Tarea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional
            modelBuilder.Entity<Meta>().ToTable("Meta");
            modelBuilder.Entity<Tarea>().ToTable("Tarea");
        }
    }
}
