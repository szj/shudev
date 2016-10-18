namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SheetEntity
    {
        public List<int> ContainCyclys { get; set; }

        public List<int> DeptOrgList { get; set; }

        public string RptId { get; set; }

        public string SheetId { get; set; }
    }
}

