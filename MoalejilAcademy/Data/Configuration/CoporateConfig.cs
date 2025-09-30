using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Data.Configuration
{
	public class CoporateConfig : IEntityTypeConfiguration<Coporates>
	{
		public void Configure(EntityTypeBuilder<Coporates> builder)
		{
			builder.ToTable("Coporates");


			builder.Property(x => x.Company)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();
			
			builder.Property(x => x.JobTitle)
				.HasColumnType("VARCHAR")
				.HasMaxLength(90)
				.IsRequired();
			
		}
	}
}
