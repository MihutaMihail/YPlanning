using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class ClassDto
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string? Subject { get; set; }

        [Column("classdate")]
        [Required(ErrorMessage = "Class date id is required")]
        public DateTime? ClassDate { get; set; }

        [Column("starttime")]
        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan? StartTime { get; set; }

        [Column("endtime")]
        [Required(ErrorMessage = "End time is required")]
        public TimeSpan? EndTime { get; set; }

        [Column("room")]
        [Required(ErrorMessage = "Room is required")]
        public string? Room { get; set; }
    }
}
