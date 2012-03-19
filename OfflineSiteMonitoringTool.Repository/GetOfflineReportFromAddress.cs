using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public MailAddress GetOfflineReportFromAddress()
        {
            MailAddress offlineNotificationReportFromAddress;

            try
            {
                offlineNotificationReportFromAddress = new MailAddress(_configHelper.GetFromAddress());
            }
            catch
            {
                // TODO: log error
                throw new Exception("Unable to load 'from' address from config file");
            }

            return offlineNotificationReportFromAddress;
        }
    }
}
