using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Gmail.v1.Data;

namespace BATDemoFramework.GmailService
{
    public interface IEmailService
    {
        /// <summary>
        /// Get token from Email
        /// </summary>
        /// <param name="message">Email message used to extract token from</param>
        string GetTokenFromEmail(Message message);
        /// <summary>
        /// List all Messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">String used to filter Messages returned.</param>
        /// <param name="recipient">String used to filter out by recipient Messages returned </param>
        Task<List<Message>> GetMessagesByQuery(string query, string recipient = null);
        /// <summary>
        /// Get all urls found in the email message
        /// </summary>
        /// <param name="message">String used to filter Messages returned.</param>
        List<string> GetUrlsFromMessage(Message message);
        /// <summary>
        /// Delete a Message.
        /// </summary>
        /// <param name="messageId">ID of the Message to delete.</param>
        void DeleteMessage(string messageId);
    }
}