namespace XocialiveProject.Data.DTO
{
	public class SectionWithParticipant
	{
		public int SectionId { get; set; }
		public string SectionName {  get; set; }
		public List<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
	}
}
