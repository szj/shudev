namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.ReportEntity;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Collections.Generic;

    public interface IBudgetAdjust
    {
        bool AutoCreateAdjust(Context ctx, BudgetAdjustModel bam);
        int CheckReportOrg(Context ctx, string reportId);
        DynamicObjectCollection ExecSql(Context ctx, string sql);
        decimal GetBudgetData(Context ctx, BudgetDataEntities entity);
        decimal GetBudgetData(Context ctx, PivotGridCellValue cell);
        Dictionary<string, string> GetBudgetOrgList(Context ctx, string reportId);
        string GetSheetBySampleIDSchemeID(Context ctx, string sampleID, string schemeID);
        int UpdateAdjustData(Context ctx, PivotGridCellValue cell);
    }
}

