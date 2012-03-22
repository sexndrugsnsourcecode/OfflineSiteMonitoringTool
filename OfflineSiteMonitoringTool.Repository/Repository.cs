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
        private IConfigHelper _configHelper;
        private ILogger _log;
        
        // todo: Load following from config file
        int numberOfRetriesAfterDatabaseError = 5;

        public Repository(IReportingEntities reportingEntity, IConfigHelper configHelper, ILogger log)
        {
            _reportingEntity = reportingEntity;
            _configHelper = configHelper;
            _log = log;
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

        // Called by: List<string> GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(int numberOfOfflineSitesToBeReportedPerHealthboardLimit)
        private List<string> ExecuteDbQuery(Func<int, List<string>> query, int num)
        {
            int attempts = 0;

            List<string> result = new List<string>();

            while (true)
            {
                try
                {
                    result = query(num);

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
        // Called by: List<string> GetSuppliersToReceiveOfflineNotifications()
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

        // Called by: List<SiteDetails> GetOfflineSitesToReport(List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit)
        private List<SiteDetails> ExecuteDbQuery(Func<List<string>, List<SiteDetails>> query, List<string> list)
        {
            int attempts = 0;

            List<SiteDetails> result = new List<SiteDetails>();

            while (true)
            {
                try
                {
                    result = query(list);

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

        // Called by: List<MailAddress> GetOfflineReportRecipients(string healthboard, string supplier)
        private List<MailAddress> ExecuteDbQuery(Func<string, List<MailAddress>> query, string str)
        {
            int attempts = 0;

            List<MailAddress> result = new List<MailAddress>();

            while (true)
            {
                try
                {
                    result = query(str);

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
        // Called by: void UpdateSiteAlreadyRecordedAsOffline(string offlineSite)
        // Called by: void RecordNewOfflineSite(string offlineSite)
        private void ExecuteDbAction(Action<string> query, string str)
        {
            // TODO:I renamed this as the complier was reporting it was ambigous with List<MailAddress> ExecuteDbQuery(Func<string, List<MailAddress>> query, string str)
            // fuck knows how! Need to investigate....

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
        // public void RecordOfflineNotificationHasBeenSentForSite(string offlineSite) { }
    }
}
