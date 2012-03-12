using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class SiteDetails
    {
        private string _orgId;
        private string _healthboard;
        private string _supplier;

        public string OrgId { get { return _orgId; } }
        public string Healthboard { get { return _healthboard; } }
        public string Supplier { get { return _supplier; } }

        public SiteDetails(string orgId, string healthboard, string supplier)
        {
            _orgId = orgId;
            _healthboard = healthboard;
            _supplier = supplier;
        }
    }
}
