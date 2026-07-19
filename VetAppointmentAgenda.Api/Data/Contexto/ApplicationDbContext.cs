using Microsoft.EntityFrameworkCore;
using VetAppointmentAgenda.Api.Data.Entidades;

namespace VetAppointmentAgenda.Api.Data.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Veterinarian> Veterinarians { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Veterinarian)
                .WithMany(v => v.Pets)
                .HasForeignKey(p => p.VeterinarianId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
