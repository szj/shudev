namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCalendar
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Description("生成预算日历")]
    public class CreateBudgetPriodServicePlugIn : AbstractOperationServicePlugIn
    {
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            base.OnAddValidators(e);
            LegalValidator item = new LegalValidator {
                EntityKey = "FBillHead"
            };
            e.Validators.Add(item);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add("FBUDGETCALENDARTYPE");
            e.FieldKeys.Add("FACID");
            e.FieldKeys.Add("FSTARTDATE");
            e.FieldKeys.Add("FENDDATE");
            e.FieldKeys.Add("FYEAR");
            e.FieldKeys.Add("FMONTH");
            e.FieldKeys.Add("FHALFOFYEAR");
            e.FieldKeys.Add("FSEASON");
            e.FieldKeys.Add("FTENDAYS");
            e.FieldKeys.Add("FWEEKS");
            e.FieldKeys.Add("FDAYS");
            e.FieldKeys.Add("FCYCLESELECTTYPE");
        }

        internal class LegalValidator : AbstractValidator
        {
            public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
            {
                foreach (ExtendedDataEntity entity in dataEntities)
                {
                    if ((Convert.ToString(entity["BUDGETCALENDARTYPE"]) == "1") && !(entity["ACID"] is DynamicObject))
                    {
                        validateContext.AddError(entity, new ValidationErrorInfo("ACID", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("日历类型选择会计日历时，会计日历字段必录。", "0032056000017550", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("生成预算期间", "0032056000017551", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                    }
                    bool flag = false;
                    IList<string> list = new List<string> { "YEAR", "HALFOFYEAR", "SEASON", "MONTH", "TENDAYS", "WEEKS", "DAYS" };
                    foreach (string str2 in list)
                    {
                        if (Convert.ToBoolean(entity[str2]))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        validateContext.AddError(entity, new ValidationErrorInfo("ACID", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("生成预算期间，应当选择至少一个周期类型。", "0032056000017552", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("生成预算期间", "0032056000017551", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                    }
                }
            }
        }
    }
}

