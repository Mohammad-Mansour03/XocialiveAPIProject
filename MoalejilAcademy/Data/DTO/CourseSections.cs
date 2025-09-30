namespace MoalejilAcademy.Data.DTO
{
	public class CourseSections
	{
		public int CourseId {  get; set; }
		public string ? CourseName { get; set; }
		public int HoursToComplete {  get; set; }
		public double Price {  get; set; }

		public List<SectionDto> ?Sections { get; set; }
	}
}
