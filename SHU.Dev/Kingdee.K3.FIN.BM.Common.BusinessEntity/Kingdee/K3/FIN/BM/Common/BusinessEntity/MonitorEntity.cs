namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MonitorEntity
    {
        public string BuildStatus { get; set; }

        public int CalendarID { get; set; }

        public DateTime EndDate { get; set; }

        public string ExcuteStatus { get; set; }

        public List<MonitorOrgEntity> MonitorOrgEntityList { get; set; }

        public int Period { get; set; }

        public string PeriodName { get; set; }

        public string PeriodNumber { get; set; }

        public int PeriodType { get; set; }

        public int SchemeId { get; set; }

        public DateTime StartDate { get; set; }

        public int Year { get; set; }
    }
}

