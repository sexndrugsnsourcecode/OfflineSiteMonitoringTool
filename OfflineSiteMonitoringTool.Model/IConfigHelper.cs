using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public interface IConfigHelper
    {
        int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();
        int GetNumberOfRetriesAfterDatabaseError();
        string GetFromAddress();
        string GetReplyToAddress();
    }
}
