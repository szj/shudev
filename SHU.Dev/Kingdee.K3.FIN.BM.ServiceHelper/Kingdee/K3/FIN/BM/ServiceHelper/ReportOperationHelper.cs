namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;

    public class ReportOperationHelper
    {
        public static ReportDataEntity Load(Context ctx, Guid reportId, int reportType)
        {
            ReportDataEntity entity;
            IReportOperationService service = ServiceFactory.GetService<IReportOperationService>(ctx);
            try
            {
                entity = service.Load(ctx, reportId, reportType);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return entity;
        }

        public static IOperationResult ModifyReportStatus(Context ctx, string rptType, string reportId, string status)
        {
            IOperationResult result;
            IReportOperationService service = ServiceFactory.GetService<IReportOperationService>(ctx);
            try
            {
                result = service.ModifyReportStatus(ctx, rptType, reportId, status);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IOperationResult Save(Context ctx, ReportDataEntity entity)
        {
            IOperationResult result;
            IReportOperationService service = ServiceFactory.GetService<IReportOperationService>(ctx);
            try
            {
                result = service.Save(ctx, entity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }
    }
}

