using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class UpdateOfflineSiteData
    {
        private IRepository _repository;

        private List<string> _offlineSites;
        private List<string> _sitesRecordedAsOffline;
        
        public UpdateOfflineSiteData(IRepository repository, IList<string> offlineSites)
        {
            _repository = repository;

            _sitesRecordedAsOffline = getSitesRecordedAsOffline();


        }

        

        private List<string> getSitesRecordedAsOffline()
        {
            List<string> sitesRecordedAsOffline = new List<string>();

            //sitesRecordedAsOffline = _repository.GetSitesRecordedAsOffline();

            return sitesRecordedAsOffline;
        }

        public void RemoveOnlineSites(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // remove sites that have come online from offline site table

            // for all sites present in sitesRecordedAsOffline but not in offlineSites, remove those sites from table
        }

        public void UpdateSitesAlreadyRecordedAsOffline(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // update any offline sites that are already recorded

            // for all sites in sitesRecordedAsOffline and offlineSites, update the site's row in table
        }
        
        public void RecordNewOfflineSites(List<string> sitesRecordedAsOffline, List<string> offlineSites)
        {
            // record any offline sites not already recorded

            // for all sites in sitesRecordedAsOffline but not in offlineSites, add details of site to table
        }
        
    }
}
