using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPT_HealthboardContacts
    {
        public static tbRPT_HealthBoardContacts AddRow(string healthboard, string contact)
        {
            tbRPT_HealthBoardContacts row = new tbRPT_HealthBoardContacts
            {
                Rid = 1,
                HealthBoard = healthboard,
                Contact = contact
            };

            return row;
        }
    }
}
