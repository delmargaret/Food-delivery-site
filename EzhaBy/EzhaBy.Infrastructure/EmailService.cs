using System.Net;
using System.Net.Mail;

namespace EzhaBy.Infrastructure
{
    public static class EmailService
    {
        public static void SendMail(string email, string subject, string body)
        {
            MailAddress from = new MailAddress("ezha.by.service@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("ezha.by.service@gmail.com", "EzhaBy-12345678"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            smtp.Send(m);
        }
    }
}
