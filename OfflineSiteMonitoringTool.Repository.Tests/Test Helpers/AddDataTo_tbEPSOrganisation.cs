using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbEPSOrganisation
    {
        public static tbEPS_Organisation AddRow(string id, DateTime? endDate = null, bool archived = false, bool dispensing = false)
        {
            tbEPS_Organisation row = new tbEPS_Organisation
            {
                rid = 1,
                id = id,
                organisationTypeRid = 1,
                shortName = "",
                name = "",
                healthBoardName = "",
                alternateName = "",
                prsRefreshEnabled = true,
                epsServicesEnabled = true,
                startDate = DateTime.Today.AddDays(-10),
                endDate = endDate,
                archived = archived,
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
                dispensing = dispensing,
                notes = ""
            };

            return row;
        }
    }
}
