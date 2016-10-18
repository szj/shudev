namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("报表基础-保存")]
    public class SaveBase : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            SaveValidate item = new SaveValidate {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            e.FieldKeys.Add("FMultiDeptOrgID");
        }
    }
}

