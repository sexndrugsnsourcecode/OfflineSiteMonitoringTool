using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public static class Message
    {
        // Fields used to store msgTypeRid values for various message types
        private static long amsPrescriptionMsgTypeRid = 21;
        private static long amsAmendItemMsgTypeRid = 22;
        private static long amsCancelItemMsgTypeRid = 23;
        private static long amsPrescriptionRequestMsgTypeRid = 24;
        private static long amsDispenseClaimMsgTypeRid = 29;
        private static long amsCancelDispenseClaimMsgTypeRid = 30;

        public static long AMSPrescriptionMsgTypeRid { get { return amsPrescriptionMsgTypeRid; } }
        public static long AMSAmendItemMsgTypeRid { get { return amsAmendItemMsgTypeRid; } }
        public static long AMSCancelItemMsgTypeRid { get { return amsCancelItemMsgTypeRid; } }
        public static long AMSPrescriptionRequestMsgTypeRid { get { return amsPrescriptionRequestMsgTypeRid; } }
        public static long AMSDispenseClaimMsgTypeRid { get { return amsDispenseClaimMsgTypeRid; } }
        public static long AMSCancelDispenseClaimMsgTypeRid { get { return amsCancelDispenseClaimMsgTypeRid; } }
    }
}
