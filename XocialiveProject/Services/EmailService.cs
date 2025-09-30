using System.Net;
using System.Net.Mail;

namespace MoalejilAcademy.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var smtpSection = _configuration.GetSection("SMTP");
			var host = smtpSection["Host"];
			var port = int.Parse(smtpSection["Port"]);
			var email = smtpSection["Email"];
			var password = smtpSection["Password"];

			using (var client = new SmtpClient(host, port))
			{
				client.Credentials = new NetworkCredential(email, password);
				client.EnableSsl = true; // Gmail يحتاج SSL/TLS
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;

				var message = new MailMessage(email, to, subject, body);
				await client.SendMailAsync(message);
			}

		}
	}

	public interface IEmailService 
	{
		public Task SendEmailAsync(string to , string subject, string body);
	}
}
