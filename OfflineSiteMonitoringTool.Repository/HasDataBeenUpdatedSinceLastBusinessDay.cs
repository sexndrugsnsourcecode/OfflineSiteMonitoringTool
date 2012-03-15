using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        //public bool HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay)
        //{
        //    DateTime lastestMsg = new DateTime();

        //    lastestMsg = (from x in _reportingEntity.tbEPS_Msg
        //                  orderby x.auditCreatedOn descending
        //                  select x.auditCreatedOn).FirstOrDefault();

        //    bool hasTableBeenUpdated;
        //    if (lastestMsg > lastBusinessDay)
        //        hasTableBeenUpdated = true;
        //    else
        //        hasTableBeenUpdated = false;

        //    return hasTableBeenUpdated;
        //}

        public bool HasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay)
        {
            bool hasTableBeenUpdated;

            hasTableBeenUpdated = ExecuteDbQuery(hasDataBeenUpdatedSinceLastBusinessDay, lastBusinessDay);

            return hasTableBeenUpdated;
        }

        private bool hasDataBeenUpdatedSinceLastBusinessDay(DateTime lastBusinessDay)
        {
            DateTime lastestMsg = new DateTime();

            lastestMsg = (from x in _reportingEntity.tbEPS_Msg
                          orderby x.auditCreatedOn descending
                          select x.auditCreatedOn).FirstOrDefault();

            bool hasTableBeenUpdated;
            if (lastestMsg > lastBusinessDay)
                hasTableBeenUpdated = true;
            else
                hasTableBeenUpdated = false;

            return hasTableBeenUpdated;
        }
    }
}
