using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Gmail.v1.Data;

namespace BATDemoFramework.GmailService
{
    public interface IEmailService
    {
        string GetTokenFromEmail(Message message);
        Task<List<Message>> GetMessagesByQuery(string query);
        void DeleteMessage(string messageId);
    }
}