namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudgetDataEntity
    {
        public Kingdee.K3.FIN.BM.Common.BusinessEntity.BillUsedValue BillUsedValue { get; set; }

        public Kingdee.K3.FIN.BM.Common.BusinessEntity.BudgetDataValue BudgetDataValue { get; set; }

        public int BusinessType { get; set; }

        public int CurrencyId { get; set; }

        public int DataType { get; set; }

        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public long OrgId { get; set; }

        public string OrgType { get; set; }

        public int Period { get; set; }

        public string PeriodType { get; set; }

        public int RuleId { get; set; }

        public int SchemeId { get; set; }

        public int Year { get; set; }
    }
}

