namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeTargetExtend : DistributeTarget
    {
        public DateTime ReportBeginData { get; set; }
    }
}

