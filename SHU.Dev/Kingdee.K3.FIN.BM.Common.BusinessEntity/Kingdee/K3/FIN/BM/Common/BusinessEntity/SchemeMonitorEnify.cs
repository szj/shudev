namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    public class SchemeMonitorEnify
    {
        public string ActiveStatus { get; set; }

        public int CalendarID { get; set; }

        public int CycleId { get; set; }

        public long DeptOrgId { get; set; }

        public DateTime EndDate { get; set; }

        public string ExcuteStatus { get; set; }

        public long FID { get; set; }

        public int Id { get; set; }

        public long OrgId { get; set; }

        public string OrgType { get; set; }

        public int Period { get; set; }

        public string PeriodName { get; set; }

        public string PeriodNumber { get; set; }

        public DateTime StartDate { get; set; }

        public int Year { get; set; }
    }
}

