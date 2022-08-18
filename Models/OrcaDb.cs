using Microsoft.EntityFrameworkCore;

namespace OrcaApi.Models
{
    public class OrcaDb : DbContext
    {
        public OrcaDb(DbContextOptions<OrcaDb> options) : base(options)
        {

        }

        public DbSet<Cinema> Cinemas { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        
    }
}