using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerA.Data.Models
{
	public class ProgressNote
	{
        [Column("id")]
        public int Id { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("type")]
        public string? Type { get; set; }
        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("Resident")]
        [Column("resident_id")]
        public int ResidentId { get; set; }

        [JsonIgnore]
        public Resident Resident { get; set; }
    }
}

