using XocialiveProject.Data.DTO;
using XocialiveProject.Models;

namespace XocialiveProject.IServices
{
	public interface IInstructorService
	{
		Task<ApiResponse<List<InstructorDto>>> GetAll();
		Task<ApiResponse<InstructorDto>> GetById(int id);

		Task<ApiResponse<InstructorDto>> Add(InstructorDto instructor);
		Task<ApiResponse<bool>> Update(InstructorDto instructor);
		Task<ApiResponse<InstructorDto>> Delete(int id);
	}
}
