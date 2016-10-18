namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DistributeSample
    {
        public List<int> ContainCycleIdList { get; set; }

        public int CycleId { get; set; }

        public DateTime ReportBeginDate { get; set; }

        public DateTime ReportBeginDate_Change { get; set; }

        public string SampleId { get; set; }
    }
}

