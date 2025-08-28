
using Microsoft.EntityFrameworkCore;
using XocialiveProject.Data;
using XocialiveProject.Extension;
using XocialiveProject.IServices;
using XocialiveProject.Repository;
using XocialiveProject.Services;
using Serilog;

using XocialiveProject.Middleware;
namespace XocialiveProject
{
	public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog
                (
                    (context, configuration) =>
                    {
                        configuration.ReadFrom.Configuration(context.Configuration);
                    }
                );

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddCustomJwtAuth(builder.Configuration);


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddRequestLoggingMiddleware();
            app.AddGlobalErrorHandlingMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}
