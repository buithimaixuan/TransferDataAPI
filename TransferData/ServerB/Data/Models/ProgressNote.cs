using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerB.Data.Models
{
	public class ProgressNote
	{
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("Resident")]
        public int ResidentId { get; set; }

        [JsonIgnore]
        public Resident? Resident { get; set; }
    }
}

