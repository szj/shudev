namespace Kingdee.K3.FIN.BM.Report.PlugIn
{
    using Kingdee.BOS.Core.CommonFilter;
    using Kingdee.BOS.Core.Report;
    using Kingdee.BOS.Core.Report.PlugIn;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.Core;
    using Kingdee.K3.FIN.BM.ServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Description("预算数据查询插件")]
    public class BudgetDataPlugIn : AbstractSysReportPlugIn
    {
        public override void AfterBindData(EventArgs e)
        {
            FilterParameter filterParameter = ((ISysReportView) this.View).Model.FilterParameter;
            int num = Convert.ToInt32(filterParameter.CustomFilter["SchemeId_Id"]);
            DynamicObject obj2 = BMCommonServiceHelper.LoadFormData(base.Context, "BM_SCHEME", num.ToString());
            if (obj2 != null)
            {
                DynamicObject dyCanlendar = BMCommonServiceHelper.LoadFormData(base.Context, "BM_BUDGETCALENDAR", obj2["CalendarId_Id"].ToString());
                if (dyCanlendar != null)
                {
                    this.BindBudgetPeriod(dyCanlendar);
                    int num2 = Convert.ToInt32(filterParameter.CustomFilter["FCtrlPeriod"]);
                    this.View.Model.SetValue("FSCHEMEID", num);
                    this.View.Model.SetValue("FCTRLPERIOD", num2);
                    this.View.UpdateView("FSCHEMEID");
                    this.View.UpdateView("FCTRLPERIOD");
                }
            }
        }

        public void BindBudgetPeriod(DynamicObject dyCanlendar)
        {
            if (dyCanlendar != null)
            {
                List<EnumItem> enumItemsByCalendar = BMCommonUtil.GetEnumItemsByCalendar(base.Context, dyCanlendar);
                this.View.GetControl<ComboFieldEditor>("FCtrlPeriod").SetComboItems(enumItemsByCalendar);
            }
        }
    }
}

