using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQModel
    {
        MessageQueue message = new MessageQueue();
        public void MsmqSender(string Token)
        {
            message.Path = @".\private$\Token";

            if (!MessageQueue.Exists(message.Path))
            {
                MessageQueue.Create(message.Path);
            }
            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });  //it Creates a asynchronous communication

            message.ReceiveCompleted += Message_ReceiveCompleted;    //Press tab

            message.Send(Token);
            message.BeginReceive();
            message.Close();
        }

        private void Message_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = message.EndReceive(e.AsyncResult);

            string token = msg.Body.ToString();

            string subject = " Fundoo Notes Password Reset";

            string Body = "token";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("kavita@gmail.com", "Kavita@1234"), //here valid email and password
                EnableSsl = true,
            };

            smtpClient.Send("kavita@gmail", "jwt", subject, Body);

            message.BeginReceive();
        }
        public string DecodeJwt(string token)
        {
            try
            {
                var decodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((decodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
