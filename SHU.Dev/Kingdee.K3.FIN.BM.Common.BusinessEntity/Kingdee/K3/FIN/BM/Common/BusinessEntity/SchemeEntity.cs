namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.Orm.DataEntity;
    using System;
    using System.Runtime.CompilerServices;

    public class SchemeEntity
    {
        public int CalendarID { get; set; }

        public DynamicObjectCollection CalendarPeriod { get; set; }

        public DateTime EndDate { get; set; }

        public int EndYear { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public int StartYear { get; set; }
    }
}

