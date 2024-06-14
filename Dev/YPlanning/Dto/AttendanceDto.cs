using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class AttendanceDto
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("classid")]
        [Required(ErrorMessage = "Class id is required")]
        public int? ClassId { get; set; }

        [Column("userid")]
        [Required(ErrorMessage = "User id is required")]
        public int? UserId { get; set; }
        
        [Column("status")]
        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Column("reason")]
        public string? Reason { get; set; }
    }
}
