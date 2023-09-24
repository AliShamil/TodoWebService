using System.Net.Mail;
using System.Net;
using TodoWebService.Models.Entities;

namespace TodoWebService.Services
{
    public class MailService : IMailService
    {
        private readonly string _email;
        private readonly string _password;
        public MailService()
        {
            _email = "elisamilzade@gmail.com";
            _password = "ihosihmulzwuyrpb";
        }

        public void SendNotifyMessage(string userMail, TodoItem todoItem)
        {
            using var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, _password)
            };

            using var message = new MailMessage()
            {

                IsBodyHtml = false,
                Subject = "Ecommerce Email confirmation",
                Body = $"Dear user {todoItem.User.UserName} you have one day to finish your work. Todoitem ID:{todoItem.Id}"
            };

            message.From = new MailAddress(_email);
            message.To.Add(new MailAddress(userMail));

            smtp.Send(message);
        }

    }

}