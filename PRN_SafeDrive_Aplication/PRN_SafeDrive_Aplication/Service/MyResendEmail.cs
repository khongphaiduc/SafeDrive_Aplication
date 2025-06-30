using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
namespace PRN_SafeDrive_Aplication.Service
{
    public class MyResendEmail
    {

        // gửi mail 
        public static async Task SendGmailAsync(string toEmail,string titel, string content)
        {
            var fromAddress = new MailAddress("vietchobann@gmail.com", "Exam SafeDrive");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "qpqu grpb nngn hmyt"; 

             string body = "<strong>"+(content ?? "Test thong bao hihih") +"</strong>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = titel,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
               
                MessageBox.Show("✅ Email sent successfully!");
            }
        }

    }
}
