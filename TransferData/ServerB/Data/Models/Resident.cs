using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerB.Data.Models
{
	public class Resident
	{
		public Resident()
		{
            this.ProgressNotes = new List<ProgressNote>();
		}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DoB { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        [JsonIgnore]
        public Facility? Facility { get; set; }
        [JsonIgnore]
        public List<ProgressNote>? ProgressNotes { get; set; }
    }
}

