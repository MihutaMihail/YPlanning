using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Account
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Column("accountcreationdate")]
        public DateTime? AccountCreationDate { get; set; }

        [Column("lastlogindate")]
        public DateTime? LastLoginDate { get; set; }

        [Column("userid")]
        [Required(ErrorMessage = "User id is required")]
        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}
