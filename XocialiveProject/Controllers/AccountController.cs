using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _service;
		private readonly IAddRoleService _roleService;

		public AccountController(IAccountService service , IAddRoleService roleService)
		{
			_service = service;
			_roleService = roleService;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto registerDto) 
		{
			var result = await _service.RegisterUser(registerDto);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);			
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LogiDto logiDto) 
		{
			var result = await _service.LoginUser(logiDto);

			if(result.Success)
				return Ok(result.Data);

			if(result.Message.Contains("Invalid UserName or Passowrd"))
				return Unauthorized(result.Message);

			return BadRequest(result);
		}

		[HttpPost("{roleName}")]
		public async Task<IActionResult> AddRole(UserDto ?user , string roleName) 
		{
			var result = await _roleService.AddUserRole(user , roleName);

			if(result.Success)
				return Ok(result.Message);

			return BadRequest(result.Message);
		}

	}
}
