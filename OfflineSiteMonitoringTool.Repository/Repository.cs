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
        
        // todo: Load following from config file
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

            cleanedData.Sort();

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

        // Called by: List<string> GetSitesToCheckMessagingActivityFor()
        // Called by: List<string> GetSitesRecordedAsOffline()
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

        // Called by: List<string> GetOfflineSites(List<string> sitesToCheckMessagingActivityFor, DateTime lastBusinessDay)
        private List<string> ExecuteDbQuery(Func<List<string>, DateTime, List<string>> query, List<string> list, DateTime date)
        {
            int attempts = 0;

            List<string> result = new List<string>();

            while (true)
            {
                try
                {
                    result = query(list, date);

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

        // Called by: void RemoveOnlineSite(string onlineSite)
        private void ExecuteDbQuery(Action<string> query, string str)
        {
            int attempts = 0;

            while (true)
            {
                try
                {
                    query(str);

                    return;
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
        // public void UpdateSiteAlreadyRecordedAsOffline(string offlineSite) { }
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
