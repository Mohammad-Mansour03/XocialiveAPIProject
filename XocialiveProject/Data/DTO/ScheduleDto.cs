using XocialiveProject.Enum;

namespace XocialiveProject.Data.DTO
{
	public class ScheduleDto
	{
		public int Id { get; set; }
		public string ? ScheduleType { get; set; }
		public bool SUN {  get; set; }
		public bool MON {  get; set; }
		public bool TUE {  get; set; }
		public bool WED {  get; set; }
		public bool THU {  get; set; }
		public bool FRI {  get; set; }
		public bool SAT {  get; set; }
	}
}
