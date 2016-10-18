namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetFormula
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    [Description("项目数据公式-保存")]
    public class Save : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            DynamicObjectCollection allCustomFunctions = new BudgetFormulaService().GetAllCustomFunctions(base.Context);
            foreach (DynamicObject obj2 in e.DataEntitys)
            {
                string input = Convert.ToString(obj2["Express"]).ToLowerInvariant();
                List<string> values = new List<string>();
                foreach (DynamicObject obj3 in allCustomFunctions)
                {
                    Regex regex = new Regex(string.Format(@"\s*({0})\s*\(", Convert.ToString(obj3["FNumber"])), RegexOptions.IgnoreCase);
                    if (regex.Match(input).Success)
                    {
                        values.Add("_" + Convert.ToString(obj3["FID"]) + "_");
                    }
                }
                obj2["FunctionIds"] = string.Join(",", values);
            }
        }

        public override void EndOperationTransaction(EndOperationTransactionArgs e)
        {
        }

        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            ItemDataFormulaSaveValidator item = new ItemDataFormulaSaveValidator {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }
    }
}

