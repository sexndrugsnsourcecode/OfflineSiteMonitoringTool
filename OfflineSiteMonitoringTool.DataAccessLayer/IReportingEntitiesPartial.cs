using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.DataAccessLayer
{
    public partial interface IReportingEntities
    {
        // Created this partial interface as I had to rewrite these methods every time 
        // I updated the Reporting entity model

        int SaveChanges();
        void DeleteObject(object entity);
    }
}
