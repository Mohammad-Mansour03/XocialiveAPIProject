using XocialiveProject.Data.DTO;
using XocialiveProject.Entities;

namespace XocialiveProject.IServices
{
	public interface IAddRoleService
	{
		public Task<ApiResponse<bool>> AddUserRole(UserDto appUser , string  roleName);
	}
}
