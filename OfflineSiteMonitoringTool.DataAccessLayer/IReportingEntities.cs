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
    /// The interface for the specialised object context. This contains all of
    /// the <code>ObjectSet</code> properties that are implemented in both the
    /// functional context class and the mock context class.
    /// </summary>
    public partial interface IReportingEntities
    {
        IObjectSet<tbEPS_Msg> tbEPS_Msg { get; }
        IObjectSet<tbEPS_Organisation> tbEPS_Organisation { get; }
        IObjectSet<tbRPT_DailyActivityCP> tbRPT_DailyActivityCP { get; }
        IObjectSet<tbRPT_DailyActivityGP> tbRPT_DailyActivityGP { get; }
        IObjectSet<tbRPT_HealthBoardContacts> tbRPT_HealthBoardContacts { get; }
        IObjectSet<tbRPT_OfflineSites> tbRPT_OfflineSites { get; }
        IObjectSet<tbRPT_OfflineSites_SuppliersToReceiveNotifications> tbRPT_OfflineSites_SuppliersToReceiveNotifications { get; }
        IObjectSet<tbRPT_OrgSupplier> tbRPT_OrgSupplier { get; }
        IObjectSet<tbRPT_SupplierContacts> tbRPT_SupplierContacts { get; }
    }
}
