namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ReportDataEntity
    {
        public ReportDataEntity()
        {
            this.Sheets = new SheetDataEntityCollection();
        }

        public BMReportProperty ReportProperty { get; set; }

        public SheetDataEntityCollection Sheets { get; set; }
    }
}

