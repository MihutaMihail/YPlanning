using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class TestDto
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

        [Column("score")]
        [Required(ErrorMessage = "Score is required")]
        public string? Score { get; set; }
    }
}
