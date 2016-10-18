namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;

    public class BudgetOrgHelper
    {
        public static int GetDefaultBudgetOrgFId(Context ctx)
        {
            int defaultBudgetOrgFId;
            IBudgetOrgEditService service = ServiceFactory.GetService<IBudgetOrgEditService>(ctx);
            try
            {
                defaultBudgetOrgFId = service.GetDefaultBudgetOrgFId(ctx);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return defaultBudgetOrgFId;
        }

        public static bool GetIsDeptHasReport(Context ctx, int deptId, int budgetOrgFID)
        {
            bool flag;
            IBudgetOrgEditService service = ServiceFactory.GetService<IBudgetOrgEditService>(ctx);
            try
            {
                flag = service.GetIsDeptHasReport(ctx, deptId, budgetOrgFID);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }
    }
}

