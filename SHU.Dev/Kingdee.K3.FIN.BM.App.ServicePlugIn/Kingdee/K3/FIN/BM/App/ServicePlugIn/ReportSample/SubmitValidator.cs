namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;

    internal class SubmitValidator : AbstractValidator
    {
        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                DynamicObjectCollection objects = entity["BM_Sheet"] as DynamicObjectCollection;
                if ((objects == null) || (objects.Count <= 0))
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("FNumber", "FRptID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), string.Format(ResManager.LoadKDString("预算模板 {0} 未编辑，不允许提交！", "0032056000021823", SubSystemType.FIN, new object[0]), entity["Name"].ToString()), string.Format(ResManager.LoadKDString("预算模板 {0} 未编辑，不允许提交！", "0032056000021823", SubSystemType.FIN, new object[0]), entity["Name"].ToString()), ErrorLevel.Error));
                }
            }
        }
    }
}

