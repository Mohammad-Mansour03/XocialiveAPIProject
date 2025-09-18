using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XocialiveProject.Entities;

namespace XocialiveProject.Data.Configuration
{
	public class OtpCodesConfig : IEntityTypeConfiguration<OtpCodes>
	{
		public void Configure(EntityTypeBuilder<OtpCodes> builder)
		{
			builder.ToTable("OtpCodes");
		}
	}
}
