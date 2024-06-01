namespace YPlanning.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public DateOnly ClassDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Test>? Tests { get; set; }
    }
}
