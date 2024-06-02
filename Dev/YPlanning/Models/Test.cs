using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Models
{
    public class Test
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("classid")]
        public int ClassId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("score")]
        public string? Score { get; set; }

        public Class? Class { get; set; }
        public User? User { get; set; }
    }
}
