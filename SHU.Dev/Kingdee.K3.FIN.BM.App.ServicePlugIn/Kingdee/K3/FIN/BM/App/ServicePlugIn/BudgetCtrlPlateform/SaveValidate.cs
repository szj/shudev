namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCtrlPlateform
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SaveValidate : AbstractValidator
    {
        private BudgetCtrlPlateformMain Convert2Entity(ExtendedDataEntity parm)
        {
            return new BudgetCtrlPlateformMain { DeptOrgId = int.Parse(parm.DataEntity["DEPTORGID_ID"].ToString()), OrgType = parm.DataEntity["ORGTYPE"].ToString(), OrgId = int.Parse((parm.DataEntity["DEPTORGID"] as DynamicObject)["ORGID"].ToString()), DeptOrgName = (parm.DataEntity["DEPTORGID"] as DynamicObject)["OrgName"].ToString(), SchemeId = int.Parse(parm.DataEntity["FSCHEMEID_ID"].ToString()), SchemeNumber = (parm.DataEntity["FSCHEMEID"] as DynamicObject)["Number"].ToString() };
        }

        private IList<BudgetCtrlPlateformMain> GetCtrlPlateformDate(ExtendedDataEntity[] dataEntities)
        {
            IList<BudgetCtrlPlateformMain> result = new List<BudgetCtrlPlateformMain>();
            dataEntities.ToList<ExtendedDataEntity>().ForEach(delegate (ExtendedDataEntity entity) {
                BudgetCtrlPlateformMain item = this.Convert2Entity(entity);
                result.Add(item);
            });
            return result;
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            IList<BudgetCtrlPlateformMain> ctrlPlateformDate = this.GetCtrlPlateformDate(dataEntities);
            IDictionary<BudgetCtrlPlateformMain, bool> dictionary = new BudgetCtrlPlateformService().CheckRepeatForHead(ctx, ctrlPlateformDate);
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                BudgetCtrlPlateformMain main = this.Convert2Entity(entity);
                if (dictionary[main])
                {
                    string message = string.Format(ResManager.LoadKDString("预算组织：{0} 预算方案：{1} 单据头已存在，不能新增", "0032056000020696", SubSystemType.FIN, new object[0]), main.DeptOrgName, main.SchemeNumber);
                    string title = ResManager.LoadKDString("批量保存", "0032056000020697", SubSystemType.FIN, new object[0]);
                    validateContext.AddError(entity, new ValidationErrorInfo("????", "0", 0, 0, "0", message, title, ErrorLevel.Error));
                }
            }
        }
    }
}

