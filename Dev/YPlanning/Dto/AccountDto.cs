using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class AccountDto
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("login")]
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Column("userid")]
        [Required(ErrorMessage = "User id is required")]
        public int? UserId { get; set; }
    }
}
