using XocialiveProject.Middleware;

namespace XocialiveProject.Extension
{
	public static class AppBuilderExtension
	{
		public static IApplicationBuilder AddGlobalErrorHandlingMiddleware(this IApplicationBuilder app) 
		{
			return app.UseMiddleware<GlobalErrorHandlingMiddleware>();
		}
		public static IApplicationBuilder AddRequestLoggingMiddleware(this IApplicationBuilder app) 
		{
			return app.UseMiddleware<RequestLoggingMiddleware>();
		}
	}
}
