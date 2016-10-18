namespace Kingdee.K3.FIN.BM.App.KDSFunc
{
    using Kingdee.BOS;
    using Kingdee.BOS.App.KDSCaculateService;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BugdetDataFunctionService : KDSFunctionService
    {
        public override DataSet GetBatchFunctionValue(Context ctx, KDSContext kdsContext, object args)
        {
            DataSet set = new DataSet();
            IDictionary<string, BudgetDataEntities> dictionary = args as IDictionary<string, BudgetDataEntities>;
            if ((dictionary != null) && (dictionary.Count != 0))
            {
                return set;
            }
            return set;
        }

        public override object GetFunctionValue(Context ctx, KDSContext kdsContext, string[] args)
        {
            BudgetDataEntities funcArgs = new BugdetDataFunctionArgs().ConvertToFunctionArgs(ctx, args);
            List<string> values = BugdetDataFunctionArgs.Validate(funcArgs, ctx);
            if (values.Count > 0)
            {
                return string.Format("#{0}", string.Join("", values));
            }
            BugdetDataService service = new BugdetDataService();
            return service.GetBugdetValue(ctx, funcArgs);
        }
    }
}

