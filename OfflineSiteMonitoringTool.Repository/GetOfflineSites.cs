using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<string> GetOfflineSites(List<string> sitesToCheckMessagingActivityFor, DateTime lastBusinessDay)
        {
            // Specification at time of writing this tool stated we are primarily interested in
            // whether or not organisations have submitted AMS messages so this method delegates
            // to the appropriate method. This can be edited in future to consider other message types.

            List<string> sitesWhoHaveNotSentAMSMessagesSinceSpecifiedDate = new List<string>();

            sitesWhoHaveNotSentAMSMessagesSinceSpecifiedDate =
                ExecuteDbQuery(getSitesWhoHaveNotSentAMSMessagesSinceSpecifiedDate, sitesToCheckMessagingActivityFor, lastBusinessDay);

            return sitesWhoHaveNotSentAMSMessagesSinceSpecifiedDate;
        }

        private List<string> getSitesWhoHaveNotSentAMSMessagesSinceSpecifiedDate(List<string> sitesToCheckMessagingActivityFor, DateTime lastBusinessDay)
        {
            // Collections to hold AMS message type RIDs for the messages we'll be checking for
            List<long> cpAMSMessageTypeRids = GetCPAMSMessageTypes();
            List<long> gpAMSMessageTypeRids = GetGPAMSMessageTypes();
            List<long> validMessageTypes = new List<long>();
            validMessageTypes.AddRange(cpAMSMessageTypeRids);
            validMessageTypes.AddRange(gpAMSMessageTypeRids);

            // Need to grab a list of all 'online' orgs then create a loop removing each org returned from the list of 
            // eligable orgs passed into method
            var onlineSites = (from x in _reportingEntity.tbEPS_Msg
                               where x.auditCreatedOn > lastBusinessDay
                               && validMessageTypes.Contains(x.msgTypeRid)
                               select x.msgTxSenderId).Distinct();

            List<string> offlineSitesRaw = (sitesToCheckMessagingActivityFor.Except(onlineSites)).ToList<string>();

            List<string> offlineSites = CleanData(offlineSitesRaw);

            return offlineSites;
        }

        /// <summary>
        /// Helper method which returns a list containing the msgTypeRid values 
        /// of the AMS messageTypes used to determine whether or not a CP is offline
        /// </summary>
        /// <returns></returns>
        private List<long> GetCPAMSMessageTypes()
        {
            List<long> cpAMSMessageTypeRids = new List<long>() 
                                                {   
                                                    Model.Message.AMSPrescriptionRequestMsgTypeRid, 
                                                    Model.Message.AMSDispenseClaimMsgTypeRid, 
                                                    Model.Message.AMSCancelDispenseClaimMsgTypeRid
                                                };

            return cpAMSMessageTypeRids;
        }

        /// <summary>
        /// Helper method which returns a list containing the msgTypeRid values 
        /// of the AMS messageTypes used to determine whether or not a GP is offline
        /// </summary>
        /// <returns></returns>
        private List<long> GetGPAMSMessageTypes()
        {
            List<long> gpAMSMessageTypeRids = new List<long>() 
                                                { 
                                                    Model.Message.AMSPrescriptionMsgTypeRid, 
                                                    Model.Message.AMSAmendItemMsgTypeRid, 
                                                    Model.Message.AMSCancelItemMsgTypeRid
                                                };

            return gpAMSMessageTypeRids;
        }
    }
}
