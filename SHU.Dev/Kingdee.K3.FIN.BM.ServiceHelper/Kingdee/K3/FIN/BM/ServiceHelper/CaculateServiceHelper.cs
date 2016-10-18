namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid;
    using Kingdee.K3.FIN.BM.Contracts;
    using Kingdee.K3.FIN.ReportEntity;
    using System;

    public class CaculateServiceHelper
    {
        public static SheetDataEntity AutoAdjustCalculate(Context ctx, BMReportProperty reportProperty, SheetDataEntity sheetDataEntity)
        {
            SheetDataEntity entity;
            ICaculateService service = ServiceFactory.GetService<ICaculateService>(ctx);
            try
            {
                entity = service.AutoAdjustCalculate(ctx, reportProperty, sheetDataEntity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return entity;
        }

        public static string CalculateFormula(Context ctx, BMReportProperty reportProperty, string formulaStr)
        {
            string str;
            ICaculateService service = ServiceFactory.GetService<ICaculateService>(ctx);
            try
            {
                str = service.CalculateFormula(ctx, reportProperty, formulaStr);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return str;
        }

        public static ReportDataEntity CalculateReport(Context ctx, ReportDataEntity reportDataEntity)
        {
            ReportDataEntity entity;
            ICaculateService service = ServiceFactory.GetService<ICaculateService>(ctx);
            try
            {
                entity = service.CalculateReport(ctx, reportDataEntity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return entity;
        }

        public static SheetDataEntity CalculateSheet(Context ctx, BMReportProperty reportProperty, SheetDataEntity sheetDataEntity)
        {
            SheetDataEntity entity;
            ICaculateService service = ServiceFactory.GetService<ICaculateService>(ctx);
            try
            {
                entity = service.CalculateSheet(ctx, reportProperty, sheetDataEntity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return entity;
        }
    }
}

