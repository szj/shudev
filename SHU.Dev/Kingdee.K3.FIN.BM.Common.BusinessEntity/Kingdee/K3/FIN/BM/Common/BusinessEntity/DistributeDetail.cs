namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.KDSReportCommon.Enums;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeDetail
    {
        public long AcctSysId { get; set; }

        public bool AllowCover { get; set; }

        public bool AllowEdit { get; set; }

        public long CompanyId { get; set; }

        public long DeptOrgID { get; set; }

        public string DistributeID { get; set; }

        public int Fid { get; set; }

        public string Id { get; set; }

        public string NewSampleId { get; set; }

        public string OrgType { get; set; }

        public DateTime ReportBeginDate { get; set; }

        public KDSEnums.enuReportType RptType { get; set; }

        public string SampleId { get; set; }
    }
}

