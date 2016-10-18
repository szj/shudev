namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudgetReportStatusTipEntity
    {
        public int FId { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int Seq { get; set; }

        public string Status { get; set; }
    }
}

