using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace QueueHospital.DAL
{


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
			
        }

		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<Visit> Visits { get; set; }
		public DbSet<TimeTable> TimeTables { get; set; }

		//protected override void OnModelCreating( DbModelBuilder modelBuilder )
		//{
		//	modelBuilder.Entity<Doctor>().HasRequired(m => m.Speciality).WithMany().HasForeignKey()
		//	base.OnModelCreating( modelBuilder );
		//}
    }
}