using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Attendance
    {
        [Column("classid")]
        public int ClassId {  get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("reason")]
        public string? Reason { get; set; }

        public User? User { get; set; }
        public Class? Class { get; set; }
    }
}
