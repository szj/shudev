namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MonitorOrgEntity
    {
        public string ActiveStatus { get; set; }

        public string BuildStatus { get; set; }

        public int DeptOrgId { get; set; }

        public string ExcuteStatus { get; set; }

        public List<MonitorOrgSampleEntity> MonitorOrgSampleEntityList { get; set; }

        public int OrgId { get; set; }

        public string OrgName { get; set; }

        public string OrgNumber { get; set; }

        public string OrgType { get; set; }
    }
}

