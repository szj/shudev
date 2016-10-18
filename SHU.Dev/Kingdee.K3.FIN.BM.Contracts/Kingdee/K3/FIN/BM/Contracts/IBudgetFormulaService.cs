namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.KDSReportEntity.OuterFunc;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBudgetFormulaService
    {
        [OperationContract]
        FunctionElementAdpter GetFunctionElementAdapter(Context ctx, string functionName);
    }
}

