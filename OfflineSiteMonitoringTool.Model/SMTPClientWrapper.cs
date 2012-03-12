using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace OfflineSiteMonitoringTool.Model
{
    public class SMTPClientWrapper : ISMTPClientWrapper
    {
        SmtpClient _smtpClient;

        // Constructor that will be used when app is ran (not unit tested)
        public SMTPClientWrapper()
        {
            _smtpClient = new SmtpClient();
        }

        // Constructor for unit testing
        public SMTPClientWrapper(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public void Send(MailMessage email)
        {
            _smtpClient.Send(email);
        }
    }
}
