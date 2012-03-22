using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public interface ILogger
    {
        void Add(string message);
        void Write();
    }
}
