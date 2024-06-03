using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class UserDto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phonenumber")]
        public string? PhoneNumber { get; set; }

        [Column("role")]
        public string? Role { get; set; }
    }
}
