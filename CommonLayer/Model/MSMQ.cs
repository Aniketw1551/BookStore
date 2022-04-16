using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();
        ////Method to Send token on Mail
        public void Sender(string token)
        {
            ////system private msmq server path 
            messageQueue.Path = @".\private$\Tokens";
            try
            {
                ////Checking Path is exists or Not
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    ////If path is not there then Creating Path
                    MessageQueue.Create(messageQueue.Path);
                }

                this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                ////Delegate Method
                this.messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                this.messageQueue.Send(token);
                this.messageQueue.BeginReceive();
                this.messageQueue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////Delegate Method for Sending E-Mail
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("aniketw23456@gmail.com", "#Aniket23456")
                };
                mailMessage.From = new MailAddress("aniketw23456@gmail.com");
                mailMessage.To.Add(new MailAddress("aniketw23456@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "Forgot Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                this.messageQueue.BeginReceive();
            }
        }
    }
}