using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPT_SupplierContacts
    {
        public static tbRPT_SupplierContacts AddRow(string supplier, string contact)
        {
            tbRPT_SupplierContacts row = new tbRPT_SupplierContacts
            {
                Rid = 1,
                Supplier = supplier,
                Contact = contact
            };

            return row;
        }
    }
}
