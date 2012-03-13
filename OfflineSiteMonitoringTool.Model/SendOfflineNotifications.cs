using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace OfflineSiteMonitoringTool.Model
{
    public class SendOfflineNotifications
    {
        IRepository _repository;
        ISMTPClientWrapper _smtpClient;

        public SendOfflineNotifications(IRepository repository, ISMTPClientWrapper smtpClient)
        {
            _repository = repository;
            _smtpClient = smtpClient;
        }

        public Dictionary<string, MailMessage> CreateOfflineReports(List<SiteDetails> sitesToBeReportedOffline)
        {
            Dictionary<string, MailMessage> offlineReports = new Dictionary<string, MailMessage>();

            foreach (SiteDetails site in sitesToBeReportedOffline)
            {
                MailMessage offlineReport = CreateOfflineReport(site);
                offlineReports.Add(site.OrgId, offlineReport);
            }

            return offlineReports;
        }

        private MailMessage CreateOfflineReport(SiteDetails siteToBeReportedOffline)
        {
            MailMessage offlineReport = new MailMessage();

            // Get email address information
            MailAddress fromAddress = _repository.GetOfflineReportFromAddress();
            MailAddress replyToAddress = _repository.GetOfflineReportReplyToAddress();
            List<MailAddress> toAddresses = _repository.GetOfflineReportRecipients(siteToBeReportedOffline.Healthboard, siteToBeReportedOffline.Supplier);

            // Set email address information
            offlineReport.From = fromAddress;
            offlineReport.ReplyTo = replyToAddress;
            foreach (MailAddress toAddress in toAddresses)
                offlineReport.To.Add(toAddress);

            offlineReport.Subject = setEmailSubject(siteToBeReportedOffline);
            offlineReport.Body = setEmailBody(siteToBeReportedOffline);

            return offlineReport;
        }

        private string setEmailSubject(SiteDetails siteToBeReportedOffline)
        {
            string emailSubject = null;

            if (siteToBeReportedOffline.Supplier == "EMIS")
                emailSubject = setEmailSubjectForEmisSite(siteToBeReportedOffline);
            else if (siteToBeReportedOffline.Supplier == "INPS")
                emailSubject = setEmailSubjectForInpsSite(siteToBeReportedOffline);

            return emailSubject;
        }

        private string setEmailSubjectForEmisSite(SiteDetails siteToBeReportedOffline)
        {
            string emailSubject;
            
            emailSubject = "Transmission Fault in ePharmacy CDB " + siteToBeReportedOffline.SupplierReference + 
                " (OrgID " + siteToBeReportedOffline.OrgId + ")";

            return emailSubject;
        }

        private string setEmailSubjectForInpsSite(SiteDetails siteToBeReportedOffline)
        {
            string emailSubject;

            emailSubject = "eAMS Offline - INPS Reference " + siteToBeReportedOffline.SupplierReference +
                " (OrgID " + siteToBeReportedOffline.OrgId + ")";

            return emailSubject;
        }

        private string setEmailBody(SiteDetails siteToBeReportedOffline)
        {
            string emailBody = null;

            if (siteToBeReportedOffline.Supplier == "EMIS")
                emailBody = setEmailBodyForEmisSite(siteToBeReportedOffline);
            else if (siteToBeReportedOffline.Supplier == "INPS")
                emailBody = setEmailBodyForInpsSite(siteToBeReportedOffline);

            return emailBody;
        }

        private string setEmailBodyForEmisSite(SiteDetails siteToBeReportedOffline)
        {
            string emailBody = null;

            emailBody =
                "Transmission Fault in ePharmacy\n\n"
                + "This site ("
                + siteToBeReportedOffline.OrgName + ", CDB "
                + siteToBeReportedOffline.SupplierReference + ", OrgID "
                + siteToBeReportedOffline.OrgId
                + ") is reported as being offline. The last AMS message was received on "
                + siteToBeReportedOffline.LastMessageDate
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            return emailBody;
        }

        private string setEmailBodyForInpsSite(SiteDetails siteToBeReportedOffline)
        {
            string emailBody = null;

            emailBody =
                "eAMS Offline\n\n"
                + "This site ("
                + siteToBeReportedOffline.OrgName + ", INPS Reference "
                + siteToBeReportedOffline.SupplierReference + ", OrgID "
                + siteToBeReportedOffline.OrgId
                + ") is reported as being offline. The last AMS message was received on "
                + siteToBeReportedOffline.LastMessageDate
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            return emailBody;
        }

        public void SendOfflineReports(Dictionary<string, MailMessage> offlineReports)
        {
            foreach (KeyValuePair<string, MailMessage> offlineReport in offlineReports)
            {
                _smtpClient.Send(offlineReport.Value);
                _repository.RecordOfflineNotificationHasBeenSentForSite(offlineReport.Key);
            }
        }
    }
}
