using Microsoft.EntityFrameworkCore;

namespace OrcaApi.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
    }

    class OrcaDb : DbContext
    {
        public OrcaDb(DbContextOptions<OrcaDb> options) : base(options)
        {

        }

        public DbSet<Cinema> Cinemas { get; set; } = null!;
    }
}