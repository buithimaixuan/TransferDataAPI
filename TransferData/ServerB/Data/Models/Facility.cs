using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerB.Data.Models
{
	public class Facility
	{
		public Facility(){
			this.Residents = new List<Resident>();
		}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

		[JsonIgnore]
		public List<Resident>? Residents { get; set; }
    }
}

