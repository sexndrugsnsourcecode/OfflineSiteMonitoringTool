using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public void RecordNewOfflineSite(string offlineSite)
        {
            ExecuteDbQuery(recordNewOfflineSite, offlineSite);
        }

        private void recordNewOfflineSite(string offlineSite)
        {
            // First check that the site isn't already recorded as offline
            var siteInOfflineSitesTable = (from x in _reportingEntity.tbRPT_OfflineSites
                                           where x.Org == offlineSite
                                           select x).FirstOrDefault();

            if (siteInOfflineSitesTable != null)
            {
                // TODO: log error
                return;
            }

            // Get details of offline site
            tbRPT_OfflineSites site = getOfflineSiteDetails(offlineSite);

            if (site == null)
            {
                // TODO: Log error - unable to find details of site in Org table
                return;
            }

            // _log.Add("Recording offline org: " + org);
            
            _reportingEntity.tbRPT_OfflineSites.AddObject(site);
            _reportingEntity.SaveChanges();

            //_log.Add("Offline organisation recorded: " + offlineOrg.Org);
            return;
        }

        // TODO: Create public method for this
        private tbRPT_OfflineSites getOfflineSiteDetails(string offlineSite)
        {
            var site = (from x in _reportingEntity.tbEPS_Organisation
                        where x.id == offlineSite
                        select x).FirstOrDefault();

            if (site != null)
            {
                tbRPT_OfflineSites siteDetails = new tbRPT_OfflineSites();
                siteDetails.Org = site.id;
                siteDetails.Healthboard = site.healthBoardName;
                siteDetails.Supplier = site.supplier;
                siteDetails.AuditCreatedOn = DateTime.Now;

                return siteDetails;
            }

            return null;
        }
    }
}
