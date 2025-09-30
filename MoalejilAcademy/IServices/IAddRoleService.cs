using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Entities;

namespace MoalejilAcademy.IServices
{
	public interface IAddRoleService
	{
		public Task<ApiResponse<bool>> AddUserRole(UserDto appUser , string  roleName);
	}
}
