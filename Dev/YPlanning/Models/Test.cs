using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Test
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

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
