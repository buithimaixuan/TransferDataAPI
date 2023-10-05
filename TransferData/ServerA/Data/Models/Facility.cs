using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerA.Data.Models
{
	public class Facility
	{
		public Facility(){
			this.Residents = new List<Resident>();
		}
		[Column("id")]
		public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("address")]
        public string? Address { get; set; }

		[JsonIgnore]
		public List<Resident> Residents { get; set; }
    }
}

