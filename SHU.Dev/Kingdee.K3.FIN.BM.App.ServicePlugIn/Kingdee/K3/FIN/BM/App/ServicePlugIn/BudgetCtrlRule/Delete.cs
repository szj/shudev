namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCtrlRule
{
    using Kingdee.BOS;
    using Kingdee.BOS.App;
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    [Description("预算控制规则服务插件--删除")]
    public class Delete : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            base.BeginOperationTransaction(e);
            DynamicObject obj2 = this.CreateLogDy(base.Context, e);
            if (obj2 != null)
            {
                BudgetCtrlLog log = new BudgetCtrlLog(base.Context);
                List<int> ruleIds = (from dy in e.DataEntitys select dy.GetDynamicObjectItemValue<int>("Id", 0)).ToList<int>();
                log.UpdateLogForRule(ruleIds);
                log.SaveLogByDys(new DynamicObject[] { obj2 });
            }
        }

        private DynamicObject CreateLogDy(Context ctx, BeginOperationTransactionArgs e)
        {
            DynamicObject obj2 = null;
            if (e.DataEntitys.Count<DynamicObject>() <= 0)
            {
                return obj2;
            }
            FormMetadata metadata = ServiceHelper.GetService<IMetaDataService>().Load(ctx, "BM_BUDGETCTRLLOG", true) as FormMetadata;
            BusinessInfo businessInfo = metadata.BusinessInfo;
            DynamicObject obj3 = (DynamicObject) metadata.BusinessInfo.GetDynamicObjectType().CreateInstance();
            obj3["BillFormID_Id"] = " ";
            obj3["FBillNo"] = " ";
            obj3["OperationId"] = 3;
            obj3["OperationNumber"] = "Delete";
            obj3["FCreateDate"] = DateTime.Now;
            obj3["FCreatorId_Id"] = ctx.UserId;
            obj3["Description"] = ResManager.LoadKDString("预算控制规则-删除", "0032056000021791", SubSystemType.FIN, new object[0]);
            int num = 1;
            string str = ResManager.LoadKDString("预算控制规则删除", "0032056000021794", SubSystemType.FIN, new object[0]);
            string format = ResManager.LoadKDString("预算控制规则:{0}[{1}]删除", "0032056000021795", SubSystemType.FIN, new object[0]);
            foreach (DynamicObject obj4 in e.DataEntitys)
            {
                DynamicObjectCollection objects = obj3["LOGENTITY"] as DynamicObjectCollection;
                DynamicObject item = objects.DynamicCollectionItemPropertyType.CreateInstance() as DynamicObject;
                item["SEQ"] = num++;
                int num2 = obj4.GetDynamicObjectItemValue<int>("Id", 0);
                string name = this.GetName(obj4.GetDynamicObjectItemValue<LocaleValue>("name", null));
                item["RuleID"] = num2;
                item["RuleName"] = name;
                item["LogTime"] = DateTime.Now;
                item["Descriptions"] = str;
                item["Detailed"] = string.Format(format, name, num2);
                item["Level"] = 0;
                item["LogType"] = 100.ToString();
                objects.Add(item);
            }
            return obj3;
        }

        private string GetName(LocaleValue value)
        {
            string str = "";
            value.TryGetValue(base.Context.UserLocale.LCID, out str);
            return str;
        }
    }
}

