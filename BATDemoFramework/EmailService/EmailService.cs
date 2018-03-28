
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BATDemoFramework.GmailService;
using BATDemoFramework.Helpers;
using Google.Apis.Util;

namespace BATDemoFramework.EmailService
{
    public class EmailService : IEmailService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes =
        {
            Google.Apis.Gmail.v1.GmailService.Scope.MailGoogleCom,
            Google.Apis.Gmail.v1.GmailService.Scope.GmailModify,
        };
        static string ApplicationName = "BATDemoFramework";
        private Google.Apis.Gmail.v1.GmailService service;

        public EmailService()
        {
            UserCredential credential;

            var clientSecretPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client_secret.json");

            using (var stream = new FileStream(clientSecretPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }


            // Create Gmail API service.
            service = new Google.Apis.Gmail.v1.GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        
        public string GetTokenFromEmail(Message message)
        {
            if (message.Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value != EmailTypes.ConfirmYourEmail)
            {
               throw new Exception($"Message doesn't have required subject: {EmailTypes.ConfirmYourEmail}");
            }

            var html = GetDecodedHtmlFromEmail(message);

            return GetTokenFromHtml(html);
        }

        
        public async Task<List<Message>> GetMessagesByQuery(string query, string recipient = null)
        {
            var ids = GetMessageIdsByQuery(query);

            return await GetMessagesById(ids, recipient);
        }

        
        public void DeleteMessage(string messageId)
        {
            service.Users.Messages.Delete("me", messageId).Execute();
        }

        
        public List<string> GetUrlsFromMessage(Message message)
        {
            var html = GetDecodedHtmlFromEmail(message);

            var urlMatches = StringHelper.GetURLsWithMatchingPattern(html, string.Empty);

            if (urlMatches == null || !urlMatches.Any())
            {
                throw new Exception("No confirm your email url found");
            }

            return urlMatches.ToList();
        }

        public string GetUrlTokenFromMessage(Message message)
        {
            var html = GetDecodedHtmlFromEmail(message);

            var urlTokenMatchingPattern = "token=";

            var urlMatches = StringHelper.GetURLsWithMatchingPattern(html, urlTokenMatchingPattern);

            if (urlMatches == null || !urlMatches.Any())
            {
                throw new Exception("No confirm your email url found");
            }
            else
            {

                return urlMatches.ToList()[0];
            }
        }


        private List<string> GetMessageIdsByQuery(string query)
        {
            var result = new List<Message>();
            var request = service.Users.Messages.List("me");
            request.Q = query;

            do
            {
                try
                {
                    var response = request.Execute();
                    result.AddRange(response.Messages);
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            } while (!string.IsNullOrEmpty(request.PageToken));

            return result.Select(message => message.Id).ToList();
        }


        private async Task<List<Message>> GetMessagesById(IEnumerable<string> messageIds, string recipient = null)
        {
            var messages = new List<Message>();
            try
            {
                foreach (var id in messageIds)
                {
                    var message = await service.Users.Messages.Get("me", id).ExecuteAsync();
                    messages.Add(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return FilterByRecipient(messages, recipient);
        }

        private List<Message> FilterByRecipient(List<Message> messages, string recipient)
        {
            if (!string.IsNullOrWhiteSpace(recipient))
            {
                messages = messages.Where(message =>
                {
                    return message.Payload.Headers.FirstOrDefault(x => x.Name == "To").Value == recipient;
                }).ToList();
            }

            return messages;
        }

        private string GetTokenFromHtml(string html)
        {
            var urlTokenMatchingPattern = "token=";

            var urlMatches = StringHelper.GetURLsWithMatchingPattern(html, urlTokenMatchingPattern);

            if (urlMatches == null || !urlMatches.Any())
            {
                throw new Exception("No confirm your email url found");
            }
            else
            {
                var confirmEmailUrl = urlMatches.ToList()[0];

                var tokenIndex = confirmEmailUrl.IndexOf(urlTokenMatchingPattern, StringComparison.Ordinal);

                var token = confirmEmailUrl.Substring(tokenIndex + urlTokenMatchingPattern.Length);

                return token;
            }
        }

      
         
        private string GetDecodedHtmlFromEmail(Message message)
        {
            var data = message.Payload.Parts[0].Body.Data;

            if (data == null)
            {
                throw new Exception("Message doesn't contain body");
            }

            return DecodeBase64(data);
        }

        private string DecodeBase64(string data)
        {
            data = data.Replace("-", "+");
            data = data.Replace("_", "/");

            var base64 = Convert.FromBase64String(data);

            var str = Encoding.UTF8.GetString(base64);

           return str;
        }
    }
}