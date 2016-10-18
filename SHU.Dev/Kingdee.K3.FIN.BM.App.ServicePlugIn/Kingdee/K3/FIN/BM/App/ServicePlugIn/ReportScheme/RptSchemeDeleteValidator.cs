namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportScheme
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;

    internal class RptSchemeDeleteValidator : AbstractValidator
    {
        private void DeleteValidator(Context ctx, ValidateContext validateContext, ExtendedDataEntity entity, ReportSchemeService service)
        {
            long reportSchemeId = Convert.ToInt64(entity["Id"]);
            bool flag = service.IsReportSchemeUsed(ctx, reportSchemeId);
            bool ctrlRuleBySchemeId = service.GetCtrlRuleBySchemeId(ctx, reportSchemeId);
            if (flag || ctrlRuleBySchemeId)
            {
                string.Format("模板样式方案 {0} 已经被引用，不允许删除！", entity["Name"]);
                validateContext.AddError(entity, new ValidationErrorInfo("Name", "Id", entity.DataEntityIndex, 0, Convert.ToString(entity["Id"]), "该模板样式方案已经被其他单据引用，不能删除！", "删除：", ErrorLevel.Error));
            }
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            ReportSchemeService service = new ReportSchemeService();
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                this.DeleteValidator(ctx, validateContext, entity, service);
            }
        }
    }
}

