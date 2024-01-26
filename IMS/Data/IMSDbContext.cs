using IMS.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Data
{
    public class IMSDbContext : DbContext
    {
        public IMSDbContext(DbContextOptions<IMSDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Insurance> Insurances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.Client)
                .WithMany(c => c.Insurances)
                .HasForeignKey(i => i.ClientId)
                .IsRequired(); // Optional: If every insurance must have an owner, use IsRequired

            // Other configurations...
        }
    }
}
