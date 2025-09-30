using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Data.Configuration
{
	public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
	{
		public void Configure(EntityTypeBuilder<Schedule> builder)
		{
			builder.ToTable("Schedules");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.ScheduleType)
				.HasConversion<string>();
				
		}
	}

}
