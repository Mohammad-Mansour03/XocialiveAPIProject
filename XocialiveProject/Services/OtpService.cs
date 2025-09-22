using System.Collections.Concurrent;
using XocialiveProject.Data;
using XocialiveProject.Data.DTO;
using XocialiveProject.Entities;

namespace XocialiveProject.Services
{
	public class OtpService : IOtpService
	{
		private readonly AppDbContext _context;

		public OtpService(AppDbContext context)
		{
			_context = context;
		}

		public  Task SaveOtpAsync(string userId, string code, TimeSpan duration)
		{
			var checkAdd =  _context.OtpCodes.AddAsync(new OtpCodes { Id = userId, ExpiryDate = DateTime.UtcNow.Add(duration), OtpCode = code });

			_context.SaveChanges();

			return Task.CompletedTask;
		}




		public  ApiResponse<bool> ValidateOtpAsync(string userId, string code)
		{

			var user = _context.OtpCodes.FirstOrDefault(x => x.Id == userId);

			if (user == null)
				return new ApiResponse<bool>(false, "There is no user with this id");

			var otpCode = _context.OtpCodes.Where(x => x.Id == userId).FirstOrDefault(x => x.OtpCode == code);

			if (otpCode == null)
				return new ApiResponse<bool>(false, "The Otp Code not found");

			return new ApiResponse<bool>(true, "", true);
		}
	}

	public interface IOtpService 
	{
		Task SaveOtpAsync(string userId, string code, TimeSpan duration);
		ApiResponse<bool> ValidateOtpAsync(string userId, string code);
	}
}
