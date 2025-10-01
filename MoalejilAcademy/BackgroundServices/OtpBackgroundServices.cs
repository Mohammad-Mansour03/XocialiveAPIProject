using Hangfire;
using MoalejilAcademy.Services;

namespace MoalejilAcademy.BackgroundServices
{
	public static class OtpBackgroundServices
	{
		public static void RemoveOtpService() 
		{
			 RecurringJob.AddOrUpdate<IOtpService>(recurringJobId: "Remove Otp",
				methodCall: x => x.RemoveExpiryOtp(), cronExpression: "*/2 * * * *");
		}
	}
}
