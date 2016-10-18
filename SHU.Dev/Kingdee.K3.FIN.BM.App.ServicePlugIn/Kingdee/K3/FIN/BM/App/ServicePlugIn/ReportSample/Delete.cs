namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Description("预算报表模板-删除")]
    public class Delete : AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(Kingdee.BOS.Core.DynamicForm.PlugIn.Args.AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            foreach (DynamicObject obj2 in e.DataEntitys)
            {
                this.TryDeleteReportScheme(obj2);
            }
            this.DelteRptDimDetail(e.DataEntitys);
        }

        private void DelteRptDimDetail(DynamicObject[] dys)
        {
            List<string> sheets = new List<string>();
            foreach (DynamicObject obj2 in dys)
            {
                DynamicObjectCollection objects = obj2["BM_Sheet"] as DynamicObjectCollection;
                foreach (DynamicObject obj3 in objects)
                {
                    sheets.Add(obj3["Id"].ToString());
                }
            }
            if (sheets.Count > 0)
            {
                new ReportSchemeService().DeleteReportDimDetail(base.Context, sheets);
            }
        }

        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            DeleteValidate item = new DeleteValidate {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            e.FieldKeys.Add("FRPTSchemeId");
        }

        private void TryDeleteReportScheme(DynamicObject entity)
        {
            DynamicObjectCollection objects = entity["BM_Sheet"] as DynamicObjectCollection;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(base.Context);
            try
            {
                foreach (DynamicObject obj2 in objects)
                {
                    long reportSchemeId = Convert.ToInt64(obj2["RptSchemeId"]);
                    service.DeleteReportScheme(base.Context, reportSchemeId);
                }
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
        }
    }
}

