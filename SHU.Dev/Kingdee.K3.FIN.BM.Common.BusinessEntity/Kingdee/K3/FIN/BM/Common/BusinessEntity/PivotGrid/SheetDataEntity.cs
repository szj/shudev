namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SheetDataEntity
    {
        [DefaultValue(0)]
        public int CalendarId { get; set; }

        public byte[] Data { get; set; }

        public string DimensionGroupKey { get; set; }

        public int Index { get; set; }

        public bool IsBudgetOrg
        {
            get
            {
                return (this.PivotGridData.LstDimensionValue.Count<DimensionValue>(dimValue => dimValue.IsBudgetOrg) > 0);
            }
        }

        public byte[] Layout { get; set; }

        public string Name { get; set; }

        public PivotGridDataModel PivotGridData { get; set; }

        public long RptSchemeId { get; set; }

        public Guid SampleSheetId { get; set; }

        public long SchemeId { get; set; }

        public Guid SheetId { get; set; }
    }
}

