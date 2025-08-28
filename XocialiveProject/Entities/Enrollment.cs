using System.ComponentModel.DataAnnotations;

namespace XocialiveProject.Models
{
	public class Enrollment1
	{
		public int SectionId {  get; set; }
		public int ParticipantId {  get; set; }

		[Required]
		public Section Section { get; set; }

		[Required]
		public Particpant Particpant { get; set; }
	}

}
