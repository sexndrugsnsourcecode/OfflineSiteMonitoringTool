using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<SiteDetails> GetOfflineSitesToReport(List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit)
        {
            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = ExecuteDbQuery(getOfflineSitesToReport, healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit);

            return offlineSitesToReport;
        }

        private List<SiteDetails> getOfflineSitesToReport(List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit)
        {
            // Get offline sites to be reported
            var sitesToBeReportedInOfflineSitesTable = (from x in _reportingEntity.tbRPT_OfflineSites
                                                        where x.DateOfflineNotificationSent == null
                                                        && !(healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit.Contains(x.Healthboard))
                                                        select x);

            List<SiteDetails> offlineSitesToReport = new List<SiteDetails>();

            foreach (var siteToBeReportedOffline in sitesToBeReportedInOfflineSitesTable)
            {
                string orgId = siteToBeReportedOffline.Org;
                string supplier = siteToBeReportedOffline.Supplier;
                string healthboard = siteToBeReportedOffline.Healthboard;
                string orgName = getOrgName(orgId);
                string supplierRef = getSupplierReference(orgId);
                DateTime? lastMsgDate = getLastMessageDate(orgId);

                SiteDetails siteToBeReportedDetails = new SiteDetails(orgId, orgName, healthboard, supplier, supplierRef, lastMsgDate);
                offlineSitesToReport.Add(siteToBeReportedDetails);
            }

            return offlineSitesToReport;
        }

        private string getOrgName(string orgId)
        {
            string orgName = (from x in _reportingEntity.tbEPS_Organisation
                              where x.id == orgId
                              select x.name).FirstOrDefault();

            if (orgName == null)
            {
                // TODO: Log warning
            }

            return orgName;
        }

        private string getSupplierReference(string orgId)
        {
            string supRef = (from x in _reportingEntity.tbEPS_Organisation
                             where x.id == orgId
                             select x.supplierReference).FirstOrDefault();

            if (supRef == null)
            {
                // TODO: log warning
            }

            return supRef;
        }

        private DateTime? getLastMessageDate(string orgId)
        {
            DateTime? lastMsgDate = null;

            lastMsgDate = (from x in _reportingEntity.tbRPT_OrgSupplier
                           where x.org == orgId
                           select x.latestAMS).FirstOrDefault();

            if (lastMsgDate == null)
            {
                // TODO: log warning
            }

            return lastMsgDate;
        }
    }
}
