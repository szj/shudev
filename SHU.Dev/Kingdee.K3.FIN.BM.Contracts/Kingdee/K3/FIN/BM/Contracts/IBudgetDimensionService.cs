namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBudgetDimensionService
    {
        Dictionary<string, string> GenBudgetDimension(Context ctx, List<DynamicDimensionDataGroup> lstDimensionDataGroup, bool isAutoGen);
        Dictionary<string, Dictionary<int, string>> GetDimensionGroup(Context ctx, List<DynamicDimensionDataGroup> lstDimensionDataGroup);
    }
}

