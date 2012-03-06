using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class CheckSitesActivity
    {
        private IRepository _repository;

        private DateTime _lastBusinessDay;

        public CheckSitesActivity(IRepository repository, DateTime lastBusinessDay)
        {
            _repository = repository;

            _lastBusinessDay = lastBusinessDay;

            setSitesToCheckMessagingActivityFor();

            setOfflineSites(sitesToCheckMessagingActivityFor);
        }

        private List<string> sitesToCheckMessagingActivityFor;

        private void setSitesToCheckMessagingActivityFor()
        {
            sitesToCheckMessagingActivityFor = _repository.GetSitesToCheckMessagingActivityFor();
        }

        private List<string> offlineSites;

        private void setOfflineSites(List<string> sites)
        {
            offlineSites = _repository.GetOfflineSites(sitesToCheckMessagingActivityFor, _lastBusinessDay);
        }

        public List<string> GetOfflineSites { get { return offlineSites; } }
    }
}
