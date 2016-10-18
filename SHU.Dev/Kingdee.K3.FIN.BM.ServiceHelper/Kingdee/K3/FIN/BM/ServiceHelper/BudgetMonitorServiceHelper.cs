namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;

    public class BudgetMonitorServiceHelper
    {
        public static IList<BudgetExcuteStatus> GetExecuteStatusByFilter(Context ctx, BudgetMonitorFilter monitorFilter)
        {
            IList<BudgetExcuteStatus> executeStatusByFilter;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                executeStatusByFilter = service.GetExecuteStatusByFilter(ctx, monitorFilter);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return executeStatusByFilter;
        }

        public static DynamicObjectCollection GetMonitorSchemeIdListInfo(Context ctx)
        {
            DynamicObjectCollection monitorSchemeIdListInfo;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                monitorSchemeIdListInfo = service.GetMonitorSchemeIdListInfo(ctx);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return monitorSchemeIdListInfo;
        }

        public static SchemeEntityExtend GetSchemBaseInfo(Context ctx, long schemId)
        {
            SchemeEntityExtend schemBaseInfo;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                schemBaseInfo = service.GetSchemBaseInfo(ctx, schemId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return schemBaseInfo;
        }

        public static IList<SchemeMonitorEnify> GetSchemeMonitorBaseInfo(Context ctx, int schemeId)
        {
            IList<SchemeMonitorEnify> schemeMonitorBaseInfo;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                schemeMonitorBaseInfo = service.GetSchemeMonitorBaseInfo(ctx, schemeId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return schemeMonitorBaseInfo;
        }

        public static IList<MonitorEntity> GetSchemMonitorInfo(Context ctx, BudgetMonitorFilter monitorFilter)
        {
            IList<MonitorEntity> schemMonitorInfo;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                schemMonitorInfo = service.GetSchemMonitorInfo(ctx, monitorFilter);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return schemMonitorInfo;
        }

        public static IOperationResult MonitorCloseStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList)
        {
            IOperationResult result;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                result = service.MonitorCloseStatus(ctx, dtoList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IOperationResult MonitorExecuteStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList)
        {
            IOperationResult result;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                result = service.MonitorExecuteStatus(ctx, dtoList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IOperationResult MonitorUnCloseStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList)
        {
            IOperationResult result;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                result = service.MonitorUnCloseStatus(ctx, dtoList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IOperationResult MonitorUnExecuteStatus(Context ctx, IList<MonitorExecuteStatusDto> dtoList)
        {
            IOperationResult result;
            IBudgetMonitorService service = ServiceFactory.GetService<IBudgetMonitorService>(ctx);
            try
            {
                result = service.MonitorUnExecuteStatus(ctx, dtoList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }
    }
}

