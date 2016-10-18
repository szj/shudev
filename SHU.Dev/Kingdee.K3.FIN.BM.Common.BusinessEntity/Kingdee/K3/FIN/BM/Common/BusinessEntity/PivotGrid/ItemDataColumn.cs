namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ItemDataColumn : PivotGridColumn
    {
        public ItemDataColumn(long itemDataId, string itemDataMumber, string itemDataName, Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty itemDataProperty, bool isBugetData) : base(itemDataMumber)
        {
            this.ItemDataId = itemDataId;
            this.ItemDataNumber = itemDataMumber;
            this.ItemDataName = itemDataName;
            this.ItemDataProperty = itemDataProperty;
            this.IsBugetData = isBugetData;
            base.Caption = itemDataName;
            switch (itemDataProperty)
            {
                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Amount:
                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Quantity:
                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Price:
                    base.DataType = typeof(decimal);
                    base.ColumnType = PivotGridColumnType.ValueData;
                    base.ReadOnly = !isBugetData;
                    return;

                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Text:
                    break;

                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Date:
                    base.DataType = typeof(DateTime);
                    base.ColumnType = PivotGridColumnType.ValueData;
                    base.ReadOnly = false;
                    return;

                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Calculate:
                    base.DataType = typeof(decimal);
                    base.ColumnType = PivotGridColumnType.Calculate;
                    base.ReadOnly = false;
                    return;

                case Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Proportion:
                    base.DataType = typeof(decimal);
                    base.ColumnType = PivotGridColumnType.Proportion;
                    base.ReadOnly = true;
                    break;

                default:
                    return;
            }
        }

        public bool IsBugetData { get; private set; }

        public long ItemDataId { get; private set; }

        public string ItemDataName { get; private set; }

        public string ItemDataNumber { get; private set; }

        public Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty ItemDataProperty { get; private set; }
    }
}

