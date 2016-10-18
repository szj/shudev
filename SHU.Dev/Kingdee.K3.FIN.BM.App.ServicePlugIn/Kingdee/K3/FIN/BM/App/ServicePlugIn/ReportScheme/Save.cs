namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportScheme
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.ComponentModel;

    [Description("模板样式方案-保存")]
    public class Save : AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(Kingdee.BOS.Core.DynamicForm.PlugIn.Args.AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            long rptSchemeId = Convert.ToInt64(e.DataEntitys[0]["Id"]);
            if (Convert.ToInt64(e.DataEntitys[0]["VersionGroupId"]) == 0L)
            {
                IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(base.Context);
                try
                {
                    service.SetVersionGroupId(base.Context, rptSchemeId);
                }
                finally
                {
                    ServiceFactory.CloseService(service);
                }
            }
        }

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

