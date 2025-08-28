using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XocialiveProject.Data;
using XocialiveProject.Entities;
using XocialiveProject.IServices;
using XocialiveProject.Repository;
using XocialiveProject.Services;

namespace XocialiveProject.Extension
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services 
			, IConfiguration configuration) 
		{
			services.AddDbContext<AppDbContext>
			(
				o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
			);

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IOfficeService, OfficeService>();
			services.AddScoped<IInstructorService, InstructorService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<ISecheduleService, ScheduleService>();
			services.AddScoped<IIndividualService, IndividualService>();
			services.AddScoped<ICoporateService, CoporateService>();
			services.AddScoped<ISectionService, SectionService>();
			services.AddScoped<IParticipantService, ParticipantService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IAddRoleService, AddRoleService>();
			services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


			return services;
		}
	}
}
