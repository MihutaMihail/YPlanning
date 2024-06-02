using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Account
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        public string? Login { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("accountcreationdate")]
        public DateTime? AccountCreationDate { get; set; }

        [Column("lastlogindate")]
        public DateTime? LastLoginDate { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
