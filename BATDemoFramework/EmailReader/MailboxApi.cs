using Mailosaur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.EmailReader
{
    public class MailboxReader
    {
        private object email;

        public MailboxReader()
        {
            var mailbox = new MailboxApi("kiucydsg", "zrFWxcy7APBDu2A");
            var emails = mailbox.GetEmailsByRecipient("donotreply@neyber.co.uk");

            var email = emails[0];
        }
       
    }
}
