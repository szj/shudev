namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface ICaculateService
    {
        SheetDataEntity AutoAdjustCalculate(Context ctx, BMReportProperty reportProperty, SheetDataEntity sheetDataEntity);
        string CalculateFormula(Context ctx, BMReportProperty reportProperty, string formulaStr);
        ReportDataEntity CalculateReport(Context ctx, ReportDataEntity reportDataEntity);
        SheetDataEntity CalculateSheet(Context ctx, BMReportProperty reportProperty, SheetDataEntity sheetDataEntity);
    }
}

