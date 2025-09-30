using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Entities;

namespace MoalejilAcademy.IServices
{
	public interface IAccountService
	{
		Task<ApiResponse<RegisterDto>> RegisterUser(RegisterDto registerDto);
		//Task<ApiResponse<Token>> LoginUser(LogiDto loginDto);

		Task<ApiResponse<VerifyOtpDto>> LoginUser(LogiDto loginDto);
		Task<ApiResponse<Token>> VerifyOtp(string userId, string otp);
	}
}
