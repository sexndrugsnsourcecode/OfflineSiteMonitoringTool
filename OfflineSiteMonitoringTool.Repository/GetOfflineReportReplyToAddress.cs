using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public MailAddress GetOfflineReportReplyToAddress()
        {
            MailAddress offlineNotificationReportReplyToAddress;

            try
            {
                offlineNotificationReportReplyToAddress = new MailAddress(_configHelper.GetReplyToAddress());
            }
            catch
            {
                _log.Add("ERROR: Unable to load 'replyTo' address from config file");
                throw new Exception("Unable to load 'replyTo' address from config file");
            }

            return offlineNotificationReportReplyToAddress;
        }
    }
}
