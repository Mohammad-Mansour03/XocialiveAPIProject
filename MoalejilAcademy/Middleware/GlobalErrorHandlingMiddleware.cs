using System.Diagnostics;
using System.Text.Json;

namespace MoalejilAcademy.Middleware
{
	public class GlobalErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

		public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger , RequestDelegate next)
		{
			_logger = logger;
			_next = next;	
		}

		public async Task InvokeAsync(HttpContext httpContext) 
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception e) 
			{
				_logger.LogError("There is an unhandeled exception happened at {DateTime} " +
					"in the source{Exception Source}"
					, DateTime.Now
					, e.Source);

				httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var responseMessage = new
				{
					StatusCode = httpContext.Response.StatusCode,
					Message = "An UnExpected error occur"
				};

				var jsonMessage = JsonSerializer.Serialize(responseMessage);

				await httpContext.Response.WriteAsync(jsonMessage);

			}
		}

	}
}
