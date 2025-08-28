namespace XocialiveProject.Data.DTO
{
	public class ParticipantAndCourses
	{
		public string ParticipantName {  get; set; }
		public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
	}
}
