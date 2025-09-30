using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MoalejilAcademy.BackgroundServices;
using MoalejilAcademy.Data;
using MoalejilAcademy.Extension;
using MoalejilAcademy.IServices;
using MoalejilAcademy.Middleware;
using MoalejilAcademy.Repository;
using MoalejilAcademy.Services;

namespace MoalejilAcademy
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
