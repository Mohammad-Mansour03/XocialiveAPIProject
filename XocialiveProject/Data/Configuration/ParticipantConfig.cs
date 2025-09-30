using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Data.Configuration
{
	public class ParticipantConfig : IEntityTypeConfiguration<Particpant>
	{
		public void Configure(EntityTypeBuilder<Particpant> builder)
		{
			builder.ToTable("Particpants");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.FName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(x => x.LName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50)
				.IsRequired();
			
		}
	}

}
