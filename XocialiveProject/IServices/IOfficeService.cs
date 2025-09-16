using XocialiveProject.Data.DTO;
using XocialiveProject.Models;

namespace XocialiveProject.IServices
{
	public interface IOfficeService
	{
		Task<ApiResponse<List<OfficeDto>>> GetAllOffices();
		Task<ApiResponse<OfficeDto>> GetOffice(int id);

		Task<ApiResponse<OfficeDto>> AddOffice(OfficeDto? office);
		Task<ApiResponse<OfficeDto>> RemoveOffice(int id);
		Task<ApiResponse<bool>> UpdateOffice(OfficeDto office);
		Task<ApiResponse<List<OfficeDto>>> FilteredOffices(string? search, string? orderBy,
			bool descSort = false, int? page = null, int? pageSize = null);
	}
}
