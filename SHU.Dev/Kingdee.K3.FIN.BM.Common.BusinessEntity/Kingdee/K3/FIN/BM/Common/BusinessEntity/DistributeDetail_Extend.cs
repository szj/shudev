namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeDetail_Extend : DistributeDetail
    {
        public string DeptOrgName { get; set; }

        public string SampleName { get; set; }
    }
}

