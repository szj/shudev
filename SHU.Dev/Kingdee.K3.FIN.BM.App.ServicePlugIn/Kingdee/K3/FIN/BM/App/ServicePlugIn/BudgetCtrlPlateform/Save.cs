namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCtrlPlateform
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("预算控制台-保存")]
    public class Save : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            SaveValidate item = new SaveValidate {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }
    }
}

