namespace YPlanning.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        
        public Account? Account { get; set; }
        public ICollection<Test>? Tests { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
