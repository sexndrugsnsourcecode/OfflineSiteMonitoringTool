using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        // Should this be in the repository???
        public DateTime GetLastBusinessDay(DateTime currentDate)
        {
            DateTime dateOfLastBusinessDay;
            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
            if (currentDayOfWeek == DayOfWeek.Sunday)
            {
                // Check for activity from Friday
                dateOfLastBusinessDay = DateTime.Now.AddDays(-2);
            }
            else if (currentDayOfWeek == DayOfWeek.Monday)
            {
                // Check for activity from Friday
                dateOfLastBusinessDay = DateTime.Now.AddDays(-3);
            }
            else
            {
                // Check for activity from previous day
                dateOfLastBusinessDay = DateTime.Now.AddDays(-1);
            }

            return dateOfLastBusinessDay;
        }
    }
}
