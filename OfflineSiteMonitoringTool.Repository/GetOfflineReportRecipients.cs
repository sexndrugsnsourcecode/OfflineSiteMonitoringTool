using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<MailAddress> GetOfflineReportRecipients(string healthboard, string supplier)
        {
            List<MailAddress> recipients = new List<MailAddress>();

            recipients.AddRange(ExecuteDbQuery(getOfflineReportHealthboardRecipients, healthboard));

            recipients.AddRange(ExecuteDbQuery(getOfflineReportSupplierRecipients, supplier));

            return recipients;
        }

        private List<MailAddress> getOfflineReportHealthboardRecipients(string healthboard)
        {
            var recipients = from x in _reportingEntity.tbRPT_HealthBoardContacts
                             where x.HealthBoard == healthboard
                             select x.Contact;

            if (recipients.Count() == 0)
            {
                //_log.Add("WARNING: No contacts could be found in tbRPT_HealthBoardContacts for: " + healthboard);
            }

            List<MailAddress> healthboardRecipients = new List<MailAddress>();
            foreach (string recipient in recipients)
            {
                healthboardRecipients.Add(new MailAddress(recipient));
            }

            return healthboardRecipients;
        }

        private List<MailAddress> getOfflineReportSupplierRecipients(string supplier)
        {
            var recipients = from x in _reportingEntity.tbRPT_SupplierContacts
                             where x.Supplier == supplier
                             select x.Contact;

            if (recipients.Count() == 0)
            {
                //_log.Add("WARNING: No contacts could be found in tbRPT_HealthBoardContacts for: " + healthboard);
            }

            List<MailAddress> supplierRecipients = new List<MailAddress>();
            foreach (string recipient in recipients)
            {
                supplierRecipients.Add(new MailAddress(recipient));
            }

            return supplierRecipients;
        }
    }
}
