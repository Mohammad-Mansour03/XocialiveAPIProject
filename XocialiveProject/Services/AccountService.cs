using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XocialiveProject.Data.DTO;
using XocialiveProject.Entities;
using XocialiveProject.IServices;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class AccountService : IAccountService
	{
		//private readonly IGenericRepository<AppUser> _repository;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IEmailService _emailService;
		private readonly IOtpService _otpService;
		public AccountService(UserManager<AppUser> userManager, IConfiguration configuration, 
			IEmailService emailService, IOtpService otpService)
		{
			_userManager = userManager;
			_configuration = configuration;
			_emailService = emailService;
			_otpService = otpService;
		}

		public async Task<ApiResponse<VerifyOtpDto>> LoginUser(LogiDto loginDto)
		{
			if (loginDto == null)
				new ApiResponse<VerifyOtpDto>(false, "There is no user with this information");

			var appUser = await _userManager.FindByNameAsync(loginDto.UserName);

			if(appUser != null && await _userManager.CheckPasswordAsync(appUser , loginDto.Password)) 
			{
				var otpCode = new Random().Next(100000 , 999999).ToString();

				await _otpService.SaveOtpAsync(appUser.Id, otpCode, TimeSpan.FromMinutes(5));

				await _emailService.SendEmailAsync(appUser.Email, "Your Otp Code", $"Your OTP code is: {otpCode}");

				return new ApiResponse<VerifyOtpDto>(true , " " , new VerifyOtpDto { OtpCode = otpCode , UserId = appUser.Id});
			}

			return new ApiResponse<VerifyOtpDto>(false, "There is no user found");
		}


		//public async Task<ApiResponse<Token>> LoginUser(LogiDto loginDto)
		//{
		//	if (loginDto == null)
		//		return new ApiResponse<Token>(false , "Invalid Login (Dto Was Null)");

		//	var appUser = await _userManager.FindByNameAsync(loginDto.UserName);

		//	if (appUser != null) 
		//	{
		//		if(await _userManager.CheckPasswordAsync(appUser , loginDto.Password))
		//		{
		//			List<Claim> claims= new List<Claim>();
		//			claims.Add(new Claim(ClaimTypes.Name, appUser.UserName!));
		//			claims.Add(new Claim(ClaimTypes.NameIdentifier , appUser.Id));
		//			claims.Add(new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()));

		//			var roles = await _userManager.GetRolesAsync(appUser);

		//			foreach (var role in roles) 
		//			{
		//				claims.Add(new Claim(ClaimTypes.Role, role));
		//			}

		//			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret Key"]!));

		//			var signingCredintials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

		//			var securityToken = new JwtSecurityToken
		//			(
		//				claims : claims,
		//				signingCredentials : signingCredintials,
		//				issuer: _configuration["JWT:Issuer"],
		//				audience: _configuration["JWT:Audience"],
		//				expires:DateTime.Now.AddMinutes(10)
		//			);

		//			var token = new Token
		//			{
		//				TokenString = new JwtSecurityTokenHandler().WriteToken(securityToken),
		//				Expiration = securityToken.ValidTo
		//			};

		//			return new ApiResponse<Token>(true , "Login Success" , token);
		//		}			
		//	}

		//	return new ApiResponse<Token>(false , "Wrong User Name or Password");

		//}



		public async Task<ApiResponse<RegisterDto>> RegisterUser(RegisterDto registerDto)
		{
			if (registerDto == null)
				return new ApiResponse<RegisterDto>(false , "Invalid Request (The Dto Was Null");

			//Initiate new user
			var user = new AppUser()
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				PhoneNumber = registerDto.PhoneNumber is null ? null : registerDto.PhoneNumber.ToString()
			};

			IdentityResult result = await _userManager.CreateAsync(user , registerDto.Password);

			if (result.Succeeded) 
			{
				return new ApiResponse<RegisterDto>(true , "Register Success" , registerDto);
			}

			return new ApiResponse<RegisterDto>(false , "There is a problem during create new user");
		}

		public async Task<ApiResponse<Token>> VerifyOtp(string userId, string otp)
		{
			var appUser = await _userManager.FindByIdAsync(userId);

			if (appUser == null)
				return new ApiResponse<Token>(false, "There is no user with this id");

			var isValid =  _otpService.ValidateOtpAsync(userId , otp);

			if (!isValid.Success)
				return new ApiResponse<Token>(false, "The Token is invalid or expired");

			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, appUser.UserName!),
				new Claim(ClaimTypes.NameIdentifier, appUser.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var roles = await _userManager.GetRolesAsync(appUser);

			foreach (var role in roles)
				claims.Add(new Claim(ClaimTypes.Role, role));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret Key"]!));
		
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var securityToken = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCredentials,
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires: DateTime.Now.AddMinutes(10)
			);

			var token = new Token
			{
				TokenString = new JwtSecurityTokenHandler().WriteToken(securityToken),
				Expiration = securityToken.ValidTo
			};

			return new ApiResponse<Token>(true, "Login Success", token);
		}
	}
}
