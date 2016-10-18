namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.KDSReportCommon.Enums;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeBMReport
    {
        public DateTime AuditDate { get; set; }

        public int AuditorID { get; set; }

        public int CreateOrgId { get; set; }

        public int CreateStyle { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorID { get; set; }

        public int CycleId { get; set; }

        public DateTime Day { get; set; }

        public int DocumentStatus { get; set; }

        public DateTime ForbidDate { get; set; }

        public int ForbidderID { get; set; }

        public int ForBidStatus { get; set; }

        public int GroupId { get; set; }

        public string ID { get; set; }

        public int ModifierID { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Number { get; set; }

        public KDSEnums.enuReportType RptType { get; set; }

        public int UseOrgId { get; set; }
    }
}

