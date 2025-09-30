using System.ComponentModel.DataAnnotations;

namespace MoalejilAcademy.Models
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
