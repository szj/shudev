namespace Kingdee.K3.FIN.BM.Report.PlugIn
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.CommonFilter;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Report;
    using Kingdee.BOS.Core.Report.PlugIn;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.ServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class BudgetExecutionPlugIn : AbstractSysReportPlugIn
    {
        private const string KEY_TBAPPLYTAKEUP = "TBAPPLYTAKEUP";
        private const string KEY_TBBILLCONNQUERY = "TBBILLCONNQUERY";
        private const string KEY_TBBUDGETEXECUTE = "TBBUDGETEXECUTE";
        private const string KEY_TBEXECUTEWRITEBACK = "TBEXECUTEWRITEBACK";

        public override void AfterBarItemClick(AfterBarItemClickEventArgs e)
        {
            base.AfterBarItemClick(e);
        }

        public override void AfterBindData(EventArgs e)
        {
            FilterParameter filterParameter = ((ISysReportView) this.View).Model.FilterParameter;
            int num = Convert.ToInt32(filterParameter.CustomFilter["SchemeId_Id"]);
            int num2 = Convert.ToInt32(filterParameter.CustomFilter["RuleId_Id"]);
            int currencyId = Convert.ToInt32(filterParameter.CustomFilter["FCURRENCYID"]);
            int num4 = Convert.ToInt32(filterParameter.CustomFilter["FBWBCURRENCYID"]);
            this.BindCurrency(currencyId);
            this.View.Model.SetValue("FSCHEMEID", num);
            this.View.Model.SetValue("FRULEID", num2);
            this.View.Model.SetValue("FCURRENCYID", currencyId);
            this.View.Model.SetValue("FBWBCURRENCYID", num4);
            this.View.UpdateView("FSCHEMEID");
            this.View.UpdateView("FRULEID");
            this.View.UpdateView("FCURRENCYID");
            this.View.UpdateView("FBWBCURRENCYID");
        }

        public void BindCurrency(int currencyId)
        {
            if (currencyId == 0)
            {
                BuildCurrency(base.Context, this.View, "FCURRENCYID", true);
                this.View.GetControl("FCURRENCYID").Enabled = false;
                BuildCurrency(base.Context, this.View, "FBWBCURRENCYID", false);
                this.View.GetControl("FBWBCURRENCYID").Visible = true;
            }
            else
            {
                BuildCurrency(base.Context, this.View, "FCURRENCYID", false);
                BuildCurrency(base.Context, this.View, "FBWBCURRENCYID", false);
                this.View.GetControl("FBWBCURRENCYID").Visible = false;
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

        public static EnumItem CreateEnumItem(IDynamicFormView view, object key, string displayValue)
        {
            return CreateEnumItem(view.Context, key, key, displayValue);
        }

        public static EnumItem CreateEnumItem(Context ctx, object key, object val, string displayValue)
        {
            return new EnumItem(new DynamicObject(EnumItem.EnumItemType)) { EnumId = key.ToString(), Value = val.ToString(), Caption = new LocaleValue(displayValue, ctx.UserLocale.LCID) };
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
    }
}

