using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class TokenDto
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("value")]
        [Required(ErrorMessage = "Value is required")]
        public string? Value { get; set; }

        [Column("role")]
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

        [Column("userid")]
        [Required(ErrorMessage = "User id is required")]
        public int? UserId { get; set; }
    }
}
