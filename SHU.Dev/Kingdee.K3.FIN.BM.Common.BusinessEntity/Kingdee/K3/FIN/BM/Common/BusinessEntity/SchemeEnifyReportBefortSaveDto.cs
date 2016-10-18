namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SchemeEnifyReportBefortSaveDto
    {
        public int CalendarId { get; set; }

        public long DeptOrgId { get; set; }

        public string DetailId { get; set; }

        public DateTime EndDate { get; set; }

        public int Fid { get; set; }

        public long OrgId { get; set; }

        public string OrgType { get; set; }

        public DateTime? ReportBefortDate { get; set; }

        public Dictionary<string, DateTime?> SampleDateList { get; set; }
    }
}

