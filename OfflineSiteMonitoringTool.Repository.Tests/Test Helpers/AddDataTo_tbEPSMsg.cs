using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbEPSMsg
    {
        public static tbEPS_Msg AddRow(DateTime date)
        {
            tbEPS_Msg row = new tbEPS_Msg
            {
                rid = 1,
                exId = new Guid(),
                exDatetime = date,
                exMepRoleId = 1,
                exMsgSourceEpocRid = 11111111,
                exMsgDestinationEpocRid = 22222222,
                msgCxRid = 1,
                msgTypeRid = 1,
                xmlSourceComplete = true,
                xmlSource = "test",
                requestMsgRid = 1,
                responseSendCount = 1,
                schemaVersion = "test",
                priorityId = 1,
                statusId = 1,
                softwareName = "test",
                softwareVersion = "test",
                softwareAuthor = "test",
                appServiceName = "test",
                appServiceVersion = 1,
                sttl = 1,
                id = new Guid(),
                stepRef = 1,
                datetime = date,
                categoryId = 1,
                lastBlockId = new Guid(),
                bodyTypeCount = 1,
                batched = false,
                deletableOn = DateTime.Today,
                msgTxSenderType = "test",
                msgTxSenderId = "test",
                msgTxSenderName = "test",
                auditCreatedOn = date,
                auditCreatedBy = "test",
                auditUpdatedOn = DateTime.Today,
                auditUpdatedBy = "test",
                deleteOn = DateTime.Today,
                deleteOnNextReview = DateTime.Today,
                deleteOnFixed = false,
                deleteFailedOn = DateTime.Today
            };

            return row;
        }
    }
}
