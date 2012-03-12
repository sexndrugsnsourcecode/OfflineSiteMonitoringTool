using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using Microsoft.Pex.Framework;

namespace OfflineSiteMonitoringTool.Model
{
    public interface IRepository
    {
        DateTime GetLastBusinessDay(DateTime currentDate);
        Boolean HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay);
        List<string> GetSitesToCheckMessagingActivityFor();
        List<string> GetOfflineSites(List<string> sitesToCheckMessagingActivityFor, DateTime lastBusinessDay);
        List<string> GetSitesRecordedAsOffline();
        void RemoveOnlineSite(string onlineSite);
        void UpdateSiteAlreadyRecordedAsOffline(string offlineSite);
        void RecordNewOfflineSite(string offlineSite);
        List<string> GetSuppliersToReceiveOfflineNotifications();
        int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();
        List<string> GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(int numberOfOfflineSitesToBeReportedPerHealthboardLimit);
        List<SiteDetails> GetOfflineSitesToReport(List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit);
        MailAddress GetOfflineReportFromAddress();
        MailAddress GetOfflineReportReplyToAddress();
        List<MailAddress> GetOfflineReportRecipients(string healthboard, string supplier);
        void RecordOfflineNotificationHasBeenSentForSite(string offlineSite);
    }
}
