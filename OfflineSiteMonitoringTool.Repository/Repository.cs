using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSiteMonitoringTool.Repository
{
    public class Repository : IRepository
    {
        // Added dummy implementations of each method required by interface here to get solution to build
        // will remove these methods into their own file as I work through them
        public DateTime GetLastBusinessDay(DateTime currentDate) { return new DateTime(); }
        public Boolean HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay) { return false; }
        public List<string> GetSitesToCheckMessagingActivityFor() { return new List<string>(); }
        public List<string> GetOfflineSites(List<string> sitesToCheckMessagingActivityFor, DateTime lastBusinessDay) { return new List<string>(); }
        public List<string> GetSitesRecordedAsOffline() { return new List<string>(); }
        public void RemoveOnlineSite(string onlineSite) { }
        public void UpdateSiteAlreadyRecordedAsOffline(string offlineSite) { }
        public void RecordNewOfflineSite(string offlineSite) { }
        public List<string> GetSuppliersToReceiveOfflineNotifications() { return new List<string>(); }
        public int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit() { return 0; }
        public List<string> GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(int numberOfOfflineSitesToBeReportedPerHealthboardLimit) { return new List<string>(); }
        public List<SiteDetails> GetOfflineSitesToReport(List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit) { return new List<SiteDetails>(); }
        public MailAddress GetOfflineReportFromAddress() { return new MailAddress("test.test.com"); }
        public MailAddress GetOfflineReportReplyToAddress() { return new MailAddress("test.test.com"); }
        public List<MailAddress> GetOfflineReportRecipients(string healthboard, string supplier) { return new List<MailAddress>(); }
        public void RecordOfflineNotificationHasBeenSentForSite(string offlineSite) { }
    }
}
