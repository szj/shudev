namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.KDSReportEntity.OuterFunc;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;

    public class BudgetFormulaServiceHelper
    {
        public static FunctionElementAdpter GetFunctionElementAdapter(Context ctx, string functionName)
        {
            FunctionElementAdpter functionElementAdapter;
            IBudgetFormulaService service = ServiceFactory.GetService<IBudgetFormulaService>(ctx);
            try
            {
                functionElementAdapter = service.GetFunctionElementAdapter(ctx, functionName);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return functionElementAdapter;
        }
    }
}

