namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;

    public class BudgetCtrlPlateformServiceHelper
    {
        public static bool AddCheck(Context ctx, IList<BudgetCtrlPlateformMain> lstHeaders, IList<BudgetCtrlPlateformEntity> LstDetails, IOperationResult operResult)
        {
            bool flag;
            IBudgetCtrlPlateformService service = ServiceFactory.GetService<IBudgetCtrlPlateformService>(ctx);
            try
            {
                flag = service.AddCheck(ctx, lstHeaders, LstDetails, operResult);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static IOperationResult BatchAdd(Context ctx, IList<BudgetCtrlPlateformMain> lstHeaders, IList<BudgetCtrlPlateformEntity> LstDetails)
        {
            IOperationResult result;
            IBudgetCtrlPlateformService service = ServiceFactory.GetService<IBudgetCtrlPlateformService>(ctx);
            try
            {
                result = service.BatchAdd(ctx, lstHeaders, LstDetails);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IDictionary<BudgetCtrlPlateformMain, bool> CheckRepeatForHead(Context ctx, IList<BudgetCtrlPlateformMain> lstTagets)
        {
            IDictionary<BudgetCtrlPlateformMain, bool> dictionary;
            IBudgetCtrlPlateformService service = ServiceFactory.GetService<IBudgetCtrlPlateformService>(ctx);
            try
            {
                dictionary = service.CheckRepeatForHead(ctx, lstTagets);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static IList<string> GetCtrlRules(Context ctx, string schemeId, string orgId)
        {
            IList<string> list;
            IBudgetCtrlPlateformService service = ServiceFactory.GetService<IBudgetCtrlPlateformService>(ctx);
            try
            {
                list = service.GetCtrlRules(ctx, schemeId, orgId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return list;
        }
    }
}

