using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using OfflineSiteMonitoringTool.Model;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository : IRepository
    {
        private IReportingEntities _reportingEntity;
        
        // Load following from config file
        int numberOfRetriesAfterDatabaseError = 5;

        public Repository(IReportingEntities reportingEntity)
        {
            _reportingEntity = reportingEntity;
        }

        private List<string> CleanData(List<string> dataToClean)
        {
            List<string> cleanedData = new List<string>();

            foreach (string str in dataToClean)
                cleanedData.Add(str.Trim());

            return cleanedData;
        }

        // Called by: bool HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay)
        private bool ExecuteDbQuery(Func<DateTime, bool> query, DateTime date)
        {
            int attempts = 0;

            bool result;

            while (true)
            {
                try
                {
                    result = query(date);

                    return result;
                }
                catch (Exception ex)
                {
                    if (attempts <= numberOfRetriesAfterDatabaseError)
                    {
                        // Retry query
                        attempts++;
                        // _log.Add("Attempt: " + attempts + " : " + ex.Message);
                        continue;
                    }
                    else
                    {
                        // Log error details and throw error
                        // _log.Add("Max Attempts Reached : " + ex.Message);
                        throw (ex);
                    }
                }
            }
        }


        private List<string> ExecuteDbQuery(Func<List<string>> query)
        {
            int attempts = 0;

            List<string> result = new List<string>();

            while (true)
            {
                try
                {
                    result = query();

                    return result;
                }
                catch (Exception ex)
                {
                    if (attempts <= numberOfRetriesAfterDatabaseError)
                    {
                        // Retry query
                        attempts++;
                        // _log.Add("Attempt: " + attempts + " : " + ex.Message);
                        continue;
                    }
                    else
                    {
                        // Log error details and throw error
                        // _log.Add("Max Attempts Reached : " + ex.Message);
                        throw (ex);
                    }
                }
            }
        }

        // Added dummy implementations of each method required by interface here to get solution to build
        // will remove these methods into their own file as I work through them
        // public DateTime GetLastBusinessDay(DateTime currentDate) {  }
        // public Boolean HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay) { return false; }
        // public List<string> GetSitesToCheckMessagingActivityFor() { return new List<string>(); }
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
