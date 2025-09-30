using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.IServices
{
	public interface IIndividualService
	{
		public Task<ApiResponse<List<IndividualsDto>>> GetIndividualsAsync();
		public Task<ApiResponse<IndividualsDto>> GetIndividualAsync(int id);
		public Task<ApiResponse<IndividualsDto>> Add(IndividualsDto individualsDto);
		public Task<ApiResponse<bool>> Update(IndividualsDto individualsDto);
		public Task<ApiResponse<IndividualsDto>> Delete(int id);
	}
}
