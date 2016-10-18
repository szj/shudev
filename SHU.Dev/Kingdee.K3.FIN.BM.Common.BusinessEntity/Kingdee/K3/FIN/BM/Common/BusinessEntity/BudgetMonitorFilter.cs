namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BudgetMonitorFilter
    {
        public List<string> BuildStatus { get; set; }

        public List<string> CycleType { get; set; }

        public List<string> ExcuteStatus { get; set; }

        public List<string> Period { get; set; }

        public int SchemeId { get; set; }

        public List<string> Year { get; set; }
    }
}

