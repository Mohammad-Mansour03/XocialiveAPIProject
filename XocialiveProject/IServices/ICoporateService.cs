using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.IServices
{
	public interface ICoporateService
	{
		 Task<ApiResponse<List<CoporateDto>>> GetCoporatesAsync();
		 Task<ApiResponse<CoporateDto>> GetCoporate(int id);
	    Task<ApiResponse<CoporateDto>> AddCoporate(CoporateDto cooporatesDto);
		Task<ApiResponse<CoporateDto>> Remove(int id);
		Task<ApiResponse<bool>> Update(CoporateDto cooporatesDto);
	}
}
