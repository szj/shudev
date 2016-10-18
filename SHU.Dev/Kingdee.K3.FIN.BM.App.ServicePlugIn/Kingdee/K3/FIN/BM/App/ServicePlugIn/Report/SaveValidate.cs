namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
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

    public class SaveValidate : AbstractValidator
    {
        private bool CheckIsExistReport(BudgetReportEntity budgetReportEntity)
        {
            return new BudgetReportPlateService().CheckReportIsExist(base.Context, budgetReportEntity);
        }

        private bool CheckOrgExecutionStatus(BudgetReportEntity budgetReportEntity)
        {
            List<BudgetExcuteStatus> statusList = new List<BudgetExcuteStatus> { 3, 5 };
            return new BudgetReportPlateService().CheckOrgExecutionStatus(base.Context, budgetReportEntity, statusList);
        }

        private BudgetReportEntity GetReportEntityByExtendedDate(ExtendedDataEntity entity)
        {
            BudgetReportEntity entity2 = new BudgetReportEntity {
                SchemeID = Convert.ToInt32(entity.DataEntity["SchemeID_Id"]),
                SampleID = entity["SampleID_Id"].ToString(),
                AmountunitID = Convert.ToInt32(entity.DataEntity["AmountUnitID_Id"]),
                CurrencyID = Convert.ToInt32(entity.DataEntity["CurrencyID_Id"]),
                DeptOrgID = Convert.ToInt64(entity.DataEntity["DeptOrgID_Id"])
            };
            DynamicObject obj2 = entity.DataEntity["DeptOrgID"] as DynamicObject;
            entity2.DeptOrgName = (obj2 == null) ? string.Empty : obj2["Name"].ToString();
            entity2.Year = Convert.ToInt32(entity.DataEntity["Year"]);
            entity2.Period = Convert.ToInt32(entity.DataEntity["Period"]);
            entity2.CycleID = entity.DataEntity["CycleID"].ToString();
            entity2.ID = entity.DataEntity["Id"].ToString();
            return entity2;
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                if (Convert.ToString(entity["FRptType"]) != "62")
                {
                    BudgetReportEntity reportEntityByExtendedDate = this.GetReportEntityByExtendedDate(entity);
                    string message = string.Empty;
                    string title = string.Empty;
                    if (this.CheckOrgExecutionStatus(reportEntityByExtendedDate))
                    {
                        message = string.Format(ResManager.LoadKDString("当前期间预算组织【{0}】处于执行或关闭状态，不能生成！", "0032056000020554", SubSystemType.FIN, new object[0]), reportEntityByExtendedDate.DeptOrgName);
                        title = ResManager.LoadKDString("保存：", "0032056000020555", SubSystemType.FIN, new object[0]) + ResManager.LoadKDString("当前期间组织处于执行或关闭状态", "0032056000020556", SubSystemType.FIN, new object[0]);
                        validateContext.AddError(entity, new ValidationErrorInfo("", "SchemeID_Id", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), message, title, ErrorLevel.Error));
                    }
                    else if (this.CheckIsExistReport(reportEntityByExtendedDate))
                    {
                        message = ResManager.LoadKDString("预算报表已经存在，不能重复生成！", "0032056000020511", SubSystemType.FIN, new object[0]);
                        title = ResManager.LoadKDString("保存：", "0032056000020555", SubSystemType.FIN, new object[0]) + ResManager.LoadKDString("预算报表已经存在", "0032056000020557", SubSystemType.FIN, new object[0]);
                        validateContext.AddError(entity, new ValidationErrorInfo("", "SchemeID_Id", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), message, title, ErrorLevel.Error));
                    }
                }
            }
        }
    }
}

