namespace XocialiveProject.Models
{
	public class Course 
	{
		public int Id { get; set; }
		public string CourseName { get; set; }
		public double Price { get; set; }
		public int HoursToComplete {  get; set; }

		public ICollection<Section> Sections { get; set; } = new List<Section>();

	}
}
