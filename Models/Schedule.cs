namespace OrcaApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int? CinemaId { get; set; }
        public int? RoomId { get; set; }
        public int? MovieId { get; set; }
        public int? DayOfWeek { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TicketPrice { get; set; }
    }
}