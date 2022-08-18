namespace OrcaApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int? TmdbId { get; set; }
        public string? Title { get; set; }
        public string? Tagline { get; set; }
        public string? Overview { get; set; }
        public int? Runtime { get; set; }
        public string? PosterUrl { get; set; }
        public string? BackdropUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}