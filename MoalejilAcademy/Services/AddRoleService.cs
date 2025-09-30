using Microsoft.AspNetCore.Identity;
using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Entities;
using MoalejilAcademy.IServices;

namespace MoalejilAcademy.Services
{
	public class AddRoleService :IAddRoleService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AddRoleService(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;		
			_roleManager = roleManager;
		}

		private async Task<AppUser?> ConvertUser(UserDto user) 
		{
			if (user == null)
				return null;

			var appUser = await _userManager.FindByNameAsync(user.UserName);
			
			if (appUser == null) 
				return null;

			if(await _userManager.CheckPasswordAsync(appUser , user.Password))
				return appUser;

			return null;
		}

		public async Task<ApiResponse<bool>> AddUserRole(UserDto user, string roleName)
		{
			if (user == null) 
			{
				var checkExist = await _roleManager.RoleExistsAsync(roleName);
			
				if(checkExist == false) 
				{
					await _roleManager.CreateAsync(new IdentityRole(roleName));
					return new ApiResponse<bool>(true, "The Role Added to the roles", true);
				}

				return new ApiResponse<bool>(true , "The Role Already Exist" , true);
			}

			var appUser = await ConvertUser(user);

			if (appUser == null)
				return new ApiResponse<bool>(false, "The User was not exist", false);

			var flag = await _userManager.IsInRoleAsync(appUser , roleName);

			if (flag)
				return new ApiResponse<bool>(true , "The User Already have this role" , true);

			var roleFlag = await _roleManager.RoleExistsAsync(roleName);

			if (roleFlag) 
			{
				await _userManager.AddToRoleAsync(appUser , roleName);
				return new ApiResponse<bool>(true, "The Role Added to the user correctly", true);
			}

			await _roleManager.CreateAsync(new IdentityRole(roleName));
			await _userManager.AddToRoleAsync(appUser, roleName);
			return new ApiResponse<bool>(true, "The Role Added to the user correctly", true);
		}
	}
}
