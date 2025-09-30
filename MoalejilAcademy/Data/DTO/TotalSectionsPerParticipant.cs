using System.Globalization;

namespace MoalejilAcademy.Data.DTO
{
	public class TotalSectionsPerParticipant
	{
		public int ParticipantId { get; set; }
		public string ?ParticipantName {  get; set; }
		public int TotalSections {  get; set; }
	}
}
