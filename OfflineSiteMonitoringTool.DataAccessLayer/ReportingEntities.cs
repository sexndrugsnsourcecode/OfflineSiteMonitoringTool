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

namespace OfflineSiteMonitoringTool.DataAccessLayer
{
    /// <summary>
    /// The functional concrete object context. This is just like the normal
    /// context that would be generated using the POCO artefact generator, 
    /// apart from the fact that this one implements an interface containing 
    /// the entity set properties and exposes <code>IObjectSet</code>
    /// instances for entity set properties.
    /// </summary>
    public partial class ReportingEntities : ObjectContext, IReportingEntities 
    {
        public const string ConnectionString = "name=ReportingEntities";
        public const string ContainerName = "ReportingEntities";
    
        #region Constructors
    
        public ReportingEntities():
            base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public ReportingEntities(string connectionString):
            base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public ReportingEntities(EntityConnection connection):
            base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public IObjectSet<tbEPS_Msg> tbEPS_Msg
        {
            get { return _tbEPS_Msg ?? (_tbEPS_Msg = CreateObjectSet<tbEPS_Msg>("tbEPS_Msg")); }
        }
        private ObjectSet<tbEPS_Msg> _tbEPS_Msg;
    
        public IObjectSet<tbEPS_Organisation> tbEPS_Organisation
        {
            get { return _tbEPS_Organisation ?? (_tbEPS_Organisation = CreateObjectSet<tbEPS_Organisation>("tbEPS_Organisation")); }
        }
        private ObjectSet<tbEPS_Organisation> _tbEPS_Organisation;
    
        public IObjectSet<tbRPT_DailyActivityCP> tbRPT_DailyActivityCP
        {
            get { return _tbRPT_DailyActivityCP ?? (_tbRPT_DailyActivityCP = CreateObjectSet<tbRPT_DailyActivityCP>("tbRPT_DailyActivityCP")); }
        }
        private ObjectSet<tbRPT_DailyActivityCP> _tbRPT_DailyActivityCP;
    
        public IObjectSet<tbRPT_DailyActivityGP> tbRPT_DailyActivityGP
        {
            get { return _tbRPT_DailyActivityGP ?? (_tbRPT_DailyActivityGP = CreateObjectSet<tbRPT_DailyActivityGP>("tbRPT_DailyActivityGP")); }
        }
        private ObjectSet<tbRPT_DailyActivityGP> _tbRPT_DailyActivityGP;
    
        public IObjectSet<tbRPT_HealthBoardContacts> tbRPT_HealthBoardContacts
        {
            get { return _tbRPT_HealthBoardContacts ?? (_tbRPT_HealthBoardContacts = CreateObjectSet<tbRPT_HealthBoardContacts>("tbRPT_HealthBoardContacts")); }
        }
        private ObjectSet<tbRPT_HealthBoardContacts> _tbRPT_HealthBoardContacts;
    
        public IObjectSet<tbRPT_OfflineSites> tbRPT_OfflineSites
        {
            get { return _tbRPT_OfflineSites ?? (_tbRPT_OfflineSites = CreateObjectSet<tbRPT_OfflineSites>("tbRPT_OfflineSites")); }
        }
        private ObjectSet<tbRPT_OfflineSites> _tbRPT_OfflineSites;
    
        public IObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications> tbRPT_OfflineSites_SuppliersToReceiveNotifications
        {
            get { return _tbRPT_OfflineSites_SuppliersToReceiveNotifications ?? (_tbRPT_OfflineSites_SuppliersToReceiveNotifications = CreateObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications>("tbRPT_OfflineSites_SuppliersToReceiveNotifications")); }
        }
        private ObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications> _tbRPT_OfflineSites_SuppliersToReceiveNotifications;
    
        public IObjectSet<tbRPT_OrgSupplier> tbRPT_OrgSupplier
        {
            get { return _tbRPT_OrgSupplier ?? (_tbRPT_OrgSupplier = CreateObjectSet<tbRPT_OrgSupplier>("tbRPT_OrgSupplier")); }
        }
        private ObjectSet<tbRPT_OrgSupplier> _tbRPT_OrgSupplier;
    
        public IObjectSet<tbRPT_SupplierContacts> tbRPT_SupplierContacts
        {
            get { return _tbRPT_SupplierContacts ?? (_tbRPT_SupplierContacts = CreateObjectSet<tbRPT_SupplierContacts>("tbRPT_SupplierContacts")); }
        }
        private ObjectSet<tbRPT_SupplierContacts> _tbRPT_SupplierContacts;

        #endregion
    }
}
