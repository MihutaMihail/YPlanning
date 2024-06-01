namespace YPlanning.Models
{
    public class Test
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int MemberId { get; set; }
        public string? Score { get; set; }

        public Class? Class { get; set; }
        public Member? Member { get; set; }
    }
}
