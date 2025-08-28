namespace XocialiveProject.Models
{
	public class Particpant 
	{
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public ICollection<Section> Sections { get; set; } = new List<Section>();

		public ICollection<Enrollment1> Enrollments { get; set; } = new List<Enrollment1>();

	}

}
