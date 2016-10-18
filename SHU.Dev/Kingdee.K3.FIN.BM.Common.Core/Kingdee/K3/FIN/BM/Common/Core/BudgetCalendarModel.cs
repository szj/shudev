namespace Kingdee.K3.FIN.BM.Common.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudgetCalendarModel
    {
        public BudgetCalendarModel()
        {
            this.ID = "0";
            this.Period = 1;
        }

        public BudgetCalendarModel(DateTime periodStartDate, DateTime periodEndDate)
        {
            this.ID = "0";
            this.Period = 1;
            this.PeriodStartDate = periodStartDate;
            this.PeriodEndDate = periodEndDate;
        }

        public string ID { get; set; }

        public BudgetCalendarModel ParentBudgetCalendarModel { get; set; }

        public string ParentID { get; set; }

        public int Period { get; set; }

        public DateTime PeriodEndDate { get; set; }

        public string PeriodName { get; set; }

        public string PeriodNumber { get; set; }

        public string PeriodShortName { get; set; }

        public DateTime PeriodStartDate { get; set; }

        public int PeriodType { get; set; }

        public int PeriodYear { get; set; }
    }
}

