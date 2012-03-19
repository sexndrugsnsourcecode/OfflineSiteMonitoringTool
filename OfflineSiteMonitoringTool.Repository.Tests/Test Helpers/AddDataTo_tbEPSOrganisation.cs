using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbEPSOrganisation
    {
        public static tbEPS_Organisation AddRow()
        {
            tbEPS_Organisation row = new tbEPS_Organisation
            {
                rid = 1,
                id = "test",
                organisationTypeRid = 1,
                shortName = "",
                name = "",
                healthBoardName = "",
                alternateName = "",
                prsRefreshEnabled = true,
                epsServicesEnabled = true,
                startDate = DateTime.Today.AddDays(-10),
                endDate = null,
                archived = false,
                auditCreatedOn = DateTime.Today.AddDays(-10),
                auditCreatedBy = "",
                auditUpdatedOn = null,
                auditUpdatedBy = "",
                address1 = "",
                address2 = "",
                address3 = "",
                address4 = "",
                postCode = "",
                country = "",
                telephone = "",
                fax = "",
                email = "",
                supplier = "",
                supplierReference = "",
                dispensing = false,
                notes = ""
            };

            return row;
        }

        public static tbEPS_Organisation AddRow(string id, DateTime? endDate = null, bool archived = false, bool dispensing = false)
        {
            tbEPS_Organisation row = AddRow();
            row.id = id;
            row.endDate = endDate;
            row.archived = archived;
            row.dispensing = dispensing;

            return row;
        }

        public static tbEPS_Organisation AddRow(string id, string healthboard, string supplier, string orgName = "test", string supplierRef = "test")
        {
            tbEPS_Organisation row = AddRow();
            row.id = id;
            row.healthBoardName = healthboard;
            row.supplier = supplier;
            row.name = orgName;
            row.supplierReference = supplierRef;

            return row;
        }
    }
}
