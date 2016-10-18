namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBudgetOrgEditService
    {
        int GetDefaultBudgetOrgFId(Context ctx);
        bool GetIsDeptHasReport(Context ctx, int deptId, int budgetOrgFID);
        bool GetOrgBudgetState(Context ctx, int deptOrgId);
    }
}

