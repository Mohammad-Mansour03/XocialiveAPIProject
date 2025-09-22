using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Services;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _service;
		private readonly IAddRoleService _roleService;
		private readonly IEmailService emailService;


		public AccountController(IAccountService service, IAddRoleService roleService, IEmailService service1)
		{
			_service = service;
			_roleService = roleService;
			emailService = service1;
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
			//var result = await _service.LoginUser(logiDto);

			//if (result.Success)
			//	return Ok(result.Data);

			//if (result.Message.Contains("Invalid UserName or Passowrd"))
			//	return Unauthorized(result.Message);

			//return BadRequest(result);


			var result = await _service.LoginUser(logiDto);

			if (result.Success)
				return Ok(new { UserData = result.Data, Message = "OTP sent to your email" });

			return Unauthorized(result.Message);
		}

		[HttpPost("VerifyOtp")]
		public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
		{
			var result = await _service.VerifyOtp(dto.UserId, dto.OtpCode);

			if (result.Success)
				return Ok(result.Data);

			return Unauthorized(result.Message);
		}

		[HttpPost("{roleName}")]
		public async Task<IActionResult> AddRole(UserDto? user, string roleName)
		{
			var result = await _roleService.AddUserRole(user, roleName);

			if (result.Success)
				return Ok(result.Message);

			return BadRequest(result.Message);
		}


	}

}
