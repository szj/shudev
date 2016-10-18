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
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class BudgetExecutionFilter : AbstractCommonFilterPlugIn
    {
        public string _curFilterSchemeId;
        public string _defaultSchemeId;
        private const string BOS_ASSISTANTDATA_SELECT = "BOS_ASSISTANTDATA_SELECT";
        private const string CATEGORY_ASSISTANTDATA = "10";
        private Dictionary<int, List<int>> dicItemDataTypeID = new Dictionary<int, List<int>>();
        private DynamicObject dyCanlendar;
        private DynamicObject dyRptScheme;
        private DynamicObject dyRule;
        private DynamicObject dyScheme;
        public string FilterSchemeFormId;
        private const string KEY_FBillFormId = "FBillFormId";
        private readonly string KEY_FCtrlDimensionEntry = "FCtrlDimensionEntry";
        private const string KEY_FDimension = "FDimension";
        private const string KEY_FDimensionID = "FDimension_ID";
        private const string KEY_FFilterName = "FFILTERNAME";
        private readonly string WARNNING_BUDGETCTRLRULEISNULL = ResManager.LoadKDString("控制规则无效，可能已被删除！", "0032058000021920", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_EdPeriodLesStrPeriod = ResManager.LoadKDString("预算结束期间不能小于预算开始期间！", "0032058000021393", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_EdYearLesStrYear = ResManager.LoadKDString("预算结束年度不能小于预算开始年度！", "0032058000021392", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_SCHEMEISNULL = ResManager.LoadKDString("预算方案无效，可能已被删除！", "0032058000021921", SubSystemType.FIN, new object[0]);
        private readonly string WARNNING_SelectCtrlDimension = ResManager.LoadKDString("请先选择控制维度！", "0032058000021391", SubSystemType.FIN, new object[0]);

        public override void AfterBindData(EventArgs e)
        {
            this.SummaryCtrl();
            this.View.GetControl("FBWBCURRENCYID").Visible = false;
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

        public override void BarItemClick(BarItemClickEventArgs e)
        {
            base.BarItemClick(e);
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
                    if (str3 == "FRULEID")
                    {
                        e.ListFilterParameter.Filter = this.GetRuleFilter(filter);
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

        public void BindBudgetPeriod(DynamicObject dyScheme, DynamicObject dyCanlendar, bool isNeedToSetValue, int ctrlPeriod = 0, int summaryPeriod = 0)
        {
            if (dyCanlendar != null)
            {
                List<EnumItem> enumItemsByCalendar = BMCommonUtil.GetEnumItemsByCalendar(base.Context, dyCanlendar);
                this.View.GetControl<ComboFieldEditor>("FCtrlPeriod").SetComboItems(enumItemsByCalendar);
                this.View.GetControl<ComboFieldEditor>("FSummaryPeriod").SetComboItems(enumItemsByCalendar);
                if (isNeedToSetValue)
                {
                    this.View.Model.SetValue("FCtrlPeriod", ctrlPeriod);
                    this.View.Model.SetValue("FSummaryPeriod", summaryPeriod);
                }
                this.BindYearList(dyScheme, isNeedToSetValue);
                this.BindPeriodList(dyScheme, dyCanlendar, isNeedToSetValue, ctrlPeriod);
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
                DynamicObjectCollection objects = dyRule["CtrlDataEntry"] as DynamicObjectCollection;
                List<int> list = new List<int>();
                foreach (DynamicObject obj2 in objects)
                {
                    int item = Convert.ToInt32(obj2["BusinessType_Id"]);
                    list.Add(item);
                }
                this.View.Model.SetValue("FBusinessTypeIds", list);
                this.View.UpdateView("FBusinessTypeIds");
            }
        }

        public void BindCurrency(DynamicObject dyRule, bool isNeedToSetValue)
        {
            int num = Convert.ToInt32(dyRule["CurrencyId"]);
            if (num == 0)
            {
                BuildCurrency(base.Context, this.View, "FCURRENCYID", true);
                this.View.GetControl("FCURRENCYID").Enabled = false;
                BuildCurrency(base.Context, this.View, "FBWBCURRENCYID", false);
                this.View.GetControl("FBWBCURRENCYID").Visible = true;
                if (!isNeedToSetValue)
                {
                    this.Model.BeginIniti();
                    this.View.Model.SetValue("FCURRENCYID", num);
                    this.View.UpdateView("FCURRENCYID");
                    this.Model.EndIniti();
                }
            }
            else
            {
                BuildCurrency(base.Context, this.View, "FCURRENCYID", false);
                BuildCurrency(base.Context, this.View, "FBWBCURRENCYID", false);
                this.View.GetControl("FBWBCURRENCYID").Visible = false;
            }
            if (isNeedToSetValue)
            {
                this.View.Model.SetValue("FCURRENCYID", num);
            }
        }

        public void BindDataInfo(bool isNeedToSetValue = true)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            int num2 = this.GetBaseDataIdByField("FRULEID");
            if ((baseDataIdByField != 0) && (num2 != 0))
            {
                this.dyRule = BMCommonServiceHelper.LoadFormData(base.Context, "BM_BudgetCtrlRule", num2.ToString());
                if (!this.CheckDyObjIsNull(this.dyRule, this.WARNNING_BUDGETCTRLRULEISNULL))
                {
                    this.dyScheme = BMCommonServiceHelper.LoadFormData(base.Context, "BM_SCHEME", baseDataIdByField.ToString());
                    if (!this.CheckDyObjIsNull(this.dyScheme, this.WARNNING_SCHEMEISNULL))
                    {
                        int num3 = Convert.ToInt32(this.dyRule["patternScheme_Id"]);
                        int ctrlPeriod = Convert.ToInt32(this.dyRule["CtrlPeriod"]);
                        int summaryPeriod = Convert.ToInt32(this.dyRule["SummaryPeriod"]);
                        this.dyRptScheme = BMCommonServiceHelper.LoadFormData(base.Context, "BM_RPTSCHEME", num3.ToString());
                        this.dyCanlendar = BMCommonServiceHelper.LoadFormData(base.Context, "BM_BUDGETCALENDAR", this.dyScheme["CalendarId_Id"].ToString());
                        if (isNeedToSetValue)
                        {
                            this.View.Model.SetValue("FIsSummaryCtrl", Convert.ToBoolean(this.dyRule["IsSummaryCtrl"]));
                            this.View.Model.SetValue("FIsDimissionSumCtrl", Convert.ToBoolean(this.dyRule["IsDimissionSumCtrl"]));
                        }
                        this.BindCurrency(this.dyRule, isNeedToSetValue);
                        this.BindBudgetPeriod(this.dyScheme, this.dyCanlendar, isNeedToSetValue, ctrlPeriod, summaryPeriod);
                        this.BindItemDataTypes(this.dyRule, isNeedToSetValue);
                        this.BindBusinessTypes(this.dyRule, isNeedToSetValue);
                        this.BindDimessionEntry(this.dyRule, isNeedToSetValue);
                    }
                }
            }
        }

        public void BindDimessionEntry(DynamicObject dyRule, bool isNeedToSetValue)
        {
            if (isNeedToSetValue)
            {
                this.View.Model.DeleteEntryData("FCtrlDimensionEntry");
                DynamicObjectCollection objects = dyRule["CtrlDimensionEntry"] as DynamicObjectCollection;
                int row = 0;
                foreach (DynamicObject obj2 in objects)
                {
                    object obj1 = obj2["Dimension"];
                    this.Model.InsertEntryRow(this.KEY_FCtrlDimensionEntry, row);
                    this.View.Model.SetValue("FDimension", obj2["Dimension"], row);
                    this.View.Model.SetValue("FDimension_ID", obj2["Dimension_ID"], row);
                    this.View.Model.SetValue("FFilterName", obj2["FilterName"], row);
                    this.View.Model.SetValue("FFilterKey", obj2["FilterKey"], row);
                    this.View.Model.SetValue("FFilterKeyDesc", obj2["FilterKeyDesc"], row);
                    this.View.Model.SetValue("DimensionFormId", obj2["DimensionFormId"], row);
                    row++;
                }
                this.View.UpdateView(this.KEY_FCtrlDimensionEntry);
            }
        }

        public void BindItemDataTypes(DynamicObject dyRule, bool isNeedToSetValue)
        {
            if (isNeedToSetValue)
            {
                DynamicObjectCollection objects = dyRule["CtrlDataEntry"] as DynamicObjectCollection;
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

        public static void BuildCurrency(Context ctx, IDynamicFormView view, string controlName, bool isContianBWB = true)
        {
            if (!string.IsNullOrWhiteSpace(controlName) && (view != null))
            {
                DynamicObjectCollection objects = BMCommonServiceHelper.QueryData(ctx, "BD_Currency", "FCurrencyId,FName", "FDocumentStatus='C' and FForbidStatus='A'");
                if ((objects != null) && (objects.Count > 0))
                {
                    List<EnumItem> items = new List<EnumItem>();
                    List<string> list2 = new List<string>();
                    foreach (DynamicObject obj2 in objects)
                    {
                        items.Add(CreateEnumItem(view, obj2["FCurrencyId"], (obj2["FName"] == null) ? "" : obj2["FName"].ToString()));
                        list2.Add(obj2["FCurrencyId"].ToString());
                    }
                    if (isContianBWB)
                    {
                        items.Add(CreateEnumItem(view, 0, GetCurrencyName(ctx, 0L)));
                    }
                    object obj3 = view.Model.GetValue(controlName);
                    if ((obj3 != null) && list2.Contains(obj3.ToString()))
                    {
                        obj3 = view.Model.GetValue(controlName);
                    }
                    else
                    {
                        obj3 = objects[0]["FCurrencyId"];
                    }
                    view.GetControl<ComboFieldEditor>(controlName).SetComboItems(items);
                    view.Model.SetValue(controlName, obj3);
                }
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
            if (bool.Parse(this.View.Model.GetValue("FIsSummaryCtrl").ToString()))
            {
                string s = Convert.ToString(this.View.Model.GetValue("FSummaryPeriod"));
                string str2 = Convert.ToString(this.View.Model.GetValue("FCtrlPeriod"));
                if (int.Parse(s) >= int.Parse(str2))
                {
                    this.View.ShowMessage(ResManager.LoadKDString("汇总周期必须大于预算周期！", "0032058000021907", SubSystemType.FIN, new object[0]), MessageBoxType.Notice);
                    return true;
                }
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
            switch (e.Field.Key.ToUpperInvariant())
            {
                case "FSCHEMEID":
                    this.BindDataInfo(true);
                    return;

                case "FRULEID":
                    this.View.Model.DeleteEntryData(this.KEY_FCtrlDimensionEntry);
                    this.View.Model.SetValue("FITEMDATATYPEIDS", "");
                    this.View.UpdateView("FITEMDATATYPEIDS");
                    this.BindDataInfo(true);
                    return;

                case "FISSUMMARYCTRL":
                    this.SummaryCtrl();
                    return;

                case "FPERIODTYPE":
                    if (e.NewValue == null)
                    {
                        break;
                    }
                    this.BindBudgetPeriod(this.dyScheme, this.dyCanlendar, true, Convert.ToInt32(e.NewValue), 0);
                    return;

                case "FITEMDATATYPEIDS":
                    this.BindBusinessTypes();
                    return;

                case "FCURRENCYID":
                    if (Convert.ToInt32(e.NewValue) != 0)
                    {
                        this.View.GetControl("FBWBCURRENCYID").Visible = false;
                        return;
                    }
                    this.View.Model.SetValue("FOrgIds", "");
                    this.View.GetControl("FBWBCURRENCYID").Visible = true;
                    BuildCurrency(base.Context, this.View, "FBWBCURRENCYID", false);
                    return;

                case "FBWBCURRENCYID":
                    this.View.Model.SetValue("FOrgIds", "");
                    break;

                default:
                    return;
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
            if (this.GetBaseDataIdByField("FRULEID") != 0)
            {
                DynamicObjectCollection objects = this.dyRule["CtrlDataEntry"] as DynamicObjectCollection;
                this.dicItemDataTypeID.Clear();
                List<int> source = new List<int>();
                foreach (DynamicObject obj2 in objects)
                {
                    List<int> list2;
                    int item = Convert.ToInt32(obj2["ItemDataType_ID"]);
                    int num3 = Convert.ToInt32(obj2["BusinessType_ID"]);
                    source.Add(item);
                    if (this.dicItemDataTypeID.TryGetValue(item, out list2))
                    {
                        list2.Add(num3);
                    }
                    else
                    {
                        List<int> list3 = new List<int> {
                            num3
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
            }
            return filter;
        }

        public string GetOrgFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            if ((this.GetBaseDataIdByField("FRULEID") == 0) || (baseDataIdByField == 0))
            {
                filter = " 1=2 ";
                return filter;
            }
            List<int> budgetOrgIdBySchemeId = BMCommonServiceHelper.GetBudgetOrgIdBySchemeId(base.Context, baseDataIdByField);
            if (budgetOrgIdBySchemeId.Count<int>() <= 0)
            {
                return "1=2";
            }
            List<int> source = new List<int>();
            if (Convert.ToInt32(this.View.Model.GetValue("FCURRENCYID")) == 0)
            {
                int currencyId = Convert.ToInt32(this.View.Model.GetValue("FBWBCURRENCYID"));
                source.AddRange(BMCommonServiceHelper.GetMainOrgUseDefAcctPLCYByCurrencyId(base.Context, currencyId));
                source.AddRange(BMCommonServiceHelper.GetSubnOrgUseDefAcctPLCYByCurrencyId(base.Context, currencyId));
                if (source.Count <= 0)
                {
                    return "1=2";
                }
            }
            if (source.Count > 0)
            {
                budgetOrgIdBySchemeId = new List<int>(budgetOrgIdBySchemeId.Intersect<int>(source.Distinct<int>()));
            }
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

        public string GetRuleFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FSCHEMEID");
            if (baseDataIdByField > 0)
            {
                if (string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
                {
                    filter = this.GetSchemeRuleFilter(baseDataIdByField, 0);
                    return filter;
                }
                filter = filter + " AND " + this.GetSchemeRuleFilter(baseDataIdByField, 0);
            }
            return filter;
        }

        public string GetSchemeFilter(string filter)
        {
            int baseDataIdByField = this.GetBaseDataIdByField("FRULEID");
            if (baseDataIdByField > 0)
            {
                if (string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
                {
                    filter = this.GetSchemeRuleFilter(0, baseDataIdByField);
                    return filter;
                }
                filter = filter + " AND " + this.GetSchemeRuleFilter(0, baseDataIdByField);
            }
            return filter;
        }

        public string GetSchemeRuleFilter(int schemeId, int ruleId)
        {
            string format = " {0} IN (SELECT {1} FROM T_BM_CTRLPLATFORM A INNER JOIN T_BM_CTRLPLATFORMENTITY B ON A.FID = B.FID WHERE A.FDOCUMENTSTATUS='C' AND A.FFORBIDSTATUS='A' AND {2}={3}) ";
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

        private void SetSumaryCtrlVisible(bool isVisible)
        {
            this.View.GetControl("FSummaryPeriod").Visible = isVisible;
        }

        private void SummaryCtrl()
        {
            this.View.GetControl("FCtrlPeriod").Enabled = !bool.Parse(this.View.Model.GetValue("FIsSummaryCtrl").ToString());
            bool isVisible = Convert.ToBoolean(this.View.Model.GetValue("FISSUMMARYCTRL"));
            this.SetSumaryCtrlVisible(isVisible);
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

