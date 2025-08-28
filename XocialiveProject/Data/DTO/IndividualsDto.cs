namespace XocialiveProject.Data.DTO
{
	public class IndividualsDto : ParticipantDto
	{
		public string University {  get; set; }
		public int YearOfGraduation { get; set; }
		public bool IsIntern {  get; set; }
	}
}
