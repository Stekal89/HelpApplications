using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n\t\t\t###### S E N D - M A I L ######\n");

            // Sender
            MailAddress senderAdd = new MailAddress("sender@domain.local", "SenderName");
            const string senderPwd = "SenderPassword";

            // Receiver
            Console.Write("Please enter the email-address of the receiver: ");
            string receiverAddress = Console.ReadLine();
            Console.Write("Please enter the Name of the receiver: ");
            string receiverName = Console.ReadLine();
            MailAddress toAddress = new MailAddress(receiverAddress, receiverName);

            Console.Write("\nPlease enter the subject of the mail: ");
            string subject = Console.ReadLine();
            Console.Write("Please enter the body of the mail: ");
            string body = Console.ReadLine();

            // Attachement
            Attachment attachment = new Attachment(@"%FullFIlePath%");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderAdd.Address, senderPwd)
            };
            using (var message = new MailMessage(senderAdd, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                // add attachement and send mail
                message.Attachments.Add(attachment);
                smtp.Send(message);
            }
        }
    }
}
