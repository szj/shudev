namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportSample
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DeleteValidate : AbstractValidator
    {
        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            if ((dataEntities != null) && (dataEntities.Length > 0))
            {
                List<string> sampleIdList = (from entity in dataEntities.ToList<ExtendedDataEntity>() select entity.DataEntity["Id"].ToString()).ToList<string>();
                IDictionary<string, bool> dictionary = new DistributeService().CheckIsSampleDistributed(base.Context, sampleIdList, "");
                foreach (ExtendedDataEntity entity in dataEntities)
                {
                    string key = entity.DataEntity["Id"].ToString();
                    string str2 = entity.DataEntity["NUMBER"].ToString();
                    if (dictionary.ContainsKey(key) && dictionary[key])
                    {
                        validateContext.AddError(entity, new ValidationErrorInfo("Number", "ID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("该模板已经被预算方案引用，不能删除！", "0032056000020512", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("删除：", "0032056000020565", SubSystemType.FIN, new object[0]) + str2, ErrorLevel.Error));
                    }
                }
            }
        }
    }
}

