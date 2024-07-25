using System.Net;
using System.Net.Mail;

namespace PersentationLayer.Utilities
{
	public static  class EmailSetting
	{
		public static void SendEmail (Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("youssefsaeedhamed9@gmail.com", "keefwvoyllyqsqxn");
			Client.Send("youssefsaeedhamed9@gmail.com", email.Recepient, email.Subject, email.Body);
		}
	}
}
