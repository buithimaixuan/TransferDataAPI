using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;

namespace ServerA.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ProgressNote> ProgressNotes { get; set; }

    }
}
