namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DistributeSampleDetail
    {
        public int DeptOrgId { get; set; }

        public List<int> DimensionIdList { get; set; }

        public List<int> ItemDataTypeList { get; set; }

        public string OrgType { get; set; }

        public int RptScheneGroupId { get; set; }

        public int RptScheneId { get; set; }

        public string SampleId { get; set; }

        public string SampleName { get; set; }

        public string SheetId { get; set; }

        public string SheetName { get; set; }

        public string SourceSampleId { get; set; }
    }
}

