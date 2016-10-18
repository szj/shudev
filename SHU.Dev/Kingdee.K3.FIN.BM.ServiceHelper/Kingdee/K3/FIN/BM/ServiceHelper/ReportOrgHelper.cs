namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;

    public class ReportOrgHelper
    {
        public static long GetOrgIdByDeptOrgId(Context ctx, long deptOrgId)
        {
            long orgIdByDeptOrgId;
            IReportOrgService service = ServiceFactory.GetService<IReportOrgService>(ctx);
            try
            {
                orgIdByDeptOrgId = service.GetOrgIdByDeptOrgId(ctx, deptOrgId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return orgIdByDeptOrgId;
        }
    }
}

