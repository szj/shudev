namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Adjust
{
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    [Description("预算调整单--审核插件")]
    public class Commit : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            base.BeginOperationTransaction(e);
            if (e.DataEntitys.Count<DynamicObject>() != 0)
            {
                List<int> list = new List<int>();
                BudgetAdjustService service = new BudgetAdjustService();
                Dictionary<int, IOperationResult> dictionary = new Dictionary<int, IOperationResult>();
                foreach (DynamicObject obj2 in e.DataEntitys)
                {
                    int fid = Convert.ToInt32(obj2["Id"]);
                    IOperationResult result = service.BuildBugdetAdjustReport(base.Context, fid);
                    if (result.IsSuccess)
                    {
                        list.Add(fid);
                        if (result.SuccessDataEnity != null)
                        {
                            obj2["Report"] = (result.SuccessDataEnity as List<DynamicObject>)[0]["SampleID_Id"];
                        }
                    }
                    dictionary.Add(fid, result);
                }
                this.UpdatebudgetValue(e);
            }
        }

        public override void EndOperationTransaction(EndOperationTransactionArgs e)
        {
            base.EndOperationTransaction(e);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add("FREPORT");
            e.FieldKeys.Add("FSampleID");
            e.FieldKeys.Add("FCYCLEID");
            e.FieldKeys.Add("FPeriod");
            e.FieldKeys.Add("FBudgetOrgId");
            e.FieldKeys.Add("FOrgDeptId");
            e.FieldKeys.Add("FCurrency");
            e.FieldKeys.Add("FAmountUnit");
            e.FieldKeys.Add("FCYCLEID");
            e.FieldKeys.Add("FOrgTypeID");
            e.FieldKeys.Add("FSchemeId");
            e.FieldKeys.Add("FYEAR");
            e.FieldKeys.Add("FADJUSTDATE");
            e.FieldKeys.Add("FRateTypeId");
            e.FieldKeys.Add("FBudgetOrgEntryID");
            e.FieldKeys.Add("FSheetID");
            e.FieldKeys.Add("FRptSchemeId");
            e.FieldKeys.Add("FBUSINESSTYPEID");
            e.FieldKeys.Add("FItemDataTypeId");
            e.FieldKeys.Add("FFinalAmount");
            e.FieldKeys.Add("FGroupID");
        }

        private void UpdatebudgetValue(BeginOperationTransactionArgs e)
        {
            new BudgetAdjustService().UpdatebudgetValue(base.Context, e, true);
        }
    }
}

