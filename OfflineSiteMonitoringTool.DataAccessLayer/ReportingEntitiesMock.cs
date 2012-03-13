//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// Architectural overview and usage guide: 
// http://blogofrab.blogspot.com/2010/08/maintenance-free-mocking-for-unit.html
//------------------------------------------------------------------------------
using System.Data.EntityClient;
using System.Data.Objects;
using OfflineSiteMonitoringTool.DataAccessLayer.ReportingEntitiesMockObjectSet;

namespace OfflineSiteMonitoringTool.DataAccessLayer
{
    /// <summary>
    /// The concrete mock context object that implements the context's interface.
    /// Provide an instance of this mock context class to client logic when testing, 
    /// instead of providing a functional context object.
    /// </summary>
    public partial class ReportingEntitiesMock : IReportingEntities
    {
        public IObjectSet<tbEPS_Msg> tbEPS_Msg
        {
            get { return _tbEPS_Msg  ?? (_tbEPS_Msg = new MockObjectSet<tbEPS_Msg>()); }
        }
        private IObjectSet<tbEPS_Msg> _tbEPS_Msg;
        public IObjectSet<tbEPS_Organisation> tbEPS_Organisation
        {
            get { return _tbEPS_Organisation  ?? (_tbEPS_Organisation = new MockObjectSet<tbEPS_Organisation>()); }
        }
        private IObjectSet<tbEPS_Organisation> _tbEPS_Organisation;
        public IObjectSet<tbRPT_DailyActivityCP> tbRPT_DailyActivityCP
        {
            get { return _tbRPT_DailyActivityCP  ?? (_tbRPT_DailyActivityCP = new MockObjectSet<tbRPT_DailyActivityCP>()); }
        }
        private IObjectSet<tbRPT_DailyActivityCP> _tbRPT_DailyActivityCP;
        public IObjectSet<tbRPT_DailyActivityGP> tbRPT_DailyActivityGP
        {
            get { return _tbRPT_DailyActivityGP  ?? (_tbRPT_DailyActivityGP = new MockObjectSet<tbRPT_DailyActivityGP>()); }
        }
        private IObjectSet<tbRPT_DailyActivityGP> _tbRPT_DailyActivityGP;
        public IObjectSet<tbRPT_HealthBoardContacts> tbRPT_HealthBoardContacts
        {
            get { return _tbRPT_HealthBoardContacts  ?? (_tbRPT_HealthBoardContacts = new MockObjectSet<tbRPT_HealthBoardContacts>()); }
        }
        private IObjectSet<tbRPT_HealthBoardContacts> _tbRPT_HealthBoardContacts;
        public IObjectSet<tbRPT_OfflineSites> tbRPT_OfflineSites
        {
            get { return _tbRPT_OfflineSites  ?? (_tbRPT_OfflineSites = new MockObjectSet<tbRPT_OfflineSites>()); }
        }
        private IObjectSet<tbRPT_OfflineSites> _tbRPT_OfflineSites;
        public IObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications> tbRPT_OfflineSites_SuppliersToReceiveNotifications
        {
            get { return _tbRPT_OfflineSites_SuppliersToReceiveNotifications  ?? (_tbRPT_OfflineSites_SuppliersToReceiveNotifications = new MockObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications>()); }
        }
        private IObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications> _tbRPT_OfflineSites_SuppliersToReceiveNotifications;
        public IObjectSet<tbRPT_OrgSupplier> tbRPT_OrgSupplier
        {
            get { return _tbRPT_OrgSupplier  ?? (_tbRPT_OrgSupplier = new MockObjectSet<tbRPT_OrgSupplier>()); }
        }
        private IObjectSet<tbRPT_OrgSupplier> _tbRPT_OrgSupplier;
        public IObjectSet<tbRPT_SupplierContacts> tbRPT_SupplierContacts
        {
            get { return _tbRPT_SupplierContacts  ?? (_tbRPT_SupplierContacts = new MockObjectSet<tbRPT_SupplierContacts>()); }
        }
        private IObjectSet<tbRPT_SupplierContacts> _tbRPT_SupplierContacts;
    }
}