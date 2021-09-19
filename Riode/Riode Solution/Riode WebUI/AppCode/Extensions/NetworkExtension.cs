using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public bool SendEmail(this IConfiguration configuration, string to, string subject, string body, bool appendCC=false)
        {
            string fromMail = configuration["emailAccount:userName"];
            string displayName = configuration["emailAccount:displayName"];
            string smtpServer = configuration["emailAccount:smtpServer"];
            string password = configuration["emailAccount:password"];
            string cc = configuration["emailAccount:cc"];
            int smtpPort = Convert.ToInt32(configuration["emailAccount:smtpPort"]);


            try
            {
                using (MailMessage message = new MailMessage(new MailAddress(fromMail, displayName), new MailAddress(to))
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    if (!string.IsNullOrEmpty(cc) && appendCC == true)
                        message.CC.Add(cc);

                    SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                    smtpClient.Credentials = new NetworkCredential(fromMail, password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
