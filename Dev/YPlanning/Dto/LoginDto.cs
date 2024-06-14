using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YPlanning.Dto
{
    public class LoginDto
    {
        [Column("login")]
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
