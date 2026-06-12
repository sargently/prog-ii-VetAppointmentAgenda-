using Microsoft.EntityFrameworkCore;
using VetAppointmentAgenda.Api.Models.Entities;

namespace VetAppointmentAgenda.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
    }
}
