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
            setAreThereAnySuppliersToReceiveOfflineNotifications();
            setOfflineSitesToBeReported();
            setAreThereOfflineSitesToBeReported();
            setNumberOfOfflineSitesToBeReportedPerHealthboardLimit();
            setHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit();
        }

        // Are there suppliers to receive offline notifications?
        private List<string> suppliersToReceiveNotifications;

        private void setSuppliersToReceiveNotifications()
        {
            suppliersToReceiveNotifications = _repository.GetSuppliersToReceiveOfflineNotifications();
        }

        public List<string> GetSuppliersToReceiveOfflineNotifications { get { return suppliersToReceiveNotifications; } }

        private bool areThereAnySuppliersToReceiveOfflineNotifications;

        private void setAreThereAnySuppliersToReceiveOfflineNotifications()
        {
            if ((suppliersToReceiveNotifications != null) && (suppliersToReceiveNotifications.Count > 0))
                areThereAnySuppliersToReceiveOfflineNotifications = true;
            else
                areThereAnySuppliersToReceiveOfflineNotifications = false;
        }

        public bool AreThereAnySuppliersToReceiveOfflineNotifications { get { return areThereAnySuppliersToReceiveOfflineNotifications; }}

        // Are there any new offline sites to report?
        private List<string> offlineSitesToReport;

        private void setOfflineSitesToBeReported()
        {
            offlineSitesToReport = _repository.GetOfflineSitesToReport();
        }

        public List<string> GetOfflineSitesToBeReported { get { return offlineSitesToReport; } }

        private bool areThereOfflineSitesToBeReported;

        private void setAreThereOfflineSitesToBeReported()
        {
            if ((offlineSitesToReport != null) && (offlineSitesToReport.Count > 0))
                areThereOfflineSitesToBeReported = true;
            else
                areThereOfflineSitesToBeReported = false;
        }

        public bool AreThereOfflineSitesToBeReported { get { return areThereOfflineSitesToBeReported; } }

        // Is the number of offline sites per healthboard limit active?
        private int numberOfOfflineSitesToBeReportedPerHealthboardLimit;

        private void setNumberOfOfflineSitesToBeReportedPerHealthboardLimit()
        {
            numberOfOfflineSitesToBeReportedPerHealthboardLimit = _repository.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();
        }

        // Have any healthboards exceeded the number of offline sites limit?
        private List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit;

        private void setHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit()
        {
            healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit =
                _repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(numberOfOfflineSitesToBeReportedPerHealthboardLimit);
        }

        public List<string> GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit 
            { get { return healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit; } }
    }
}
