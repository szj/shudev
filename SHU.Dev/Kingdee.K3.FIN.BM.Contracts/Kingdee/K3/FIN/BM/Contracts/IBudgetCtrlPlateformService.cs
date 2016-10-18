namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Rpc;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [RpcServiceError, ServiceContract]
    public interface IBudgetCtrlPlateformService
    {
        bool AddCheck(Context ctx, IList<BudgetCtrlPlateformMain> lstHeaders, IList<BudgetCtrlPlateformEntity> LstDetails, IOperationResult operResult);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IOperationResult BatchAdd(Context ctx, IList<BudgetCtrlPlateformMain> lstHeaders, IList<BudgetCtrlPlateformEntity> LstDetails);
        IDictionary<BudgetCtrlPlateformMain, bool> CheckRepeatForHead(Context ctx, IList<BudgetCtrlPlateformMain> lstTagets);
        DynamicObjectCollection GetCtrlRuleBills(Context ctx, string ctrlRuleId);
        IList<string> GetCtrlRules(Context ctx, string schemeId, string orgId);
    }
}

