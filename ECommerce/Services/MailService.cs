using System.Net;
using System.Net.Mail;
namespace ECommerce.Services
{
    public class MailService
    {

        // Looking to send emails in production? Check out our Email API/SMTP product!

        public void SendMessage(string SendtoEmail ,string Subject, string Body )
        {

            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("6ecff26c8406b7", "83304a61e8f702"),
                EnableSsl = true
            };


            client.Send("from@example.com", SendtoEmail, Subject, Body);
            
        }
    }
    

}
