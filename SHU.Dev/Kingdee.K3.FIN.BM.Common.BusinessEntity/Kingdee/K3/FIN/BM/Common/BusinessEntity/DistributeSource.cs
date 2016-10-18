namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeSource
    {
        public string DistributeID { get; set; }

        public int Fid { get; set; }

        public DateTime ReportBeginDate { get; set; }

        public int RptType { get; set; }

        public string SampleId { get; set; }
    }
}

