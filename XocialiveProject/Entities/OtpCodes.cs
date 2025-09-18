namespace XocialiveProject.Entities
{
	public class OtpCodes
	{
		public string Id {  get; set; }
		public string OtpCode {  get; set; }

		public DateTime ExpiryDate { get; set; }
	}
}
