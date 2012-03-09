using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class CheckOfflineNotificationsShouldBeSent
    {
        IRepository _repository;

        public CheckOfflineNotificationsShouldBeSent(IRepository repository)
        {
            _repository = repository;

            setSuppliersToReceiveNotifications();
        }

        // Are there suppliers to receive offline notifications?

        private List<string> suppliersToReceiveNotifications;

        private void setSuppliersToReceiveNotifications()
        {
            // suppliersToReceiveNotifications = _repository.GetSuppliersToReceiveOfflineNotifications();
        }

        public List<string> GetSuppliersToReceiveOfflineNotifications { get { return suppliersToReceiveNotifications; } }

        private bool areThereAnySuppliersToReceiveOfflineNotifications = false;

        private void setAreThereAnySuppliersToReceiveOfflineNotifications()
        {
            if ((suppliersToReceiveNotifications != null) && (suppliersToReceiveNotifications.Count > 0))
                areThereAnySuppliersToReceiveOfflineNotifications = true;
            else
                areThereAnySuppliersToReceiveOfflineNotifications = false;
        }

        public bool AreThereAnySuppliersToReceiveOfflineNotifications { get { return areThereAnySuppliersToReceiveOfflineNotifications; }}

        // Are there any new offline sites to report?

        private List<string> 

        // Is the number of offline sites per healthboard limit active?

        // Have any healthboards exceeded the number of offline sites limit?
    }
}
