using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace TruckEasyWebSite.Models
{
    public class Notification
    {
        public void SendEmail(string from, string to, string subject, string body)
        {

            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["AdminUser"], WebConfigurationManager.AppSettings["AdminPassword"]);
                using (MailMessage message = new MailMessage())
                {
                    smtpClient.Host = WebConfigurationManager.AppSettings["SMTPName"];
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;

                    MailAddress fromAddress = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
                    MailAddress replyToAddress = new MailAddress(from);

                    message.From = fromAddress;
                    message.Subject = subject;
                    message.IsBodyHtml = true; // Set IsBodyHtml to true means you can send HTML email.
                    message.ReplyToList.Add(replyToAddress);
                    message.Body = body;
                    message.To.Add(to);
                    message.Bcc.Add("getquotemoving@gmail.com");

                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            
        }

    }
}