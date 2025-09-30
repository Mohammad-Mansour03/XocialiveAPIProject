using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Data.Configuration
{
	public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment1>
	{
		public void Configure(EntityTypeBuilder<Enrollment1> builder)
		{
			builder.ToTable("Enrollments");

			builder.HasKey(x => new {x.SectionId , x.ParticipantId});

		}
	}

}
