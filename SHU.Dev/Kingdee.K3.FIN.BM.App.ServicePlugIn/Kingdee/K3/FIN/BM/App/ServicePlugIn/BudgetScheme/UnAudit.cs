namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetScheme
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("预算方案-反审核")]
    public class UnAudit : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            UnAuditValidate item = new UnAuditValidate {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }
    }
}

