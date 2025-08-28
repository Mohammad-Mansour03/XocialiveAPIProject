using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Models;

namespace XocialiveProject.Data.Configuration
{
	public class IndividualConfig : IEntityTypeConfiguration<Individuals>
	{
		public void Configure(EntityTypeBuilder<Individuals> builder)
		{
			builder.ToTable("Individuals");

			builder.Property(x => x.University)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();
			
		}
	}	
}
