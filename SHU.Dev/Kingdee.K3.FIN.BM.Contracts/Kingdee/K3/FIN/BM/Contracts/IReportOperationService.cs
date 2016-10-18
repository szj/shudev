namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Rpc;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid;
    using System;
    using System.ServiceModel;

    [ServiceContract, RpcServiceError]
    public interface IReportOperationService
    {
        [FaultContract(typeof(ServiceFault)), OperationContract]
        ReportDataEntity Load(Context ctx, Guid reportId, int reportType);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IOperationResult ModifyReportStatus(Context ctx, string rptType, string reportId, string status);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IOperationResult Save(Context ctx, ReportDataEntity entity);
    }
}

