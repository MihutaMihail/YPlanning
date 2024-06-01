namespace YPlanning.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int MemberId { get; set; }

        public Member? Member { get; set; }
    }
}
