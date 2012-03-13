using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.DataAccessLayer
{
    public partial class ReportingEntitiesMock
    {
        // Added this partial class so I didn't have to add these methods
        // again every time I updated the Reporting entity model

        public int SaveChanges() { return 0; }
        public void DeleteObject(object entity)
        {
            tbRPT_OfflineSites orgs = (tbRPT_OfflineSites)entity;
            this._tbRPT_OfflineSites.DeleteObject(orgs);
        }  
    }
}
