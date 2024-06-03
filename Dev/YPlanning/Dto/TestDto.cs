using System.ComponentModel.DataAnnotations.Schema;

namespace YPlanning.Dto
{
    public class TestDto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("classid")]
        public int ClassId { get; set; }
        
        [Column("userid")]
        public int UserId { get; set; }

        [Column("score")]
        public string? Score { get; set; }
    }
}
