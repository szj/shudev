namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportScheme
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("预算模板样式方案-删除")]
    public class Delete : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            RptSchemeDeleteValidator item = new RptSchemeDeleteValidator {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }
    }
}

