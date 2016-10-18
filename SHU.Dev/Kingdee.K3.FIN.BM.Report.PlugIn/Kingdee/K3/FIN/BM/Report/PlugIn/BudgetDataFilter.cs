namespace Kingdee.K3.FIN.BM.Report.PlugIn
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.CommonFilter.PlugIn;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.List;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Metadata.EntityElement;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.ServiceHelper;
    using Kingdee.BOS.Util;
    using Kingdee.K3.FIN.BM.Common.Core;
    using Kingdee.K3.FIN.BM.ServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    [Description("预算数据查询过滤插件")]
    public class BudgetDataFilter : AbstractCommonFilterPlugIn
    {
        public string _curFilterSchemeId;
        public string _defaultSchemeId;
        private const string BOS_ASSISTANTDATA_SELECT = "BOS_ASSISTANTDATA_SELECT";
        private const string CATEGORY_ASSISTANTDATA = "10";
        private Dictionary<int, List<int>> dicItemDataTypeID = new Dictionary<int, List<int>>();
        private DynamicObject dyScheme;
        private DynamicObject dyWizardScheme;
        public string FilterSchemeFormId;
        private const string FWIZARDSCHEMEID = "FWIZARDSCHEMEID";
        private const string KEY_FBillFormId = "FBillFormId";
        private readonly string KEY_FCtrlDimensionEntry = "FCtrlDimensionEntry";
        private const string KEY_FDimension = "FDimension";
        private const string KEY_FDimensionID = "FDimension_ID";
        private const string KEY_FFilterName = "FFILTERNAME";
        private readonly string WARNNING_BUDGETWIZARDSCHEMEISNULL = ResManager.LoadKDString("模板样式方案无效，可能已被删除！", "0032058000021922", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_EdPeriodLesStrPeriod = ResManager.LoadKDString("预算结束期间不能小于预算开始期间！", "0032058000021393", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_EdYearLesStrYear = ResManager.LoadKDString("预算结束年度不能小于预算开始年度！", "0032058000021392", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_SCHEMEISNULL = ResManager.LoadKDString("预算方案无效，可能已被删除！", "0032058000021921", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_SelectCtrlDimension = ResManager.LoadKDString("请先选择控制维度！", "0032058000021391", SubSystemType.FIN, new object[0]);

        public override void AfterBindData(EventArgs e)
        {
            this.BindDataInfo(false);
            this._defaultSchemeId = ReportCommonFunction.GetDefaultSchemeId(base.Context, this.Model, this.FilterSchemeFormId, this._defaultSchemeId);
            base.AfterBindData(e);
        }

        public override void AfterEntryBarItemClick(AfterBarItemClickEventArgs e)
        {
            string str;
            if (((str = e.BarItemKey.ToUpperInvariant()) != null) && (str == "TBCLEARDIMENSION"))
            {
                this.ClearDimensionData();
            }
            base.AfterEntryBarItemClick(e);
        }

        public override void BeforeF7Select(BeforeF7SelectEventArgs e)
        {
            string filter = e.ListFilterParameter.Filter;
            string billFormId = this.GetBillFormId();
            string str3 = e.FieldKey.ToUpperInvariant();
            if (str3 != null)
            {
                if (!(str3 == "FSCHEMEID"))
                {
                    if (str3 == "FWIZARDSCHEMEID")
                    {
                        e.ListFilterParameter.Filter = this.GetWizardSchemeFilter(filter);
                        this.ShowBugdetFormulaForm(e);
                        e.Cancel = true;
                    }
                    else if (str3 == "FORGIDS")
                    {
                        e.ListFilterParameter.Filter = this.GetOrgFilter(filter);
                    }
                    else if (str3 == "FFILTERNAME")
                    {
                        DynamicObject obj2 = this.View.Model.GetValue("FDimension", e.Row) as DynamicObject;
                        object obj3 = obj2["BaseDataType_Id"];
                        if (obj3 == null)
                        {
                            this.View.ShowNotificationMessage(this.WARNNING_SelectCtrlDimension, "", MessageBoxType.Notice);
                            return;
                        }
                        billFormId = obj3.ToString();
                        this.SelectDimensionValue(billFormId, e, DimentionScopeType.Dimension);
                    }
                    else if (str3 == "FITEMDATATYPEIDS")
                    {
                        e.ListFilterParameter.Filter = this.GetItemDataFilter(filter);
                    }
                }
                else
                {
                    e.ListFilterParameter.Filter = this.GetSchemeFilter(filter);
                }
            }
            base.BeforeF7Select(e);
        }

        public void BindBudgetPeriod(DynamicObject dyScheme, DynamicObject dyCanlendar, bool isNeedToSetValue)
        {
            if (dyCanlendar != null)
            {
                List<EnumItem> enumItemsByCalendar = BMCommonUtil.GetEnumItemsByCalendar(base.Context, dyCanlendar);
                int num = Convert.ToInt32(enumItemsByCalendar.LastOrDefault<EnumItem>().Value);
                this.View.GetControl<ComboFieldEditor>("FCtrlPeriod").SetComboItems(enumItemsByCalendar);
                if (isNeedToSetValue)
                {
                    this.View.Model.SetValue("FCtrlPeriod", num);
                }
                this.BindYearList(dyScheme, isNeedToSetValue);
                this.BindPeriodList(dyScheme, dyCanlendar, isNeedToSetValue, num);
            }
        }

        public void BindBusinessTypes()
        {
            DynamicObjectCollection objects = this.View.Model.GetValue("FITEMDATATYPEIDS") as DynamicObjectCollection;
            if ((objects == null) || (objects.Count == 0))
            {
                this.Model.SetValue("FBusinessTypeIds", null);
            }
            else
            {
                List<int> source = new List<int>();
                foreach (DynamicObject obj2 in objects)
                {
                    int key = Convert.ToInt32(obj2["ItemDataTypeIds_Id"]);
                    if (this.dicItemDataTypeID.ContainsKey(key))
                    {
                        source.AddRange(this.dicItemDataTypeID[key]);
                    }
                }
                this.Model.SetValue("FBusinessTypeIds", source.Distinct<int>().ToList<int>());
            }
        }

        public void BindBusinessTypes(DynamicObject dyRule, bool isNeedToSetValue)
        {
            if (isNeedToSetValue)
            {
                DynamicObjectCollection objects = dyRule["BusinessTypes"] as DynamicObjectCollection;
                List<int> list = new List<int>();
                foreach (DynamicObject obj2 in objects)
                {
                    int item = Convert.ToInt32(obj2["BusinessTypes_Id"]);
                    list.Add(item);
                }
                this.View.Model.SetValue("FBusinessTypeIds", list);
                this.View.UpdateView("FBusinessTypeIds");
            }
        }

        public void BindDataInfo(bool isNeedToSetValue = true)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            int num2 = this.GetBaseDataIdByField("FWizardSchemeId");
            if ((baseDataIdByField != 0) && (num2 != 0))
            {
                this.dyWizardScheme = BMCommonServiceHelper.LoadFormData(base.Context, "BM_RPTSCHEME", num2.ToString());
                if (!this.CheckDyObjIsNull(this.dyWizardScheme, this.WARNNING_BUDGETWIZARDSCHEMEISNULL))
                {
                    DynamicObject dyObj = BMCommonServiceHelper.LoadFormData(base.Context, "BM_SCHEME", baseDataIdByField.ToString());
                    if (!this.CheckDyObjIsNull(dyObj, this.WARNNING_SCHEMEISNULL))
                    {
                        BMCommonServiceHelper.LoadFormData(base.Context, "BM_SCHEME", Convert.ToInt32(this.dyWizardScheme["Id"]).ToString());
                        DynamicObject dyCanlendar = BMCommonServiceHelper.LoadFormData(base.Context, "BM_BUDGETCALENDAR", dyObj["CalendarId_Id"].ToString());
                        this.BindBudgetPeriod(dyObj, dyCanlendar, isNeedToSetValue);
                        this.BindItemDataTypes(this.dyWizardScheme, isNeedToSetValue);
                        this.BindBusinessTypes(this.dyWizardScheme, isNeedToSetValue);
                        this.BindDimessionEntry(this.dyWizardScheme, isNeedToSetValue);
                    }
                }
            }
        }

        public void BindDimessionEntry(DynamicObject dyScheme, bool isNeedToSetValue)
        {
            if (isNeedToSetValue)
            {
                this.View.Model.DeleteEntryData("FCtrlDimensionEntry");
                DynamicObjectCollection objects = dyScheme["DimensionEntity"] as DynamicObjectCollection;
                int row = 0;
                foreach (DynamicObject obj2 in objects)
                {
                    DynamicObject obj3 = obj2["DimensionId"] as DynamicObject;
                    if (((obj3 != null) && !(Convert.ToString(obj3["BaseDataType_Id"]) == "BM_BUDGETCALENDAR")) && !(Convert.ToString(obj3["BaseDataType_Id"]) == "BM_DEPTORG"))
                    {
                        this.Model.InsertEntryRow(this.KEY_FCtrlDimensionEntry, row);
                        this.View.Model.SetValue("FDimension", obj2["DimensionID"], row);
                        this.View.Model.SetValue("FDimension_ID", obj2["DimensionID_ID"], row);
                        row++;
                    }
                }
                this.View.UpdateView(this.KEY_FCtrlDimensionEntry);
            }
        }

        public void BindItemDataTypes(DynamicObject dyRule, bool isNeedToSetValue)
        {
            if (isNeedToSetValue)
            {
                DynamicObjectCollection objects = dyRule["ItemDataTypeEntity"] as DynamicObjectCollection;
                List<int> list = new List<int>();
                foreach (DynamicObject obj2 in objects)
                {
                    int item = Convert.ToInt32(obj2["ItemDataType_Id"]);
                    list.Add(item);
                }
                this.View.Model.SetValue("FItemDataTypeIds", list);
                this.View.UpdateView("FItemDataTypeIds");
            }
        }

        public void BindPeriodList(DynamicObject dyScheme, DynamicObject dyCanlendar, bool isNeedToSetValue, int periodType = 0)
        {
            Func<DynamicObject, bool> predicate = null;
            int startYear = Convert.ToInt32(this.View.Model.GetValue("FSTARTYEAR"));
            int endYear = Convert.ToInt32(this.View.Model.GetValue("FENDYEAR"));
            List<EnumItem> items = new List<EnumItem>();
            List<EnumItem> list2 = new List<EnumItem>();
            DynamicObjectCollection source = dyCanlendar["BudgetPeriodEntity"] as DynamicObjectCollection;
            int num = 0;
            int num2 = 0;
            foreach (DynamicObject obj2 in from p in source
                where (Convert.ToInt32(p["PeriodType"]) == periodType) && (Convert.ToInt32(p["PeriodYear"]) == startYear)
                select p)
            {
                EnumItem item = new EnumItem {
                    EnumId = Convert.ToString(obj2["Period"]),
                    Seq = num,
                    Value = Convert.ToString(obj2["Period"]),
                    Caption = new LocaleValue(Convert.ToString(obj2["Period"]))
                };
                items.Add(item);
                if (startYear == endYear)
                {
                    list2.Add(item);
                }
            }
            if (startYear != endYear)
            {
                if (predicate == null)
                {
                    predicate = p => (Convert.ToInt32(p["PeriodType"]) == periodType) && (Convert.ToInt32(p["PeriodYear"]) == endYear);
                }
                foreach (DynamicObject obj3 in source.Where<DynamicObject>(predicate))
                {
                    EnumItem item2 = new EnumItem {
                        EnumId = Convert.ToString(obj3["Period"]),
                        Seq = num2,
                        Value = Convert.ToString(obj3["Period"]),
                        Caption = new LocaleValue(Convert.ToString(obj3["Period"]))
                    };
                    list2.Add(item2);
                }
            }
            this.View.GetControl<ComboFieldEditor>("FSTARTPERIOD").SetComboItems(items);
            this.View.GetControl<ComboFieldEditor>("FENDPERIOD").SetComboItems(list2);
            if ((items.Count > 0) && isNeedToSetValue)
            {
                this.View.Model.SetValue("FSTARTPERIOD", items[0].Value);
            }
            if ((list2.Count > 0) && isNeedToSetValue)
            {
                this.View.Model.SetValue("FENDPERIOD", list2[0].Value);
            }
        }

        public void BindYearList(DynamicObject dyScheme, bool isNeedToSetValue)
        {
            List<EnumItem> items = new List<EnumItem>();
            int num = Convert.ToInt32(dyScheme["STARTYEAR"]);
            int num2 = Convert.ToInt32(dyScheme["ENDYEAR"]);
            EnumItem item = null;
            int num3 = 0;
            for (int i = num; i <= num2; i++)
            {
                item = new EnumItem(new DynamicObject(EnumItem.EnumItemType)) {
                    EnumId = i.ToString(),
                    Seq = num3,
                    Value = i.ToString(),
                    Caption = new LocaleValue(i.ToString())
                };
                items.Add(item);
                num3++;
            }
            this.View.GetControl<ComboFieldEditor>("FSTARTYEAR").SetComboItems(items);
            this.View.GetControl<ComboFieldEditor>("FENDYEAR").SetComboItems(items);
            if (isNeedToSetValue)
            {
                this.View.Model.SetValue("FSTARTYEAR", num);
                this.View.Model.SetValue("FENDYEAR", num);
            }
        }

        public override void ButtonClick(ButtonClickEventArgs e)
        {
            string str;
            if (((str = e.Key.ToUpperInvariant()) != null) && (str == "FBTNOK"))
            {
                e.Cancel = this.CheckParam();
                ReportCommonFunction.CheckDefaultScheme(this.View, this.Model, e, this._curFilterSchemeId, this._defaultSchemeId);
            }
            base.ButtonClick(e);
        }

        private bool CheckDyObjIsNull(DynamicObject dyObj, string errorMsg)
        {
            if (dyObj == null)
            {
                this.View.ShowWarnningMessage(errorMsg, "", MessageBoxOptions.OK, null, MessageBoxType.Advise);
                this.View.GetControl("FBtnOK").Enabled = false;
                return true;
            }
            this.View.GetControl("FBtnOK").Enabled = true;
            return false;
        }

        public bool CheckParam()
        {
            int num = Convert.ToInt32(this.View.Model.GetValue("FSTARTYEAR"));
            int num2 = Convert.ToInt32(this.View.Model.GetValue("FENDYEAR"));
            int num3 = Convert.ToInt32(this.View.Model.GetValue("FSTARTPERIOD"));
            int num4 = Convert.ToInt32(this.View.Model.GetValue("FENDPERIOD"));
            if (num > num2)
            {
                this.View.ShowWarnningMessage(this.WARNNING_EdYearLesStrYear, "", MessageBoxOptions.OK, null, MessageBoxType.Advise);
                return true;
            }
            if (num3 > num4)
            {
                this.View.ShowWarnningMessage(this.WARNNING_EdPeriodLesStrPeriod, "", MessageBoxOptions.OK, null, MessageBoxType.Advise);
                return true;
            }
            return false;
        }

        public void ClearDimensionData()
        {
            int entryCurrentRowIndex = this.View.Model.GetEntryCurrentRowIndex(this.KEY_FCtrlDimensionEntry);
            if (entryCurrentRowIndex >= 0)
            {
                this.Model.SetValue("FFilterName", "", entryCurrentRowIndex);
                this.Model.SetValue("FFilterKey", "", entryCurrentRowIndex);
                this.Model.SetValue("FFilterKeyDesc", "", entryCurrentRowIndex);
            }
        }

        public static EnumItem CreateEnumItem(IDynamicFormView view, object key, string displayValue)
        {
            return CreateEnumItem(view.Context, key, key, displayValue);
        }

        public static EnumItem CreateEnumItem(Context ctx, object key, object val, string displayValue)
        {
            return new EnumItem(new DynamicObject(EnumItem.EnumItemType)) { EnumId = key.ToString(), Value = val.ToString(), Caption = new LocaleValue(displayValue, ctx.UserLocale.LCID) };
        }

        public override void DataChanged(DataChangedEventArgs e)
        {
            string str = e.Field.Key.ToUpperInvariant();
            if (str != null)
            {
                if (!(str == "FWIZARDSCHEMEID"))
                {
                    if (!(str == "FSCHEMEID"))
                    {
                        if (!(str == "FSHOWADJUSTSUMDATA"))
                        {
                            if (!(str == "FSHOWADJUSTDETAILDATA"))
                            {
                                if (str == "FITEMDATATYPEIDS")
                                {
                                    this.BindBusinessTypes();
                                }
                                return;
                            }
                            this.View.Model.SetValue("FSHOWADJUSTSUMDATA", e.OldValue);
                            this.View.UpdateView("FSHOWADJUSTSUMDATA");
                            return;
                        }
                        this.View.Model.SetValue("FShowAdjustDetaildata", e.OldValue);
                        this.View.UpdateView("FShowAdjustDetaildata");
                        return;
                    }
                }
                else
                {
                    this.View.Model.DeleteEntryData(this.KEY_FCtrlDimensionEntry);
                    this.View.Model.SetValue("FITEMDATATYPEIDS", "");
                    this.View.UpdateView("FITEMDATATYPEIDS");
                    this.BindDataInfo(true);
                    return;
                }
                this.BindDataInfo(true);
            }
        }

        private void FillSelectedDatas(Kingdee.BOS.Core.DynamicForm.FormResult fre, BeforeF7SelectEventArgs e, DimentionScopeType scopetype)
        {
            if ((fre != null) && (fre.ReturnData != null))
            {
                ListSelectedRowCollection returnData = (ListSelectedRowCollection) fre.ReturnData;
                if ((returnData != null) && (returnData.Count != 0))
                {
                    StringBuilder builder = new StringBuilder();
                    StringBuilder builder2 = new StringBuilder();
                    bool flag = false;
                    foreach (ListSelectedRow row in returnData)
                    {
                        if (flag)
                        {
                            builder.Append(",");
                            builder2.Append(",");
                        }
                        flag = true;
                        builder.Append(row.PrimaryKeyValue);
                        builder2.Append(row.Name);
                    }
                    switch (scopetype)
                    {
                        case DimentionScopeType.Dimension:
                            this.View.Model.SetValue("FFilterKey", builder.ToString(), e.Row);
                            this.View.Model.SetValue("FFilterName", builder2.ToString(), e.Row);
                            this.View.Model.SetValue("FFilterKeyDesc", builder2.ToString(), e.Row);
                            return;

                        case DimentionScopeType.BillDimension:
                            this.View.Model.SetValue("FBillFilterKey", builder.ToString(), e.Row);
                            this.View.Model.SetValue("FBillFilterName", builder2.ToString(), e.Row);
                            this.View.Model.SetValue("FBillFilterKeyDesc", builder2.ToString(), e.Row);
                            return;
                    }
                }
            }
        }

        public int GetBaseDataIdByField(string field)
        {
            int num = 0;
            DynamicObject obj2 = this.View.Model.GetValue(field) as DynamicObject;
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2["ID"]);
            }
            return num;
        }

        private string GetBaseDataType(DynamicObject dy, string dimKey)
        {
            string str = "";
            DynamicObject obj2 = dy[dimKey] as DynamicObject;
            if (obj2 != null)
            {
                str = obj2["Category"].ToString();
            }
            return str;
        }

        private string GetBaseDataType(string key, int row, string dimKey)
        {
            DynamicObject entryData = this.GetEntryData(key, row);
            return this.GetBaseDataType(entryData, dimKey);
        }

        private string GetBillFormId()
        {
            string str = string.Empty;
            int entryCurrentRowIndex = this.Model.GetEntryCurrentRowIndex(this.KEY_FCtrlDimensionEntry);
            DynamicObject obj2 = (DynamicObject) this.Model.GetValue("FBillFormId", entryCurrentRowIndex);
            if (obj2 != null)
            {
                str = obj2["Id"].ToString();
            }
            return str;
        }

        public static LocaleValue GetCurrencyLocalName(Context ctx, long currencyId)
        {
            return new LocaleValue(GetCurrencyName(ctx, currencyId));
        }

        public static string GetCurrencyName(Context ctx, long currencyId)
        {
            if (currencyId == 0L)
            {
                return ResManager.LoadKDString("综合本位币", "0032057000017645", SubSystemType.FIN, new object[0]);
            }
            DynamicObjectCollection objects = BMCommonServiceHelper.QueryData(ctx, "BD_Currency", "FCurrencyId,FName", string.Format("FDocumentStatus='C' and FForbidStatus='A' And FCURRENCYID={0} ", currencyId));
            if (objects.Count <= 0)
            {
                return string.Empty;
            }
            if (objects[0]["FName"] != null)
            {
                return objects[0]["FName"].ToString();
            }
            return "";
        }

        private DynamicObject GetEntryData(string key, int row)
        {
            DynamicObject obj2 = null;
            Entity entity = this.View.BusinessInfo.GetEntity(key);
            DynamicObjectCollection entityDataObject = this.View.Model.GetEntityDataObject(entity);
            if (row < entityDataObject.Count)
            {
                obj2 = entityDataObject[row];
            }
            return obj2;
        }

        public string GetItemDataFilter(string filter)
        {
            if (this.dyWizardScheme == null)
            {
                return " 1=2 ";
            }
            DynamicObjectCollection objects = this.dyWizardScheme["ItemDataTypeEntity"] as DynamicObjectCollection;
            this.dicItemDataTypeID.Clear();
            List<int> source = new List<int>();
            foreach (DynamicObject obj2 in objects)
            {
                List<int> list2;
                int item = Convert.ToInt32(obj2["ItemDataType_ID"]);
                int num2 = Convert.ToInt32(obj2["BusinessType_ID"]);
                source.Add(item);
                if (this.dicItemDataTypeID.TryGetValue(item, out list2))
                {
                    list2.Add(num2);
                }
                else
                {
                    List<int> list3 = new List<int> {
                        num2
                    };
                    this.dicItemDataTypeID.Add(item, list3);
                }
            }
            if (string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
            {
                filter = string.Format(" FDATATYPEID IN ({0}) ", string.Join<int>(",", source.Distinct<int>()));
                return filter;
            }
            filter = filter + " AND " + string.Format(" FDATATYPEID IN ({0})", string.Join<int>(",", source.Distinct<int>()));
            return filter;
        }

        public string GetOrgFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            if ((this.GetBaseDataIdByField("FWIZARDSCHEMEID") == 0) || (baseDataIdByField == 0))
            {
                filter = " 1=2 ";
                return filter;
            }
            List<int> budgetOrgIdBySchemeId = BMCommonServiceHelper.GetBudgetOrgIdBySchemeId(base.Context, baseDataIdByField);
            if (budgetOrgIdBySchemeId.Count<int>() <= 0)
            {
                return "1=2";
            }
            string format = " FORGID IN ({0}) AND FISDEFAULT='1' ";
            if (string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
            {
                filter = string.Format(format, string.Join<int>(",", budgetOrgIdBySchemeId));
                return filter;
            }
            filter = filter + " AND " + string.Format(format, string.Join<int>(",", budgetOrgIdBySchemeId));
            return filter;
        }

        public string GetSchemeFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FWizardSchemeId");
            if (baseDataIdByField > 0)
            {
                if (string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
                {
                    filter = this.GetSchemeFilter(0, baseDataIdByField);
                    return filter;
                }
                filter = filter + " AND " + this.GetSchemeFilter(0, baseDataIdByField);
            }
            return filter;
        }

        public string GetSchemeFilter(int schemeId, int ruleId)
        {
            string format = " {0} IN (SELECT {1} FROM T_BM_CTRLPLATFORM A \r\n                                            INNER JOIN T_BM_CTRLPLATFORMENTITY B ON A.FID = B.FID AND A.FDOCUMENTSTATUS='C' AND A.FFORBIDSTATUS='A' \r\n                                            INNER JOIN T_BM_CTRLRULE C ON {2}= C.FID \r\n                                            INNER JOIN T_BM_RPTSCHEME RS1 ON C.FPATTERNSCHEME= RS1.FID\r\n                                            INNER JOIN T_BM_RPTSCHEME RS2 ON RS1.FVERSIONGROUPID=RS2.FVERSIONGROUPID AND RS2.FID={3} ) ";
            if (schemeId > 0)
            {
                return string.Format(format, new object[] { "FID", "B.FBUDGETCTRLRULEID", "A.FSCHEMEID", schemeId });
            }
            if (ruleId > 0)
            {
                return string.Format(format, new object[] { "FID", "A.FSCHEMEID", "B.FBUDGETCTRLRULEID", ruleId });
            }
            return "";
        }

        public string GetWizardSchemeFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" FID IN ( SELECT distinct T4.FID FROM T_BM_SCHEMEENTRY T1 ", new object[0]);
            builder.AppendFormat(" LEFT JOIN T_BM_DISTRIBUTE T2 ON T1.FDISTRIBUTEID=T2.FDETAILID AND T2.FSAMPLEID = T1.FSAMPLEID ", new object[0]);
            builder.AppendFormat(" INNER JOIN T_BM_SHEET T3 ON T3.FID = T2.FNEWSAMPLEID ", new object[0]);
            builder.Append(" INNER JOIN T_BM_RPTSCHEME T4 ON T4.FID = T3.FRPTSCHEMEID  WHERE T1.FID={0} )");
            if (baseDataIdByField > 0)
            {
                return string.Format(builder.ToString(), baseDataIdByField);
            }
            return filter;
        }

        private void SelectDimensionValue(string formid, BeforeF7SelectEventArgs e, DimentionScopeType scopetype)
        {
            ListShowParameter parameter2 = new ListShowParameter {
                FormId = formid,
                ParentPageId = this.View.PageId,
                MultiSelect = true,
                SyncCallBackAction = true,
                IsShowQuickFilter = false,
                IsLookUp = true
            };
            ListShowParameter param = parameter2;
            long iD = base.Context.CurrentOrganizationInfo.ID;
            if (this.GetBaseDataType("FCtrlDimensionEntry", e.Row, "Dimension") == "10")
            {
                param.FormId = "BOS_ASSISTANTDATA_SELECT";
                string str2 = string.Format("FID='{0}'", formid);
                if (str2.IsNullOrEmptyOrWhiteSpace())
                {
                    param.ListFilterParameter.Filter = str2;
                }
                else
                {
                    param.ListFilterParameter.Filter = param.ListFilterParameter.Filter.JoinFilterString(str2, "AND");
                }
                param.IsIsolationOrg = true;
                param.UseOrgId = base.Context.CurrentOrganizationInfo.ID;
            }
            else if (OrganizationServiceHelper.GetBaseDataType(base.Context, formid) == 2)
            {
                FormMetadata cachedFormMetaData = FormMetaDataCache.GetCachedFormMetaData(base.Context, formid);
                string pkFieldName = cachedFormMetaData.BusinessInfo.GetForm().PkFieldName;
                string masterPKFieldName = cachedFormMetaData.BusinessInfo.GetForm().MasterPKFieldName;
                string tableName = cachedFormMetaData.BusinessInfo.Entrys[0].TableName;
                string useOrgFieldKey = cachedFormMetaData.BusinessInfo.GetForm().UseOrgFieldKey;
                param.ListFilterParameter.Filter = param.ListFilterParameter.Filter.JoinFilterString(string.Format(" {0}={1} and {1} in (select distinct {1} from {2} where  FDOCUMENTSTATUS='C' AND FFORBIDSTATUS='A' AND {3} ={4}) ", new object[] { pkFieldName, masterPKFieldName, tableName, useOrgFieldKey, iD }), "AND");
                param.IsIsolationOrg = false;
            }
            else
            {
                param.IsIsolationOrg = true;
                param.UseOrgId = base.Context.CurrentOrganizationInfo.ID;
            }
            param.OpenStyle.ShowType = ShowType.Modal;
            this.View.ShowForm(param, delegate (Kingdee.BOS.Core.DynamicForm.FormResult formResult) {
                if (formResult != null)
                {
                    this.FillSelectedDatas(formResult, e, scopetype);
                }
            });
        }

        private void SetBugdetFormula(Kingdee.BOS.Core.DynamicForm.FormResult result)
        {
            if ((result != null) && (result.ReturnData != null))
            {
                this.Model.SetValue("FWIZARDSCHEMEID", result.ReturnData.ToString());
            }
        }

        private void ShowBugdetFormulaForm(BeforeF7SelectEventArgs e)
        {
            DynamicFormShowParameter param = new DynamicFormShowParameter {
                PageId = Guid.NewGuid().ToString()
            };
            param.OpenStyle.ShowType = ShowType.Modal;
            param.FormId = "BM_SelectScheme";
            param.CustomParams.Add("FSchemeFilter", e.ListFilterParameter.Filter);
            this.View.ShowForm(param, result => this.SetBugdetFormula(result));
        }

        public override void TreeNodeClick(TreeNodeArgs e)
        {
            base.TreeNodeClick(e);
            this._curFilterSchemeId = e.NodeId;
        }

        private enum DimentionScopeType
        {
            BillDimension = 2,
            Dimension = 1
        }
    }
}

