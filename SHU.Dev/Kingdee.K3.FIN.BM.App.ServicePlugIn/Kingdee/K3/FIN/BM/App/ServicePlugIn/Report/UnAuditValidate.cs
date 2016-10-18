namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnAuditValidate : AbstractValidator
    {
        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            if ((dataEntities != null) && (dataEntities.Length > 0))
            {
                List<string> list = (from entity in dataEntities.ToList<ExtendedDataEntity>() select entity.DataEntity["Id"].ToString()).ToList<string>();
                new BudgetReportPlateService().GetReportExecuteStatus(ctx, list);
                foreach (ExtendedDataEntity entity in dataEntities)
                {
                    entity.DataEntity["Id"].ToString();
                    string str = entity.DataEntity["NUMBER"].ToString();
                    if (Convert.ToInt32(entity.DataEntity["EXCUTESTATUS"]) != 1)
                    {
                        string message = ResManager.LoadKDString("报表非未执行状态，不能反审核！", "0032056000022152", SubSystemType.FIN, new object[0]);
                        string title = ResManager.LoadKDString("反审核：", "0032056000020553", SubSystemType.FIN, new object[0]) + str;
                        validateContext.AddError(entity, new ValidationErrorInfo("FNumber", "FRptID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), message, title, ErrorLevel.Error));
                    }
                }
            }
        }
    }
}

