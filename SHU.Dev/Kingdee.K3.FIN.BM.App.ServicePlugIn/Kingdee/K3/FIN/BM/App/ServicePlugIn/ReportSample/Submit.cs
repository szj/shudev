namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("预算报表模板-提交")]
    public class Submit : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            SubmitValidator item = new SubmitValidator {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            e.FieldKeys.Add("FSheetName");
        }
    }
}

