namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.ComponentModel;

    [Description("预算报表模板-反禁用")]
    public class UnForbid : AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(Kingdee.BOS.Core.DynamicForm.PlugIn.Args.AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            foreach (DynamicObject obj2 in e.DataEntitys)
            {
                this.TryForbidReportDistribute(obj2);
            }
        }

        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
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

        internal class LegalValidator : AbstractValidator
        {
            public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
            {
                foreach (ExtendedDataEntity entity in dataEntities)
                {
                    if (!ServiceFactory.GetService<IReportSchemeService>(base.Context).CheckSampleDistribute(base.Context, entity["Id"].ToString()))
                    {
                        validateContext.AddError(entity, new ValidationErrorInfo("FNumber", "FRptID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), "一个模板样式方案只能在一个组织/方案下，存在一次", "预算模版反禁用", ErrorLevel.Error));
                    }
                }
            }
        }
    }
}

