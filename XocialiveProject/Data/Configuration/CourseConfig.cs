using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Models;

namespace XocialiveProject.Data.Configuration
{
	public class CourseConfig : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.ToTable("Courses");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.CourseName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(150)
				.IsRequired();

			builder.Property(x => x.Price)
				.HasPrecision(8, 2)
				.IsRequired();

		}
	}
	

}
