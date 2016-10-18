namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    public class OrgEntity
    {
        public int DeptOrgId { get; set; }

        public int LocaleId { get; set; }

        public string OrgName { get; set; }

        public string OrgNumber { get; set; }

        public string OrgType { get; set; }

        public int ParentOrgId { get; set; }
    }
}

