using XocialiveProject.Data.DTO;
using XocialiveProject.Entities;

namespace XocialiveProject.IServices
{
	public interface IAccountService
	{
		Task<ApiResponse<RegisterDto>> RegisterUser(RegisterDto registerDto);
		Task<ApiResponse<Token>> LoginUser(LogiDto loginDto);
	}
}
