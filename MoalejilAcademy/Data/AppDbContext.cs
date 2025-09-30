using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoalejilAcademy.Entities;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{

		public AppDbContext(DbContextOptions<AppDbContext> option) : base (option) { }

		public DbSet<Office> Offices { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Particpant> Particpants { get; set; }
		public DbSet<Individuals> Individuals { get; set; }
		public DbSet<Coporates> Coporates { get; set; }
		public DbSet<OtpCodes> OtpCodes { get; set; }
		



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}

	}
}
