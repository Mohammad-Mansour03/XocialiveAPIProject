using XocialiveProject.Models;

namespace XocialiveProject.Data.DTO
{
	public class SectionDto
	{
		public int Id { get; set; }
		public string SectionName { get; set; }
		public DateSlot DateSlot { get; set; }
		public TimeSlot TimeSlot { get; set; }
		public int CourseId {  get; set; }
		public int ScheduleId {  get; set; }
		public int ? InstructorId { get; set; }

	}
}
