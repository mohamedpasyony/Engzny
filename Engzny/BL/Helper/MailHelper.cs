using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Engzny.BL.Helper
{
    public  class MailHelper:IMailHelper
    {
        private readonly MailSettings _mailSettings;
        public MailHelper(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public  async Task SendMail(string Reciver, string Title, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(_mailSettings.Host, _mailSettings.port);

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential(_mailSettings.Email,_mailSettings.Password);


                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("EngznyM@outlook.com");
                    mailMessage.To.Add(Reciver);
                    mailMessage.Subject = Title;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    smtp.Send(mailMessage);

                }

                }
            catch( Exception)
            {
                
            }



        }
    }
}
