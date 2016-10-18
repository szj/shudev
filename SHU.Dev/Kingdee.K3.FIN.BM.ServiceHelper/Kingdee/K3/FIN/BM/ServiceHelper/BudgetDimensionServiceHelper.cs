namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;

    public class BudgetDimensionServiceHelper
    {
        public static Dictionary<string, string> GenBudgetDimension(Context ctx, List<DynamicDimensionDataGroup> lstDimensionDataGroup, bool isAutoGen)
        {
            Dictionary<string, string> dictionary;
            IBudgetDimensionService service = ServiceFactory.GetService<IBudgetDimensionService>(ctx);
            try
            {
                dictionary = service.GenBudgetDimension(ctx, lstDimensionDataGroup, isAutoGen);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static Dictionary<string, Dictionary<int, string>> GetDimensionGroup(Context ctx, List<DynamicDimensionDataGroup> lstDimensionDataGroup)
        {
            Dictionary<string, Dictionary<int, string>> dimensionGroup;
            IBudgetDimensionService service = ServiceFactory.GetService<IBudgetDimensionService>(ctx);
            try
            {
                dimensionGroup = service.GetDimensionGroup(ctx, lstDimensionDataGroup);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dimensionGroup;
        }
    }
}

