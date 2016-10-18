namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Adjust
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.ComponentModel;

    [Description("预算调整单--反审核插件")]
    public class UnCommit : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            base.BeginOperationTransaction(e);
            this.UpdatebudgetValue(e);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add("FSheetID");
        }

        private void UpdatebudgetValue(BeginOperationTransactionArgs e)
        {
            new BudgetAdjustService().UpdatebudgetValue(base.Context, e, false);
        }
    }
}

