using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace XocialiveProject.Extension
{
	public static class CustomJwtExtension
	{
		public static void AddCustomJwtAuth(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddAuthentication
				(
					o =>
					{
						o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
						o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
						o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					}
				).AddJwtBearer
				(
					o =>
					{
						o.RequireHttpsMetadata = false;
						o.SaveToken = true;
						o.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateAudience = false,
							ValidateIssuer = true,
							ValidIssuer = configuration["JWT:Issuer"],
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
							GetBytes(configuration["JWT:Secret Key"]))
						};
					}
				);
		}

		public static void AddSwaggerGen(this IServiceCollection services) 
		{
			services.AddSwaggerGen
				(
					o =>
					{
						o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
						{
							Name = "Authorization",
							Type = SecuritySchemeType.ApiKey,
							Scheme = "Bearer",
							BearerFormat = "Auth",
							In = ParameterLocation.Header,
							Description = "Enter The Jwt Token",
						});

						o.AddSecurityRequirement(new OpenApiSecurityRequirement()
						{
							{
								new OpenApiSecurityScheme()
								{
									Reference = new OpenApiReference()
									{
										Type = ReferenceType.SecurityScheme,
										Id = "Bearer"
									},
									Name = "Bearer",
									In = ParameterLocation.Header
								},
								new List<string>() 
							}
						}); 

					}
				);
		}
	}
}
