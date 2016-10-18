namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.ComponentModel;

    [Description("预算报表模板-禁用")]
    public class Forbid : AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(Kingdee.BOS.Core.DynamicForm.PlugIn.Args.AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            foreach (DynamicObject obj2 in e.DataEntitys)
            {
                this.TryForbidReportDistribute(obj2);
            }
        }

        private void TryForbidReportDistribute(DynamicObject entity)
        {
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(base.Context);
            try
            {
                service.UpdateDistributeTempStatus(base.Context, entity["Id"].ToString(), entity["ForbidStatus"].ToString());
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
        }
    }
}

