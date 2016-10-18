namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetFormula
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.BM.App.ServicePlugIn;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ItemDataFormulaSaveValidator : AbstractValidator
    {
        private void CheckFormula(Context ctx, ValidateContext validateContext, ExtendedDataEntity entity, BudgetFormulaService service)
        {
            string formula = Convert.ToString(entity["Express"]);
            if (!service.ValidityFormula(formula))
            {
                string title = ResManager.LoadKDString("合法性校验", "003286000011212", SubSystemType.FIN, new object[0]);
                string msg = ResManager.LoadKDString("公式设置不正确，请重新设置！", "003286000011305", SubSystemType.FIN, new object[0]);
                CommonValidate.AddMsg(validateContext, entity, title, msg, "", ErrorLevel.Error);
            }
        }

        private List<long> GetOrgIds(ExtendedDataEntity entity)
        {
            DynamicObjectCollection objects = entity["OrgId"] as DynamicObjectCollection;
            if (objects == null)
            {
                return new List<long>();
            }
            return (from p in objects select Convert.ToInt64(p["OrgId_Id"])).ToList<long>();
        }

        private List<long> GetProfitCenterIds(ExtendedDataEntity entity)
        {
            DynamicObjectCollection objects = entity["ProfitCenter"] as DynamicObjectCollection;
            if (objects == null)
            {
                return new List<long>();
            }
            return (from p in objects select Convert.ToInt64(p["ProfitCenter_Id"])).ToList<long>();
        }

        private string GetRptUseTypeName(string useType)
        {
            switch (useType)
            {
                case "1":
                    return ResManager.LoadKDString("个别报表", "003286000011296", SubSystemType.FIN, new object[0]);

                case "2":
                    return ResManager.LoadKDString("抵消报表", "003286000011297", SubSystemType.FIN, new object[0]);

                case "3":
                    return ResManager.LoadKDString("阿米巴报表", "003286000011298", SubSystemType.FIN, new object[0]);
            }
            return string.Empty;
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            BudgetFormulaService service = new BudgetFormulaService();
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                this.CheckFormula(ctx, validateContext, entity, service);
            }
        }
    }
}

