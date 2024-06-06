using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lastname")]
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Column("firstname")]
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [Column("birthdate")]
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime? BirthDate { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Column("phonenumber")]
        public string? PhoneNumber { get; set; }

        [Column("role")]
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

        public Account? Account { get; set; }
        public ICollection<Test>? Tests { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
