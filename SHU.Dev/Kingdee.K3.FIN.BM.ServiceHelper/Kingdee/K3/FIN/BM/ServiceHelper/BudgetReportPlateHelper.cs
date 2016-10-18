namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;

    public class BudgetReportPlateHelper
    {
        public static bool CheckOrgExecutionStatus(Context ctx, BudgetReportEntity budgetReportEntity, IList<BudgetExcuteStatus> statusList)
        {
            bool flag;
            IBudgetReportPlateService service = ServiceFactory.GetService<IBudgetReportPlateService>(ctx);
            try
            {
                flag = service.CheckOrgExecutionStatus(ctx, budgetReportEntity, statusList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static bool CheckReportIsExist(Context ctx, BudgetReportEntity budgetReportEntity)
        {
            bool flag;
            IBudgetReportPlateService service = ServiceFactory.GetService<IBudgetReportPlateService>(ctx);
            try
            {
                flag = service.CheckReportIsExist(ctx, budgetReportEntity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static DynamicObjectCollection GetDistributeMonitorInfo(Context ctx, long orgId, string sampleId)
        {
            DynamicObjectCollection objects;
            IBudgetReportPlateService service = ServiceFactory.GetService<IBudgetReportPlateService>(ctx);
            try
            {
                objects = service.GetDistributeMonitorInfo(ctx, orgId, sampleId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return objects;
        }

        public static IOperationResult UpdateReportStatus(Context ctx, List<int> lstIds, string status)
        {
            IOperationResult result;
            IBudgetReportPlateService service = ServiceFactory.GetService<IBudgetReportPlateService>(ctx);
            try
            {
                result = service.UpdateReportStatus(ctx, lstIds, status);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }
    }
}

