using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.IServices
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
