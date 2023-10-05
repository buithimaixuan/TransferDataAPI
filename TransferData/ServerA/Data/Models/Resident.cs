using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerA.Data.Models
{
	public class Resident
	{
		public Resident()
		{
            this.ProgressNotes = new List<ProgressNote>();
		}
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        [Column("dob")]
        public DateTime? DoB { get; set; }

        [ForeignKey("Facility")]
        [Column("facility_id")]
        public int FacilityId { get; set; }

        [JsonIgnore]
        public Facility Facility { get; set; }
        [JsonIgnore]
        public List<ProgressNote> ProgressNotes { get; set; }
    }
}

