using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace SysWal
{
    class SendMail
    {
        private string mailSender = "katogaroman@gmail.com";
     //   private string mailReceiver = "azslomian@gmail.com";
        private string senderPassword = "katogaroman1234567890";
     //   private string name = "Adam";


        public string MailSend(string mailReceiver, string name)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(mailSender);
                message.To.Add(mailReceiver);
                message.Body = ("Witaj " + name + "!\n Twoja rejestracja przebiegła pomyślnie!\n #CurrencySystem Exchange");
                message.Subject = "Rejestracja do ExchangeSystem";
                client.UseDefaultCredentials = false;

                client.Credentials = new System.Net.NetworkCredential(mailSender, senderPassword);
                client.EnableSsl = true;
                client.Send(message);
                message = null;
                // return true;
                return "Email został wysłany";
            }
            catch (Exception ex)
            {
                return ex.Message;
                // return false;
            }


            /*        public string MailSend()
                    {
                        try
                        {
                            MailMessage message = new System.Net.Mail.MailMessage();
                            message.To.Add(mailReceiver);
                            message.Subject = "This is the Subject line";
                            message.From = new System.Net.Mail.MailAddress(mailSender, "Testmail");
                            message.Body = "This is the message body";

                            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 465);
                            smtp.UseDefaultCredentials = false;
                            smtp.EnableSsl = true;
                            smtp.Credentials = new System.Net.NetworkCredential(mailSender,senderPassword);
                            smtp.Send(message);
                            return "Cool";
                        }
                        catch(Exception ex)
                        {
                            return ex.Message;
                        }
                    }   
                    
                    */

            /*      MailMessage message = new MailMessage();
                  message.To.Add(mailReceiver);
                  message.Subject = "This is the Subject line";
                  message.Priority = MailPriority.High;
                  message.From = new System.Net.Mail.MailAddress(mailSender, "TestMail");
                  message.Body = "This is the message body";
                  SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                  smtp.EnableSsl = true;
                  smtp.Credentials = new System.Net.NetworkCredential(mailSender, senderPassword);

                  using (var smtpClient = new SmtpClient())
                  {
                      await smtpClient.SendMailAsync(message);
                  }
              }*/
        }
    }
}
