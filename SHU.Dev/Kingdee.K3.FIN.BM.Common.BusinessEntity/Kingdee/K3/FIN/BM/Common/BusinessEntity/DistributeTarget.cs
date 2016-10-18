namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeTarget
    {
        public bool AllowCover { get; set; }

        public bool AllowEdit { get; set; }

        public long DeptOrgID { get; set; }

        public string DeptOrgName { get; set; }

        public string DistributeID { get; set; }

        public string OrgId { get; set; }

        public string OrgType { get; set; }
    }
}

