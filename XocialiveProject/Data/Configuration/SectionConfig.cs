using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Models;

namespace XocialiveProject.Data.Configuration
{
	public class SectionConfig : IEntityTypeConfiguration<Section>
	{
		public void Configure(EntityTypeBuilder<Section> builder)
		{
			builder.ToTable("Sections");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.SectionName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(150)
				.IsRequired();

			builder.OwnsOne(x => x.TimeSlot
			, ts =>
			{
				ts.Property(x => x.StartTime).HasColumnName("StartTime").HasColumnType("time(0)").IsRequired();
				ts.Property(x => x.EndTime).HasColumnName("EndTime").HasColumnType("time(0)").IsRequired();
			});

			builder.OwnsOne(x => x.DateSlot , 
				ds => 
				{
					ds.Property(x => x.StartDate).HasColumnName("StartDate").HasColumnType("Date").IsRequired();
					ds.Property(x => x.EndDate).HasColumnName("EndDate").HasColumnType("Date").IsRequired();
				}
				);

			//The Relations 

			//Between Section and Insrtuctor (One from Section and Many from Instrcutor optional)
			builder.HasOne(x => x.Instructor)
				.WithMany(x => x.Sections)
				.HasForeignKey(x => x.InstructorId)
				.IsRequired(false);

			//Between Section and Courses (One from Section and Many from Courses)
			builder.HasOne(x => x.Course)
				.WithMany(x => x.Sections)
				.HasForeignKey(x => x.CourseId)
				.IsRequired();

			//Between Section and Schedule(One from Section and Many from Schedule)
			builder.HasOne(x => x.Schedule)
				.WithMany(x=> x.Sections)
				.HasForeignKey(x => x.ScheduleId)
				.IsRequired();

			//Between Section and Particpant Many To Many with using Enrollment Table as bridge table
			builder.HasMany(x => x.Particpants)
				.WithMany(x => x.Sections)
				.UsingEntity<Enrollment1>
				(
					l => l.HasOne(x => x.Particpant)
					.WithMany(x => x.Enrollments)
					.HasForeignKey(x => x.ParticipantId),
					r => r.HasOne(x => x.Section)
					.WithMany(x => x.Enrollments)
					.HasForeignKey(x => x.SectionId)
				);

		}
	}


}
