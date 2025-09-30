namespace MoalejilAcademy.Middleware
{
	public class RequestLoggingMiddleware
	{
		private readonly ILogger<RequestLoggingMiddleware> _logger;
		private readonly RequestDelegate _next;

		public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger , RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext) 
		{
			var request = httpContext.Request;

			_logger.LogInformation("Http Rquest: {method} {Url} At {DateTime} " , request.Method , 
				request.Path,DateTime.Now);

			await _next(httpContext);

			var response = httpContext.Response;
			_logger.LogInformation("Http Response: {StatusCode} At {DateTime} ", response.StatusCode,
			 DateTime.Now);

		}
	}
}
