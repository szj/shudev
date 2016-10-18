namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBudgetReportPlateService
    {
        [OperationContract]
        bool CheckOrgExecutionStatus(Context ctx, BudgetReportEntity budgetReportEntity, IList<BudgetExcuteStatus> statusList);
        [OperationContract]
        bool CheckReportIsExist(Context ctx, BudgetReportEntity budgetReportEntity);
        [OperationContract]
        DynamicObjectCollection GetDistributeMonitorInfo(Context ctx, long orgId, string sampleId);
        [OperationContract]
        IOperationResult UpdateReportStatus(Context ctx, List<int> lstIds, string status);
    }
}

