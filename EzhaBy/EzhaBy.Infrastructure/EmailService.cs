using System.Net;
using System.Net.Mail;

namespace EzhaBy.Infrastructure
{
    public static class EmailService
    {
        public static void SendMail(string email, string subject, string body)
        {
            MailAddress from = new MailAddress("appprojectmanagement@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("appprojectmanagement@gmail.com", "zsvspsosnyhmtwln");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
