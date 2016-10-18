namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.K3.FIN.ReportEntity;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [Serializable]
    public class ReportDataModel
    {
        private SheetDataModelCollection _sheetModels;

        public ReportDataModel()
        {
            this._sheetModels = new SheetDataModelCollection();
            this._sheetModels.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SheetModels_CollectionChanged);
        }

        public ReportDataModel(ReportDataEntity entity)
        {
            this._sheetModels = new SheetDataModelCollection();
            this.ReportProperty = entity.ReportProperty;
            this._sheetModels.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SheetModels_CollectionChanged);
            foreach (SheetDataEntity entity2 in entity.Sheets)
            {
                SheetDataModel sheeModel = new SheetDataModel(entity2);
                this.AddSheet(sheeModel);
            }
        }

        public ReportDataModel(BMReportProperty property)
        {
            this._sheetModels = new SheetDataModelCollection();
            this.ReportProperty = property;
            this._sheetModels.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SheetModels_CollectionChanged);
        }

        public void AddSheet(SheetDataModel sheeModel)
        {
            this._sheetModels.Add(sheeModel);
        }

        public ReportDataEntity GetDataEntity(bool hasBlobData = true)
        {
            ReportDataEntity entity = new ReportDataEntity {
                ReportProperty = this.ReportProperty
            };
            int num = 0;
            foreach (SheetDataModel model in from s in this.SheetModels
                orderby s.Index
                select s)
            {
                PivotGridDataModel pivotGridModel = model.GetPivotGridModel();
                if (pivotGridModel.Columns.Count != 0)
                {
                    SheetDataEntity sheet = new SheetDataEntity {
                        SheetId = model.SheetId,
                        Name = model.SheetName,
                        Index = num++,
                        DimensionGroupKey = model.DimensionGroupKey
                    };
                    if (hasBlobData)
                    {
                        sheet.Data = model.GetDataBlob();
                    }
                    sheet.SchemeId = model.SchemeId;
                    sheet.CalendarId = model.CalendarId;
                    sheet.PivotGridData = pivotGridModel;
                    entity.Sheets.Add(sheet);
                }
            }
            return entity;
        }

        public Dictionary<int, string> GetDefaultSheetOrder()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < this.SheetModels.Count; i++)
            {
                dictionary.Add(i + 1, this.SheetModels[i].SheetName);
            }
            return dictionary;
        }

        public ReportDataEntity GetModelDataEntity()
        {
            return new ReportDataEntity();
        }

        public void InsertSheet(int index, SheetDataModel sheeModel)
        {
            this._sheetModels.Insert(index, sheeModel);
        }

        public void RemoveSheet(SheetDataModel sheeModel)
        {
            this._sheetModels.Remove(sheeModel);
        }

        public void RemoveSheet(int index)
        {
            this._sheetModels.Remove(index);
        }

        private void SheetModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj2 in e.NewItems)
                    {
                        SheetDataModel model = obj2 as SheetDataModel;
                        model.ReportModel = this;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj3 in e.OldItems)
                    {
                        SheetDataModel model2 = obj3 as SheetDataModel;
                        model2.ReportModel = null;
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                {
                    SheetDataModel model3 = e.OldItems[0] as SheetDataModel;
                    SheetDataModel model4 = e.NewItems[0] as SheetDataModel;
                    model3.ReportModel = null;
                    model4.ReportModel = this;
                    break;
                }
                default:
                    return;
            }
        }

        public BMReportProperty ReportProperty { get; private set; }

        public SheetDataModel SelectedSheet
        {
            get
            {
                if (this.SelectedSheetIndex >= this._sheetModels.Count)
                {
                    this.SelectedSheetIndex = this._sheetModels.Count - 1;
                }
                return this._sheetModels.SingleOrDefault<SheetDataModel>(s => (s.Index == this.SelectedSheetIndex));
            }
        }

        [DefaultValue(0)]
        public int SelectedSheetIndex { get; set; }

        public SheetDataModelCollection SheetModels
        {
            get
            {
                return this._sheetModels;
            }
        }
    }
}

