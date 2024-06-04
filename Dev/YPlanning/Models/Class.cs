using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Class
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("classdate")]
        public DateTime ClassDate { get; set; }

        [Column("starttime")]
        public TimeSpan StartTime { get; set; }

        [Column("endtime")]
        public TimeSpan EndTime { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Test>? Tests { get; set; }
    }
}
