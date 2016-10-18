namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SchemeOrgDetail
    {
        public long DeptOrgId { get; set; }

        public List<DistributeSample> DistributeSampleList { get; set; }

        public DateTime EffectEndDate { get; set; }

        public DateTime EffectStartDate { get; set; }

        public long OrgId { get; set; }

        public string OrgType { get; set; }

        public DateTime StartDate { get; set; }
    }
}

