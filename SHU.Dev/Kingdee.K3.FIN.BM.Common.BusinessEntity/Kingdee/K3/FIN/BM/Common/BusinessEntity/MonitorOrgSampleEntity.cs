namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MonitorOrgSampleEntity
    {
        public string BuildStatus { get; set; }

        public List<int> ContainPeriodList { get; set; }

        public int DeptOrgId { get; set; }

        public string DeptOrgName { get; set; }

        public string OrgType { get; set; }

        public string PeriodType { get; set; }

        public DateTime ReportBeginDate { get; set; }

        public string SampleId { get; set; }

        public string SampleName { get; set; }

        public string SampleNumber { get; set; }
    }
}

