namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MonitorReprotDto
    {
        public List<int> ContainCycleIdList { get; set; }

        public int CycleId { get; set; }

        public string DeptOrgId { get; set; }

        public string DocumentStatus { get; set; }

        public string FID { get; set; }

        public string OrgTypeId { get; set; }

        public int Period { get; set; }

        public DateTime PeriodEndDate { get; set; }

        public DateTime PeriodStartDate { get; set; }

        public DateTime ReportBeginDate { get; set; }

        public string SampleId { get; set; }

        public string SchemeId { get; set; }

        public int Year { get; set; }
    }
}

