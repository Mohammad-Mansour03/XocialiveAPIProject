using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XocialiveProject.Data.DTO
{
	public class OfficeDto
	{
		[NotMapped]
		public int Id { get; set; }

		[Required]
		public string OfficeName {  get; set; }
		[Required]
		public string OfficeLocation { get; set; }
	}
}
