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
    public interface IBudgetMonitorService
    {
        [OperationContract]
        IList<BudgetExcuteStatus> GetExecuteStatusByFilter(Context ctx, BudgetMonitorFilter monitorFilter);
        [OperationContract]
        DynamicObjectCollection GetMonitorSchemeIdListInfo(Context ctx);
        [OperationContract]
        SchemeEntityExtend GetSchemBaseInfo(Context ctx, long schemId);
        [OperationContract]
        IList<SchemeMonitorEnify> GetSchemeMonitorBaseInfo(Context ctx, int schemeId);
        [OperationContract]
        IList<MonitorEntity> GetSchemMonitorInfo(Context ctx, BudgetMonitorFilter monitorFilter);
        [OperationContract]
        IOperationResult MonitorCloseStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList);
        [OperationContract]
        IOperationResult MonitorExecuteStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList);
        [OperationContract]
        IOperationResult MonitorUnCloseStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList);
        [OperationContract]
        IOperationResult MonitorUnExecuteStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList);
    }
}

