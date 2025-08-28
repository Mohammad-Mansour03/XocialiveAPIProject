using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Models;

namespace XocialiveProject.Data.Configuration
{
	public class InstructorConfig : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			builder.ToTable("Instructors");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.FName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(x => x.LName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(60)
				.IsRequired();

			builder.HasOne(x => x.Office)
				.WithOne(x => x.Instructor)
				.HasForeignKey<Instructor>(x => x.OfficeId)
				.IsRequired(false);

		}
	}
}
