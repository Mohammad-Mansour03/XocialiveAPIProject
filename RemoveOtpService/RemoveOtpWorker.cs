using Microsoft.Data.SqlClient;

namespace RemoveOtpService
{
	public class RemoveOtpWorker : BackgroundService
	{
		private readonly ILogger<RemoveOtpWorker> _logger;
		private readonly IConfiguration _configuration;

		public RemoveOtpWorker(ILogger<RemoveOtpWorker> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{

			_logger.LogWarning($"Remove Otp Worker started at {DateTime.UtcNow}");

			try
			{
				string? connectionString = _configuration.GetConnectionString("DefaultConnection");
				int intervalWait = _configuration.GetValue<int>("OtpCleanupSettings:IntervalSeconds");

				while (!stoppingToken.IsCancellationRequested)
				{
					try
					{
						using (SqlConnection connection = new SqlConnection(connectionString))
						{
							var date = DateTime.UtcNow;

							await connection.OpenAsync();

							var query = "Delete From OtpCodes where ExpiryDate < @expiryDate";

							using (SqlCommand command = new SqlCommand(query, connection))
							{
								command.Parameters.AddWithValue("expiryDate", date);

								var rowsAffected = await command.ExecuteNonQueryAsync(stoppingToken);

								if (rowsAffected > 0)
								{
									_logger.LogInformation($"{rowsAffected} expired OTP codes deleted at: {DateTime.Now}");
								}
							}
							await Task.Delay(TimeSpan.FromSeconds(intervalWait), stoppingToken);
						}
					}
					catch (Exception ex) 
					{
						_logger.LogError($"There is error happened {ex} at {DateTime.UtcNow}");
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"There is an error happened at {DateTime.Now} , with Exception Message {ex}");
			}
		}
	}
}
