using Microsoft.EntityFrameworkCore;
using TobeDev.FirstProject.Web.Data.Entity;

namespace TobeDev.FirstProject.Web
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        {
        }
        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.FirstName)
                    .HasMaxLength(20);
                entity.Property(c => c.LastName)
                    .HasMaxLength(20);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
