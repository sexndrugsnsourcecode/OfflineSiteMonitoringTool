using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class UpdateOfflineSiteData
    {
        private IRepository _repository;

        public UpdateOfflineSiteData(IRepository repository)
        {
            _repository = repository;
        }

        public List<string> GetSitesRecordedAsOffline()
        {
            List<string> sitesRecordedAsOffline = new List<string>();

            sitesRecordedAsOffline = _repository.GetSitesRecordedAsOffline();

            return sitesRecordedAsOffline;
        }

        // remove sites that have come online from offline site table
        public void RemoveOnlineSites(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // for all sites present in sitesRecordedAsOffline but not in offlineSites, remove those sites from table
            IEnumerable<string> onlineSites = sitesRecordedAsOffline.Except(offlineSites);
            
            foreach (string onlineSite in onlineSites)
                _repository.RemoveOnlineSite(onlineSite);
        }

        // update any offline sites that are already recorded
        public void UpdateSitesAlreadyRecordedAsOffline(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // for all sites in sitesRecordedAsOffline and offlineSites, update the site's row in table

            foreach (string offlineSite in sitesRecordedAsOffline)
            {
                if (offlineSites.Contains(offlineSite))
                    _repository.UpdateSiteAlreadyRecordedAsOffline(offlineSite);
            }
        }

        // record any offline sites not already recorded
        public void RecordNewOfflineSites(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // for all sites in sitesRecordedAsOffline but not in offlineSites, add details of site to table
            IEnumerable<string> newOfflineSites = offlineSites.Except(sitesRecordedAsOffline);

            foreach (string newOfflineSite in newOfflineSites)
                _repository.RecordNewOfflineSite(newOfflineSite);
        }
        
    }
}
