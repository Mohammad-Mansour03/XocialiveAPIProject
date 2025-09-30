using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoalejilAcademy.Entities;

namespace MoalejilAcademy.Data.Configuration
{
	public class OtpCodesConfig : IEntityTypeConfiguration<OtpCodes>
	{
		public void Configure(EntityTypeBuilder<OtpCodes> builder)
		{
			builder.ToTable("OtpCodes");
		}
	}
}
