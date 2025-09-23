namespace RemoveOtpService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Host.CreateDefaultBuilder(args)
				.UseWindowsService(options => 
				{
					options.ServiceName = "RemoveOtpCode";
				})
				.ConfigureServices(services => 
				{
					services.AddHostedService<RemoveOtpWorker>();
				})
				.Build();

			builder.Run();
		}
	}
}