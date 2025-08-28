using System.ComponentModel.DataAnnotations;

namespace XocialiveProject.Data.DTO
{
	public class RegisterDto
	{
		public string UserName {  get; set; }
		
		public string Password { get; set; }
	
		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string ? PhoneNumber {  get; set; }
	}
}
