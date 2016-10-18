namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCalendar
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using System;
    using System.ComponentModel;

    [Description("预算日历服务插件--保存")]
    public class Save : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            if (((e.DataEntitys != null) && (e.DataEntitys.Length > 0)) && (e.DataEntitys[0]["BudgetCalendarType"].ToString() == "1"))
            {
                DynamicObject obj2 = e.DataEntitys[0]["ACId"] as DynamicObject;
                if (obj2 != null)
                {
                    e.DataEntitys[0]["StartDate"] = obj2["STARTDATE"];
                    e.DataEntitys[0]["EndDate"] = obj2["ENDDATE"];
                }
            }
        }
    }
}

