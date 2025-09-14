using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XocialiveProject.Data.DTO
{
	public class InstructorDto
	{
		[NotMapped]
		public int Id { get; set; }

		[Required]
		public string FName {  get; set; }
		[Required]
		public string LName { get; set; }

		public int ? OfficeId { get; set; }
		public string ? OfficeName {  get; set; }
	}
}
