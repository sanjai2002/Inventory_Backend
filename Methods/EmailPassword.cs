using System.Net.Mail;
using System.Net;

namespace InventoryManagementSystem.Methods
{
    public class SendEmailPassword
    {
        public static void Sendpassword(string password, string Email)
        {
            string sender = "sanjairock85@gmail.com";
            string senderPass = "vmrc sKxx ihyK jscu";
            // string recieve = "smano4570@gmail.com";

            MailMessage mail = new MailMessage(sender, Email);
            mail.Subject = "password";
            mail.Body = $" your password {password}";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(sender, senderPass);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);

        }

    }
}
