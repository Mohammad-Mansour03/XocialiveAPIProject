using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Enum;
using XocialiveProject.Models;
using System;
using System.Reflection.Emit;
using System.ComponentModel;

namespace XocialiveProject.Data.Configuration
{
	public class OfficeConfig : IEntityTypeConfiguration<Office>
	{
		public void Configure(EntityTypeBuilder<Office> builder)
		{
			builder.ToTable("Offices");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.OfficeName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(x => x.OfficeLocation)
				.HasColumnType("VARCHAR")
				.HasMaxLength(100)
				.IsRequired();

		}
	}


}
