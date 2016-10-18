namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IReportOrgService
    {
        long GetOrgIdByDeptOrgId(Context ctx, long deptOrgId);
    }
}

