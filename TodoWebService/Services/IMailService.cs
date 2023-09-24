using TodoWebService.Models.Entities;

namespace TodoWebService.Services
{
    public interface IMailService
    {
        void SendNotifyMessage(string userMail, TodoItem todoItem);
    }
}
