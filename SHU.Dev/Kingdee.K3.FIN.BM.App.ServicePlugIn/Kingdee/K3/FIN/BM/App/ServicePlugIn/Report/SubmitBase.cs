namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using System;
    using System.ComponentModel;

    [Description("报表基础-提交")]
    public class SubmitBase : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            SubmitValidate item = new SubmitValidate {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            e.FieldKeys.Add("FSheetName");
            e.FieldKeys.Add("FOrgID");
            e.FieldKeys.Add("FNUMBER");
            e.FieldKeys.Add("FCycleID");
            e.FieldKeys.Add("FYear");
            e.FieldKeys.Add("FCurrencyID");
            e.FieldKeys.Add("FSchemeID");
        }
    }
}

