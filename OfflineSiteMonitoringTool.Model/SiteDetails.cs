using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class SiteDetails
    {
        private string _orgId;
        private string _orgName;
        private string _healthboard;
        private string _supplier;
        private string _supplierReference;
        private DateTime? _lastMessageDate;

        public string OrgId { get { return _orgId; } }
        public string OrgName { get { return _orgName; } }
        public string Healthboard { get { return _healthboard; } }
        public string Supplier { get { return _supplier; } }
        public string SupplierReference { get { return _supplierReference; } }
        public DateTime? LastMessageDate { get { return _lastMessageDate; } }

        public SiteDetails(string orgId, string orgName, string healthboard, string supplier, string supplierReference, DateTime? lastMessageDate)
        {
            _orgId = orgId;
            _orgName = orgName;
            _healthboard = healthboard;
            _supplier = supplier;
            _supplierReference = supplierReference;
            _lastMessageDate = lastMessageDate;
        }
    }
}
