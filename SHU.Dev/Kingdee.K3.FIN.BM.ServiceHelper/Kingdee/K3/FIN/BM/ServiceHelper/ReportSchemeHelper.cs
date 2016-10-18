namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;

    public class ReportSchemeHelper
    {
        public static void DeleteReportScheme(Context ctx, long reportSchemeId)
        {
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                service.DeleteReportScheme(ctx, reportSchemeId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
        }

        public static DynamicObjectCollection GetDetailAndData(Context ctx, string sampleId)
        {
            DynamicObjectCollection detailAndData;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                detailAndData = service.GetDetailAndData(ctx, sampleId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return detailAndData;
        }

        public static bool GetDistributeByTempId(Context ctx, string tempId)
        {
            bool distributeByTempId;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                distributeByTempId = service.GetDistributeByTempId(ctx, tempId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return distributeByTempId;
        }

        public static Tuple<bool, int> GetVersionInfo(Context ctx, long reportSchemeId)
        {
            Tuple<bool, int> versionInfo;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                versionInfo = service.GetVersionInfo(ctx, reportSchemeId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return versionInfo;
        }

        public static bool IsReportSchemeUsed(Context ctx, long reportSchemeId)
        {
            bool flag;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                flag = service.IsReportSchemeUsed(ctx, reportSchemeId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static OperateResult RollBackReportSchemeVersion(Context ctx, long versionGroupId, int toVersion)
        {
            OperateResult result;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                result = service.RollBackReportSchemeVersion(ctx, versionGroupId, toVersion);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IOperationResult SaveNewReportSchemeVersion(Context ctx, BusinessInfo bizInfo, DynamicObject changedObj, object editObj)
        {
            IOperationResult result;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                result = service.SaveNewReportSchemeVersion(ctx, bizInfo, changedObj, editObj);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static bool UpdateDistributeTempStatus(Context ctx, string tempId, string tempStatu)
        {
            bool flag;
            IReportSchemeService service = ServiceFactory.GetService<IReportSchemeService>(ctx);
            try
            {
                flag = service.UpdateDistributeTempStatus(ctx, tempId, tempStatu);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }
    }
}

