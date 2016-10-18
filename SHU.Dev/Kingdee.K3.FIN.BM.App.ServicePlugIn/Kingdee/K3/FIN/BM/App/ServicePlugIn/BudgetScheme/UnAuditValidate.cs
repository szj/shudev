namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetScheme
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
            List<string> list = (from entity in dataEntities.ToList<ExtendedDataEntity>() select entity.DataEntity["Id"].ToString()).ToList<string>();
            IDictionary<string, bool> dictionary = new DistributeService().CheckIsSchemeDistributed(base.Context, list);
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                string key = entity.DataEntity["Id"].ToString();
                string str2 = entity.DataEntity["Number"].ToString();
                if (dictionary.ContainsKey(key) && dictionary[key])
                {
                    string message = ResManager.LoadKDString("该预算方案已经存在分发，不能反审核！", "0032056000020552", SubSystemType.FIN, new object[0]);
                    string title = ResManager.LoadKDString("反审核:", "0032056000020553", SubSystemType.FIN, new object[0]) + str2;
                    validateContext.AddError(entity, new ValidationErrorInfo("Number", "ID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), message, title, ErrorLevel.Error));
                }
            }
        }
    }
}

