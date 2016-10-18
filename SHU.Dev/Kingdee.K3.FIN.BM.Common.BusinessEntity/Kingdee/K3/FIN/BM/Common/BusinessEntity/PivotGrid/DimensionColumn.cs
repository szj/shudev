namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DimensionColumn : PivotGridColumn
    {
        public DimensionColumn(long dimensionId, string dimensionNumber, string dimensionName, Kingdee.K3.FIN.BM.Common.BusinessEntity.DimensionType dimensionType, string baseDataObjectId) : base(dimensionNumber, typeof(string))
        {
            this.DimensionId = dimensionId;
            this.DimensionNumber = dimensionNumber;
            this.DimensionName = dimensionName;
            this.DimensionType = dimensionType;
            this.BaseDataObjectId = baseDataObjectId;
            base.Caption = dimensionName;
            base.ColumnType = PivotGridColumnType.Dimension;
        }

        public string BaseDataObjectId { get; private set; }

        public long DimensionId { get; private set; }

        public string DimensionName { get; private set; }

        public string DimensionNumber { get; private set; }

        public Kingdee.K3.FIN.BM.Common.BusinessEntity.DimensionType DimensionType { get; private set; }
    }
}

