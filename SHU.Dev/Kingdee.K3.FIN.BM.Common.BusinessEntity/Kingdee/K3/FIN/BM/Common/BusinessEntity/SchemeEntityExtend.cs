namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SchemeEntityExtend : SchemeEntity
    {
        public List<int> PeriodTypeList { get; set; }

        public List<SchemeOrgDetail> schemeDetailList { get; set; }
    }
}

