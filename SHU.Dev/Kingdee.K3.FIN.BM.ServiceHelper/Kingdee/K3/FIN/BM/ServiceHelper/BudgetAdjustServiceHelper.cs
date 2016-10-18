namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;

    public class BudgetAdjustServiceHelper
    {
        public static bool AutoCreateAdjust(Context ctx, BudgetAdjustModel bam)
        {
            bool flag;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                flag = service.AutoCreateAdjust(ctx, bam);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static int CheckReportOrg(Context ctx, string reportId)
        {
            int num;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                num = service.CheckReportOrg(ctx, reportId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return num;
        }

        public DynamicObjectCollection ExecSql(Context ctx, string sql)
        {
            DynamicObjectCollection objects;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                objects = service.ExecSql(ctx, sql);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return objects;
        }

        public static decimal GetBudgetData(Context ctx, BudgetDataEntities entity)
        {
            decimal budgetData;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                budgetData = service.GetBudgetData(ctx, entity);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return budgetData;
        }

        public static Dictionary<string, string> GetBudgetOrgList(Context ctx, string reportId)
        {
            Dictionary<string, string> budgetOrgList;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                budgetOrgList = service.GetBudgetOrgList(ctx, reportId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return budgetOrgList;
        }

        public static string GetSheetBySampleIDSchemeID(Context ctx, string sampleID, string schemeID)
        {
            string str;
            IBudgetAdjust service = ServiceFactory.GetService<IBudgetAdjust>(ctx);
            try
            {
                str = service.GetSheetBySampleIDSchemeID(ctx, sampleID, schemeID);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return str;
        }
    }
}

