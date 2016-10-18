namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BudgetReportEntity
    {
        public int AmountunitID { get; set; }

        public int CurrencyID { get; set; }

        public string CycleID { get; set; }

        public long DeptOrgID { get; set; }

        public string DeptOrgName { get; set; }

        public string ID { get; set; }

        public long OrgID { get; set; }

        public string OrgTypeID { get; set; }

        public int Period { get; set; }

        public string SampleID { get; set; }

        public int SchemeID { get; set; }

        public List<SheetEntity> SheetEntityList { get; set; }

        public int Year { get; set; }
    }
}

