namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MonitorExecuteStatusDto
    {
        public string ExcuteStatus { get; set; }

        public List<int> MonitorOrgList { get; set; }

        public string PeriodName { get; set; }

        public string PeriodNumber { get; set; }

        public int PeriodType { get; set; }

        public int SchemeId { get; set; }
    }
}

