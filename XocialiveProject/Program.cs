using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using XocialiveProject.BackgroundServices;
using XocialiveProject.Data;
using XocialiveProject.Extension;
using XocialiveProject.IServices;
using XocialiveProject.Middleware;
using XocialiveProject.Repository;
using XocialiveProject.Services;

namespace XocialiveProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((context, configuration) =>
			{
				configuration.ReadFrom.Configuration(context.Configuration);
			});

			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddCustomJwtAuth(builder.Configuration);
			
			builder.Services.AddHangfire(config =>
				config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
					  .UseSimpleAssemblyNameTypeSerializer()
					  .UseRecommendedSerializerSettings()
					  .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnection"),
						  new SqlServerStorageOptions
						  {
							  PrepareSchemaIfNecessary = true 
						  }));

			builder.Services.AddHangfireServer();

			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(policy =>
				{
					policy.AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowAnyOrigin();
				});
			});

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

		
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.AddRequestLoggingMiddleware();
			app.AddGlobalErrorHandlingMiddleware();

			//  Hangfire Dashboard
			app.UseHangfireDashboard("/hangfire");

			// Register recurring job
			OtpBackgroundServices.RemoveOtpService();

			app.MapControllers();
			app.Run();
		}
	}
}
