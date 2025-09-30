namespace MoalejilAcademy.Models
{
	public class Section
	{
		public int Id { get; set; }
		public string SectionName { get; set; }
		
		public DateSlot? DateSlot { get; set; }
		public TimeSlot? TimeSlot { get; set; }

		public int CourseId {  get; set; }
		public Course Course { get; set; }

		public int ScheduleId {  get; set; }
		public Schedule Schedule { get; set; }

		public int ?InstructorId {  get; set; }
		public Instructor ?Instructor { get; set; }

		public ICollection<Particpant> Particpants { get; set; } = new List<Particpant>();
		public ICollection<Enrollment1> Enrollments { get; set; } = new List<Enrollment1>();

	}
	public class DateSlot
	{
		public DateOnly StartDate { get; set; }
		public DateOnly EndDate { get; set; }
	}

	public class TimeSlot
	{
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
	}
}
