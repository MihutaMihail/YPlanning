using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class ClassDto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("classdate")]
        public DateOnly ClassDate { get; set; }

        [Column("startime")]
        public TimeOnly StartTime { get; set; }

        [Column("endtime")]
        public TimeOnly EndTime { get; set; }
    }
}
