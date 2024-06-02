namespace YPlanning.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int ClassId {  get; set; }
        public int MemberId { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }

        public Member? Member { get; set; }
        public Class? Class { get; set; }
    }
}
