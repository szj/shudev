namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.ReportEntity;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;

    [Serializable]
    public class SheetDataModel
    {
        private PivotGridDataModel _pivotGridModel;
        private ReportDataModel _reportModel;
        private int j = 200;

        public SheetDataModel(SheetDataEntity sheetDataEntity)
        {
            this.HeaderDimensions = new DimensionColumnCollection();
            this.RowDimensions = new DimensionColumnCollection();
            this.ColDimensions = new DimensionColumnCollection();
            this.DataDimensions = new ItemDataColumnCollection();
            this._pivotGridModel = new PivotGridDataModel(sheetDataEntity.SheetId, sheetDataEntity.Name);
            this._pivotGridModel.LstDimensionValue = sheetDataEntity.PivotGridData.LstDimensionValue;
            this.SheetId = sheetDataEntity.SheetId;
            this.SchemeId = sheetDataEntity.SchemeId;
            this.CalendarId = sheetDataEntity.CalendarId;
            this.SampleSheetId = sheetDataEntity.SampleSheetId;
            this.SheetName = sheetDataEntity.Name;
            this.Index = sheetDataEntity.Index;
            this.DimensionGroupKey = sheetDataEntity.DimensionGroupKey;
            this.Data = sheetDataEntity.Data;
            this.HeaderDimensions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Dimensions_ColumnCollectionChangedHandle);
            this.RowDimensions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Dimensions_ColumnCollectionChangedHandle);
            this.ColDimensions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Dimensions_ColumnCollectionChangedHandle);
            this.DataDimensions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Dimensions_ColumnCollectionChangedHandle);
            if (sheetDataEntity.Data != null)
            {
                this.SetData(sheetDataEntity.Data);
            }
        }

        public void ClearDimensions()
        {
            this.HeaderDimensions.Clear();
            this.RowDimensions.Clear();
            this.ColDimensions.Clear();
            this.DataDimensions.Clear();
            this._pivotGridModel = new PivotGridDataModel(this.SheetId, this.SheetName);
        }

        private XmlElement CreateDataColumnPart(XmlDocument xml, ItemDataColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("Column");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("Caption");
                newChild.AppendChild(xml.CreateTextNode(column.Caption));
                element.AppendChild(newChild);
                XmlElement element3 = xml.CreateElement("ColumnName");
                element3.AppendChild(xml.CreateTextNode(column.ColumnName));
                element.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnType");
                element4.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                element.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("DataType");
                element5.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                element.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("Formula");
                element6.AppendChild(xml.CreateTextNode(column.Formula));
                element.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("ItemDataType");
                element7.AppendChild(xml.CreateTextNode(column.ItemDataType.ToString()));
                element.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("ProportionDataColumn");
                element8.AppendChild(xml.CreateTextNode(column.ProportionDataColumn));
                element.AppendChild(element8);
                XmlElement element9 = xml.CreateElement("ReadOnly");
                element9.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                element.AppendChild(element9);
                XmlElement element10 = xml.CreateElement("Area");
                element10.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                element.AppendChild(element10);
            }
            return element;
        }

        private XmlElement CreateDataColumnPartDA(XmlDocument xml, ItemDataColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("DataAreaColumns");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("DataAreaColumn");
                XmlElement element3 = xml.CreateElement("Caption");
                element3.AppendChild(xml.CreateTextNode(column.Caption));
                newChild.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnName");
                element4.AppendChild(xml.CreateTextNode(column.ColumnName));
                newChild.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("ColumnType");
                element5.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                newChild.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("DataType");
                element6.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                newChild.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("Formula");
                element7.AppendChild(xml.CreateTextNode(column.Formula));
                newChild.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("ItemDataType");
                element8.AppendChild(xml.CreateTextNode(column.ItemDataType.ToString()));
                newChild.AppendChild(element8);
                XmlElement element9 = xml.CreateElement("BusinessType");
                element9.AppendChild(xml.CreateTextNode(column.BusinessType.ToString()));
                newChild.AppendChild(element9);
                XmlElement element10 = xml.CreateElement("ProportionDataColumn");
                element10.AppendChild(xml.CreateTextNode(column.ProportionDataColumn));
                newChild.AppendChild(element10);
                XmlElement element11 = xml.CreateElement("ReadOnly");
                element11.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                newChild.AppendChild(element11);
                XmlElement element12 = xml.CreateElement("Area");
                element12.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                newChild.AppendChild(element12);
                XmlElement element13 = xml.CreateElement("FormatString");
                element13.AppendChild(xml.CreateTextNode(column.FormatString.ToString()));
                newChild.AppendChild(element13);
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement CreateDataPart(XmlDocument xml)
        {
            XmlElement element = xml.CreateElement("Data");
            if (this._pivotGridModel.Rows.Count > 0)
            {
                int num = 0;
                foreach (PivotGridRow row in this._pivotGridModel.Rows)
                {
                    XmlElement newChild = xml.CreateElement("Row");
                    XmlAttribute node = xml.CreateAttribute("Index");
                    node.Value = num++.ToString();
                    newChild.Attributes.Append(node);
                    element.AppendChild(newChild);
                    foreach (PivotGridColumn column in row.Columns)
                    {
                        if (column.ColumnType != PivotGridColumnType.Proportion)
                        {
                            XmlElement element3 = xml.CreateElement("Column");
                            XmlAttribute attribute2 = xml.CreateAttribute("Name");
                            attribute2.Value = column.ColumnName;
                            element3.Attributes.Append(attribute2);
                            newChild.AppendChild(element3);
                            PivotGridCellValue cellValue = row.GetCellValue(column.ColumnName);
                            if (column.ColumnType == PivotGridColumnType.Dimension)
                            {
                                XmlElement element4 = xml.CreateElement("ID");
                                element4.AppendChild(xml.CreateTextNode(cellValue.Id));
                                element3.AppendChild(element4);
                                XmlElement element5 = xml.CreateElement("Number");
                                element5.AppendChild(xml.CreateTextNode(cellValue.Number));
                                element3.AppendChild(element5);
                            }
                            if (column.ColumnType == PivotGridColumnType.ValueData)
                            {
                                XmlElement element6 = xml.CreateElement("Year");
                                element6.AppendChild(xml.CreateTextNode(cellValue.Year.ToString()));
                                element3.AppendChild(element6);
                                XmlElement element7 = xml.CreateElement("Period");
                                element7.AppendChild(xml.CreateTextNode(cellValue.Period.ToString()));
                                element3.AppendChild(element7);
                                XmlElement element8 = xml.CreateElement("PeriodType");
                                element8.AppendChild(xml.CreateTextNode(((int) cellValue.PeriodType).ToString()));
                                element3.AppendChild(element8);
                                XmlElement element9 = xml.CreateElement("ValueDataType");
                                element9.AppendChild(xml.CreateTextNode(cellValue.ValueDataType.ToString()));
                                element3.AppendChild(element9);
                                XmlElement element10 = xml.CreateElement("BusinessType");
                                element10.AppendChild(xml.CreateTextNode(cellValue.BusinessType.ToString()));
                                element3.AppendChild(element10);
                                XmlElement element11 = xml.CreateElement("Formula");
                                element11.AppendChild(xml.CreateTextNode(cellValue.Formula));
                                element3.AppendChild(element11);
                                XmlElement element12 = xml.CreateElement("DimensionEntityList");
                                foreach (DimensionEntity entity in cellValue.LstDimensionEntity)
                                {
                                    XmlElement element13 = xml.CreateElement("DimensionEntity");
                                    XmlElement element14 = xml.CreateElement("ID");
                                    element14.AppendChild(xml.CreateTextNode(Convert.ToString(entity.Id)));
                                    element13.AppendChild(element14);
                                    XmlElement element15 = xml.CreateElement("DimValue");
                                    element15.AppendChild(xml.CreateTextNode(Convert.ToString(entity.DimValue)));
                                    element13.AppendChild(element15);
                                    XmlElement element16 = xml.CreateElement("IsBudgetOrg");
                                    element16.AppendChild(xml.CreateTextNode(entity.IsBudgetOrg.ToString()));
                                    element13.AppendChild(element16);
                                    element12.AppendChild(element13);
                                }
                                element3.AppendChild(element12);
                            }
                            XmlElement element17 = xml.CreateElement("Value");
                            if (this.ReportModel.ReportProperty.IsSample && (cellValue.ValueDataType > 0))
                            {
                                element17.AppendChild(xml.CreateTextNode("0"));
                            }
                            else
                            {
                                element17.AppendChild(xml.CreateTextNode(Convert.ToString(cellValue.Value)));
                            }
                            element3.AppendChild(element17);
                        }
                    }
                }
            }
            return element;
        }

        private XmlElement CreateDimensionColumnPart(XmlDocument xml, DimensionColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("Column");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("Caption");
                newChild.AppendChild(xml.CreateTextNode(column.Caption));
                element.AppendChild(newChild);
                XmlElement element3 = xml.CreateElement("ColumnName");
                element3.AppendChild(xml.CreateTextNode(column.ColumnName));
                element.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnType");
                element4.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                element.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("DataType");
                element5.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                element.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("ReadOnly");
                element6.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                element.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("Area");
                element7.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                element.AppendChild(element7);
            }
            return element;
        }

        private XmlElement CreateDimensionColumnPartCA(XmlDocument xml, DimensionColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("ColumnAreaColumns");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("ColumnAreaColumn");
                XmlElement element3 = xml.CreateElement("Caption");
                element3.AppendChild(xml.CreateTextNode(column.Caption));
                newChild.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnName");
                element4.AppendChild(xml.CreateTextNode(column.ColumnName));
                newChild.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("ColumnType");
                element5.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                newChild.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("DataType");
                element6.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                newChild.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("ItemDataType");
                element7.AppendChild(xml.CreateTextNode(column.ItemDataType.ToString()));
                newChild.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("BusinessType");
                element8.AppendChild(xml.CreateTextNode(column.BusinessType.ToString()));
                newChild.AppendChild(element8);
                XmlElement element9 = xml.CreateElement("ReadOnly");
                element9.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                newChild.AppendChild(element9);
                XmlElement element10 = xml.CreateElement("Area");
                element10.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                newChild.AppendChild(element10);
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement CreateDimensionColumnPartHA(XmlDocument xml, DimensionColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("HeaderAreaColumns");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("HeaderAreaColumn");
                XmlElement element3 = xml.CreateElement("Caption");
                element3.AppendChild(xml.CreateTextNode(column.Caption));
                newChild.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnName");
                element4.AppendChild(xml.CreateTextNode(column.ColumnName));
                newChild.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("ColumnType");
                element5.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                newChild.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("DataType");
                element6.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                newChild.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("ReadOnly");
                element7.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                newChild.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("Area");
                element8.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                newChild.AppendChild(element8);
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement CreateDimensionColumnPartRA(XmlDocument xml, DimensionColumnCollection columns)
        {
            XmlElement element = xml.CreateElement("RowAreaColumns");
            foreach (PivotGridColumn column in columns)
            {
                XmlElement newChild = xml.CreateElement("RowAreaColumn");
                XmlElement element3 = xml.CreateElement("Caption");
                element3.AppendChild(xml.CreateTextNode(column.Caption));
                newChild.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("ColumnName");
                element4.AppendChild(xml.CreateTextNode(column.ColumnName));
                newChild.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("ColumnType");
                element5.AppendChild(xml.CreateTextNode(((int) column.ColumnType).ToString()));
                newChild.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("DataType");
                element6.AppendChild(xml.CreateTextNode(column.DataType.ToString()));
                newChild.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("ReadOnly");
                element7.AppendChild(xml.CreateTextNode(column.ReadOnly.ToString()));
                newChild.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("Area");
                element8.AppendChild(xml.CreateTextNode(column.Area.ToString()));
                newChild.AppendChild(element8);
                element.AppendChild(newChild);
            }
            return element;
        }

        private XmlElement CreateShcemaPart(XmlDocument xml)
        {
            XmlElement newChild = xml.CreateElement("Schema");
            xml.DocumentElement.AppendChild(newChild);
            if (this.HeaderDimensions.Count > 0)
            {
                XmlElement element2 = xml.CreateElement("HeaderArea");
                newChild.AppendChild(element2);
                element2.AppendChild(this.CreateDimensionColumnPartHA(xml, this.HeaderDimensions));
            }
            if (this.ColDimensions.Count > 0)
            {
                XmlElement element3 = xml.CreateElement("ColumnArea");
                newChild.AppendChild(element3);
                element3.AppendChild(this.CreateDimensionColumnPartCA(xml, this.ColDimensions));
            }
            if (this.RowDimensions.Count > 0)
            {
                XmlElement element4 = xml.CreateElement("RowArea");
                newChild.AppendChild(element4);
                element4.AppendChild(this.CreateDimensionColumnPartRA(xml, this.RowDimensions));
            }
            if (this.DataDimensions.Count > 0)
            {
                XmlElement element5 = xml.CreateElement("DataArea");
                newChild.AppendChild(element5);
                element5.AppendChild(this.CreateDataColumnPartDA(xml, this.DataDimensions));
            }
            if (this.GroupingColumns.Count > 0)
            {
                XmlElement element6 = xml.CreateElement("Grouping");
                newChild.AppendChild(element6);
                element6.AppendChild(xml.CreateTextNode(string.Join(",", this.GroupingColumns)));
            }
            return newChild;
        }

        private XmlElement CreateSheetPropertyPart(XmlDocument xml)
        {
            XmlElement newChild = xml.CreateElement("Property");
            xml.DocumentElement.AppendChild(newChild);
            XmlElement element2 = xml.CreateElement("ReportNumber");
            element2.AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.Number));
            newChild.AppendChild(element2);
            XmlElement element3 = xml.CreateElement("ReportName");
            element3.AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.Name));
            newChild.AppendChild(element3);
            XmlElement element4 = xml.CreateElement("ReportID");
            element4.AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.ID));
            newChild.AppendChild(element4);
            XmlElement element5 = xml.CreateElement("ReportType");
            element5.AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.ReportType.ToString()));
            newChild.AppendChild(element5);
            XmlElement element6 = xml.CreateElement("SheetId");
            element6.AppendChild(xml.CreateTextNode(this.SheetId.ToString()));
            newChild.AppendChild(element6);
            XmlElement element7 = xml.CreateElement("SheetName");
            element7.AppendChild(xml.CreateTextNode(this.SheetName));
            newChild.AppendChild(element7);
            xml.CreateElement("IsSample").AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.IsSample.ToString()));
            newChild.AppendChild(element5);
            XmlElement element9 = xml.CreateElement("SchemeId");
            element9.AppendChild(xml.CreateTextNode(this.SchemeId.ToString()));
            newChild.AppendChild(element9);
            XmlElement element10 = xml.CreateElement("CalendarID");
            element10.AppendChild(xml.CreateTextNode(this.CalendarId.ToString()));
            newChild.AppendChild(element10);
            XmlElement element11 = xml.CreateElement("MinReportCycle");
            element11.AppendChild(xml.CreateTextNode(((int) this.MinReportCycle).ToString()));
            newChild.AppendChild(element11);
            XmlElement element12 = xml.CreateElement("ReportCycles");
            if (this.ReportCycles != null)
            {
                element12.AppendChild(xml.CreateTextNode(string.Join<int>(",", from c in this.ReportCycles select (int) c)));
            }
            newChild.AppendChild(element12);
            XmlElement element13 = xml.CreateElement("Org");
            XmlAttribute node = xml.CreateAttribute("ID");
            node.Value = this.ReportModel.ReportProperty.OrgId.ToString();
            element13.Attributes.Append(node);
            XmlAttribute attribute2 = xml.CreateAttribute("Number");
            attribute2.Value = this.ReportModel.ReportProperty.OrgNumber;
            element13.Attributes.Append(attribute2);
            element13.AppendChild(xml.CreateTextNode(this.ReportModel.ReportProperty.OrgName));
            newChild.AppendChild(element13);
            XmlElement element14 = xml.CreateElement("Currency");
            XmlAttribute attribute3 = xml.CreateAttribute("ID");
            attribute3.Value = this.ReportModel.ReportProperty.CurrencyId.ToString();
            element14.Attributes.Append(attribute3);
            XmlAttribute attribute4 = xml.CreateAttribute("Number");
            attribute4.Value = this.ReportModel.ReportProperty.CurrencyNumber;
            element14.Attributes.Append(attribute4);
            newChild.AppendChild(element14);
            XmlElement element15 = xml.CreateElement("CurrencyUnit");
            XmlAttribute attribute5 = xml.CreateAttribute("ID");
            attribute5.Value = this.ReportModel.ReportProperty.CurrencyUnitId.ToString();
            element15.Attributes.Append(attribute5);
            XmlAttribute attribute6 = xml.CreateAttribute("Number");
            attribute6.Value = this.ReportModel.ReportProperty.CurrencyUnitNumber;
            element15.Attributes.Append(attribute6);
            newChild.AppendChild(element15);
            XmlElement element16 = xml.CreateElement("EditorType");
            newChild.AppendChild(element16);
            XmlElement element17 = xml.CreateElement("EditorID");
            newChild.AppendChild(element17);
            XmlElement element18 = xml.CreateElement("DimensionGroupKey");
            element18.AppendChild(xml.CreateTextNode(this.DimensionGroupKey));
            newChild.AppendChild(element18);
            return newChild;
        }

        private void Dimensions_ColumnCollectionChangedHandle(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj2 in e.NewItems)
                    {
                        PivotGridColumn column = obj2 as PivotGridColumn;
                        this._pivotGridModel.Columns.Add(column);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj3 in e.OldItems)
                    {
                        PivotGridColumn column2 = obj3 as PivotGridColumn;
                        this._pivotGridModel.Columns.Remove(column2);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                {
                    PivotGridColumn column3 = e.OldItems[0] as PivotGridColumn;
                    PivotGridColumn column4 = e.NewItems[0] as PivotGridColumn;
                    this._pivotGridModel.Columns[column3.ColumnName] = column4;
                    break;
                }
                default:
                    return;
            }
        }

        public byte[] GetDataBlob()
        {
            string dataXml = this.GetDataXml();
            byte[] bytes = Encoding.UTF8.GetBytes(dataXml);
            KDZipLib lib = new KDZipLib();
            return lib.Zip(bytes);
        }

        public SheetDataEntity GetDataEntity()
        {
            SheetDataEntity entity = new SheetDataEntity {
                SheetId = this.SheetId,
                SchemeId = this.SchemeId,
                CalendarId = this.CalendarId,
                SampleSheetId = this.SampleSheetId,
                Name = this.SheetName,
                Index = this.Index,
                DimensionGroupKey = this.DimensionGroupKey,
                Data = this.Data
            };
            PivotGridDataModel pivotGridModel = this.GetPivotGridModel();
            entity.PivotGridData = pivotGridModel;
            return entity;
        }

        public string GetDataXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
            XmlElement newChild = xml.CreateElement("Root");
            xml.AppendChild(newChild);
            newChild.AppendChild(this.CreateSheetPropertyPart(xml));
            newChild.AppendChild(this.CreateShcemaPart(xml));
            newChild.AppendChild(this.CreateDataPart(xml));
            return xml.OuterXml;
        }

        private List<DimensionEntity> GetDimensionEntityList(XmlTextReader xr)
        {
            List<DimensionEntity> list = new List<DimensionEntity>();
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            while (xr.Read())
            {
                if (xr.NodeType != XmlNodeType.Element)
                {
                    goto Label_0087;
                }
                string name = xr.Name;
                if (name != null)
                {
                    if (!(name == "ID"))
                    {
                        if (name == "DimValue")
                        {
                            goto Label_006F;
                        }
                        if (name == "IsBudgetOrg")
                        {
                            goto Label_007B;
                        }
                    }
                    else
                    {
                        str = xr.ReadInnerXml();
                    }
                }
                continue;
            Label_006F:
                str2 = xr.ReadInnerXml();
                continue;
            Label_007B:
                str3 = xr.ReadInnerXml();
                continue;
            Label_0087:
                if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "DimensionEntity"))
                {
                    DimensionEntity item = new DimensionEntity {
                        Id = Convert.ToInt64(str),
                        DimValue = str2,
                        IsBudgetOrg = (!string.IsNullOrWhiteSpace(str3) && !string.IsNullOrEmpty(str3)) && Convert.ToBoolean(str3)
                    };
                    list.Add(item);
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "DimensionEntityList"))
                {
                    return list;
                }
            }
            return list;
        }

        public PivotGridDataModel GetPivotGridModel()
        {
            this._pivotGridModel.ModelName = this.SheetName;
            return this._pivotGridModel;
        }

        public string GetSchemaXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
            xml.AppendChild(this.CreateShcemaPart(xml));
            return xml.OuterXml;
        }

        private void ParseData(XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "Row"))
                    {
                        PivotGridRow row = this._pivotGridModel.NewRow();
                        this.ParseDataColumn(xr, row);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "Data"))
                {
                    return;
                }
            }
        }

        private void ParseDataCell(string columnName, XmlTextReader xr, PivotGridRow row)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string str7 = string.Empty;
            string str8 = string.Empty;
            List<DimensionEntity> dimensionEntityList = new List<DimensionEntity>();
            PivotGridCellValue value2 = new PivotGridCellValue();
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.Name)
                    {
                        case "ID":
                        {
                            str = xr.ReadInnerXml();
                            continue;
                        }
                        case "Number":
                        {
                            str2 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Formula":
                        {
                            str3 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Value":
                        {
                            str4 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Year":
                        {
                            str5 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Period":
                        {
                            str7 = xr.ReadInnerXml();
                            continue;
                        }
                        case "PeriodType":
                        {
                            str6 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ValueDataType":
                        {
                            str8 = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionEntityList":
                        {
                            dimensionEntityList = this.GetDimensionEntityList(xr);
                            continue;
                        }
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "Column"))
                {
                    value2.Id = str.Trim();
                    value2.Number = str2.Trim();
                    value2.Formula = str3.Trim();
                    value2.Value = str4;
                    if (!string.IsNullOrWhiteSpace(str5))
                    {
                        value2.Year = Convert.ToInt32(str5);
                    }
                    if (!string.IsNullOrWhiteSpace(str7))
                    {
                        value2.Period = Convert.ToInt32(str7);
                    }
                    if (!string.IsNullOrWhiteSpace(str6))
                    {
                        value2.PeriodType = (CycleType) Convert.ToInt32(str6);
                    }
                    if (!string.IsNullOrWhiteSpace(str8))
                    {
                        value2.ValueDataType = Convert.ToInt32(str8);
                    }
                    value2.LstDimensionEntity = dimensionEntityList;
                    row.SetCellValue(columnName, value2);
                    return;
                }
            }
        }

        private void ParseDataColumn(XmlTextReader xr, PivotGridRow row)
        {
            while (xr.Read())
            {
                string attribute = xr.GetAttribute("Name");
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str2;
                    if (((str2 = xr.Name) != null) && (str2 == "Column"))
                    {
                        this.ParseDataCell(attribute, xr, row);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "Row"))
                {
                    this._pivotGridModel.Rows.Add(row);
                    return;
                }
            }
        }

        private void ParseDataModel(XmlTextReader xr)
        {
            while (xr.Read())
            {
                string str;
                if ((xr.NodeType == XmlNodeType.Element) && ((str = xr.Name) != null))
                {
                    if (!(str == "Property"))
                    {
                        if (str == "Schema")
                        {
                            goto Label_0047;
                        }
                        if (str == "Data")
                        {
                            goto Label_0050;
                        }
                    }
                    else
                    {
                        this.ParseProperty(xr);
                    }
                }
                continue;
            Label_0047:
                this.ParseSchema(xr);
                continue;
            Label_0050:
                this.ParseData(xr);
            }
        }

        private void ParseProperty(XmlTextReader xr)
        {
            while (xr.Read())
            {
                string str2;
                string str3;
                if (xr.NodeType != XmlNodeType.Element)
                {
                    goto Label_0118;
                }
                string name = xr.Name;
                if (name != null)
                {
                    if (!(name == "CalendarId"))
                    {
                        if (name == "MinReportCycle")
                        {
                            goto Label_0087;
                        }
                        if (name == "ReportCycles")
                        {
                            goto Label_00AF;
                        }
                        if (name == "DimensionGroupKey")
                        {
                            goto Label_010A;
                        }
                    }
                    else
                    {
                        string str = xr.ReadInnerXml();
                        if (!string.IsNullOrWhiteSpace(str.Trim()))
                        {
                            this.CalendarId = Convert.ToInt32(str);
                        }
                    }
                }
                continue;
            Label_0087:
                str2 = xr.ReadInnerXml();
                if (!string.IsNullOrWhiteSpace(str2.Trim()))
                {
                    this.MinReportCycle = (CycleType) Convert.ToInt32(str2);
                }
                continue;
            Label_00AF:
                str3 = xr.ReadInnerXml();
                if (!string.IsNullOrWhiteSpace(str3.Trim()))
                {
                    string[] strArray = str3.Split(new char[] { ',' });
                    this.ReportCycles = (from c in strArray select (CycleType) Convert.ToInt32(c)).ToArray<CycleType>();
                }
                continue;
            Label_010A:
                this.DimensionGroupKey = xr.ReadInnerXml();
                continue;
            Label_0118:
                if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "Property"))
                {
                    return;
                }
            }
        }

        private void ParseSchema(XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType != XmlNodeType.Element)
                {
                    goto Label_008D;
                }
                string name = xr.Name;
                if (name != null)
                {
                    if (!(name == "HeaderArea"))
                    {
                        if (name == "ColumnArea")
                        {
                            goto Label_0060;
                        }
                        if (name == "RowArea")
                        {
                            goto Label_006F;
                        }
                        if (name == "DataArea")
                        {
                            goto Label_007E;
                        }
                    }
                    else
                    {
                        this.ParseShcemaColumnsHA(this.HeaderDimensions, xr);
                    }
                }
                continue;
            Label_0060:
                this.ParseShcemaColumnsCA(this.ColDimensions, xr);
                continue;
            Label_006F:
                this.ParseShcemaColumnsRA(this.RowDimensions, xr);
                continue;
            Label_007E:
                this.ParseShcemaDataColumns(this.DataDimensions, xr);
                continue;
            Label_008D:
                if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "Schema"))
                {
                    return;
                }
            }
        }

        private void ParseSchemaColumnProperyCA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            long dimensionId = 0L;
            string dimensionNumber = string.Empty;
            string dimensionName = string.Empty;
            string baseDataObjectId = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string str7 = string.Empty;
            DimensionType baseData = DimensionType.BaseData;
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.Name)
                    {
                        case "DimensionID":
                        {
                            dimensionId = Convert.ToInt64(xr.ReadInnerXml());
                            continue;
                        }
                        case "DimensionNumber":
                        {
                            dimensionNumber = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionName":
                        {
                            dimensionName = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionType":
                        {
                            baseData = (DimensionType) Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "BaseDataObjectId":
                        {
                            baseDataObjectId = xr.ReadInnerXml();
                            continue;
                        }
                        case "Caption":
                        {
                            str4 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ColumnName":
                        {
                            str5 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Area":
                        {
                            Convert.ToInt32("1");
                            continue;
                        }
                        case "ColumnType":
                        {
                            Convert.ToInt32("2");
                            continue;
                        }
                        case "DataType":
                        {
                            Type.GetType(xr.ReadInnerXml());
                            continue;
                        }
                        case "ItemDataType":
                        {
                            str6 = xr.ReadInnerXml();
                            continue;
                        }
                        case "BusinessType":
                        {
                            str7 = xr.ReadInnerXml();
                            continue;
                        }
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "ColumnAreaColumn"))
                {
                    DimensionColumn column = new DimensionColumn(dimensionId, dimensionNumber, dimensionName, baseData, baseDataObjectId) {
                        Area = PivotGridArea.ColumnArea,
                        Caption = str4,
                        ColumnName = str5,
                        ColumnType = PivotGridColumnType.Dimension,
                        ReadOnly = true,
                        ItemDataType = (string.IsNullOrEmpty(str6) || string.IsNullOrWhiteSpace(str6)) ? 0 : Convert.ToInt32(str6),
                        BusinessType = (string.IsNullOrEmpty(str7) || string.IsNullOrWhiteSpace(str7)) ? 0 : Convert.ToInt32(str7),
                        DataType = typeof(string)
                    };
                    columns.Add(column);
                    return;
                }
            }
        }

        private void ParseSchemaColumnProperyHA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            long dimensionId = 0L;
            string dimensionNumber = string.Empty;
            string dimensionName = string.Empty;
            string baseDataObjectId = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            DimensionType baseData = DimensionType.BaseData;
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.Name)
                    {
                        case "DimensionID":
                        {
                            dimensionId = Convert.ToInt64(xr.ReadInnerXml());
                            continue;
                        }
                        case "DimensionNumber":
                        {
                            dimensionNumber = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionName":
                        {
                            dimensionName = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionType":
                        {
                            baseData = (DimensionType) Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "BaseDataObjectId":
                        {
                            baseDataObjectId = xr.ReadInnerXml();
                            continue;
                        }
                        case "Caption":
                        {
                            str4 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ColumnName":
                        {
                            str5 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Area":
                        {
                            Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "ColumnType":
                        {
                            Convert.ToInt32("2");
                            continue;
                        }
                        case "DataType":
                        {
                            Type.GetType(xr.ReadInnerXml());
                            continue;
                        }
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "HeaderAreaColumn"))
                {
                    DimensionColumn column = new DimensionColumn(dimensionId, dimensionNumber, dimensionName, baseData, baseDataObjectId) {
                        Area = PivotGridArea.FilterArea,
                        Caption = str4,
                        ColumnName = str5,
                        ColumnType = PivotGridColumnType.Dimension,
                        ReadOnly = false,
                        DataType = typeof(string)
                    };
                    columns.Add(column);
                    return;
                }
            }
        }

        private void ParseSchemaColumnProperyRA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            long dimensionId = 0L;
            string dimensionNumber = string.Empty;
            string dimensionName = string.Empty;
            string baseDataObjectId = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            DimensionType baseData = DimensionType.BaseData;
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.Name)
                    {
                        case "DimensionID":
                        {
                            dimensionId = Convert.ToInt64(xr.ReadInnerXml());
                            continue;
                        }
                        case "DimensionNumber":
                        {
                            dimensionNumber = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionName":
                        {
                            dimensionName = xr.ReadInnerXml();
                            continue;
                        }
                        case "DimensionType":
                        {
                            baseData = (DimensionType) Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "BaseDataObjectId":
                        {
                            baseDataObjectId = xr.ReadInnerXml();
                            continue;
                        }
                        case "Caption":
                        {
                            str4 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ColumnName":
                        {
                            str5 = xr.ReadInnerXml();
                            continue;
                        }
                        case "Area":
                        {
                            Convert.ToInt32("1");
                            continue;
                        }
                        case "ColumnType":
                        {
                            Convert.ToInt32("2");
                            continue;
                        }
                        case "DataType":
                        {
                            Type.GetType(xr.ReadInnerXml());
                            continue;
                        }
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "RowAreaColumn"))
                {
                    DimensionColumn column = new DimensionColumn(dimensionId, dimensionNumber, dimensionName, baseData, baseDataObjectId) {
                        Area = PivotGridArea.RowArea,
                        Caption = str4,
                        ColumnName = str5,
                        ColumnType = PivotGridColumnType.Dimension,
                        ReadOnly = false,
                        DataType = typeof(string)
                    };
                    columns.Add(column);
                    return;
                }
            }
        }

        private void ParseSchemaDataPropery(ItemDataColumnCollection columns, XmlTextReader xr)
        {
            bool isBugetData = true;
            long itemDataId = 0L;
            string itemDataMumber = string.Empty;
            string itemDataName = string.Empty;
            int num2 = 0;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    switch (xr.Name)
                    {
                        case "IsBugetData":
                        {
                            isBugetData = Convert.ToBoolean(xr.ReadInnerXml());
                            continue;
                        }
                        case "ItemDataId":
                        {
                            itemDataId = Convert.ToInt64(xr.ReadInnerXml());
                            continue;
                        }
                        case "ItemDataNumber":
                        {
                            itemDataMumber = xr.ReadInnerXml();
                            continue;
                        }
                        case "ItemDataName":
                        {
                            itemDataName = xr.ReadInnerXml();
                            continue;
                        }
                        case "ItemDataType":
                        {
                            num2 = Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "Caption":
                        {
                            str3 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ColumnName":
                        {
                            str4 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ProportionDataColumn":
                        {
                            xr.ReadInnerXml();
                            continue;
                        }
                        case "Formula":
                        {
                            xr.ReadInnerXml();
                            continue;
                        }
                        case "ColumnType":
                        {
                            Convert.ToInt32(xr.ReadInnerXml());
                            continue;
                        }
                        case "DataType":
                        {
                            Type.GetType(xr.ReadInnerXml());
                            continue;
                        }
                        case "BusinessType":
                        {
                            str5 = xr.ReadInnerXml();
                            continue;
                        }
                        case "ReadOnly":
                        {
                            Convert.ToBoolean(xr.ReadInnerXml());
                            continue;
                        }
                        case "FormatString":
                        {
                            str6 = xr.ReadInnerXml();
                            continue;
                        }
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "DataAreaColumn"))
                {
                    ItemDataColumn column = new ItemDataColumn(itemDataId, itemDataMumber, itemDataName, Kingdee.K3.FIN.BM.Common.BusinessEntity.ItemDataProperty.Amount, isBugetData) {
                        Area = PivotGridArea.DataArea,
                        Caption = str3,
                        ColumnName = str4
                    };
                    new Random();
                    column.ColumnType = PivotGridColumnType.ValueData;
                    column.ReadOnly = false;
                    column.ItemDataType = num2;
                    column.BusinessType = string.IsNullOrEmpty(str5) ? 0 : Convert.ToInt32(str5);
                    column.DataType = typeof(decimal);
                    column.FormatString = str6;
                    columns.Add(column);
                    return;
                }
            }
        }

        private void ParseShcemaColumnCA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "ColumnAreaColumn"))
                    {
                        this.ParseSchemaColumnProperyCA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "ColumnAreaColumns"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaColumnHA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "HeaderAreaColumn"))
                    {
                        this.ParseSchemaColumnProperyHA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "HeaderAreaColumns"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaColumnRA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "RowAreaColumn"))
                    {
                        this.ParseSchemaColumnProperyRA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "RowAreaColumns"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaColumnsCA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "ColumnAreaColumns"))
                    {
                        this.ParseShcemaColumnCA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "ColumnArea"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaColumnsHA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "HeaderAreaColumns"))
                    {
                        this.ParseShcemaColumnHA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "HeaderArea"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaColumnsRA(DimensionColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "RowAreaColumns"))
                    {
                        this.ParseShcemaColumnRA(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "RowArea"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaDataColumn(ItemDataColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "DataAreaColumn"))
                    {
                        this.ParseSchemaDataPropery(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "DataAreaColumns"))
                {
                    return;
                }
            }
        }

        private void ParseShcemaDataColumns(ItemDataColumnCollection columns, XmlTextReader xr)
        {
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    string str;
                    if (((str = xr.Name) != null) && (str == "DataAreaColumns"))
                    {
                        this.ParseShcemaDataColumn(columns, xr);
                    }
                }
                else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "DataArea"))
                {
                    return;
                }
            }
        }

        public void SetData(byte[] blob)
        {
            KDZipLib lib = new KDZipLib();
            MemoryStream stream = new MemoryStream(lib.UnZip(blob));
            string xml = new StreamReader(stream).ReadToEnd();
            string filename = AppDomain.CurrentDomain.BaseDirectory + "Parse.xml";
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            document.Save(filename);
            XmlTextReader xr = new XmlTextReader(filename) {
                WhitespaceHandling = WhitespaceHandling.All
            };
            this.ParseDataModel(xr);
            xr.Close();
            File.Delete(filename);
        }

        [DefaultValue(0)]
        public int CalendarId { get; set; }

        public DimensionColumnCollection ColDimensions { get; private set; }

        public byte[] Data { get; set; }

        public ItemDataColumnCollection DataDimensions { get; private set; }

        public string DimensionGroupKey { get; private set; }

        public List<string> GroupingColumns
        {
            get
            {
                return this._pivotGridModel.GroupingColumns;
            }
        }

        public DimensionColumnCollection HeaderDimensions { get; private set; }

        public int Index { get; set; }

        public CycleType MinReportCycle { get; set; }

        public CycleType[] ReportCycles { get; set; }

        public ReportDataModel ReportModel { get; internal set; }

        public DimensionColumnCollection RowDimensions { get; private set; }

        public Guid SampleSheetId { get; set; }

        public long SchemeId { get; set; }

        public Guid SheetId
        {
            get
            {
                return this._pivotGridModel.ModelId;
            }
            set
            {
                this._pivotGridModel.ModelId = value;
            }
        }

        public string SheetName { get; set; }
    }
}

