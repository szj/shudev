namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Contracts;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.SqlBuilder;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.BM.Common.Core;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class BMCommonServiceHelper
    {
        public static IList<string> CtrlRuleTest(Context ctx, DynamicObject ctrlrule, BusinessInfo businessInfo, DynamicObject dataObject)
        {
            IList<string> list;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                list = service.CtrlRuleTest(ctx, ctrlrule, businessInfo, dataObject);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return list;
        }

        public static string GetBudgeCalYearPeriodByScheme(Context ctx, int schemeId, DateTime dtBudge)
        {
            string str;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                str = service.GetBudgeCalYearPeriodByScheme(ctx, schemeId, dtBudge);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return str;
        }

        public static DateTime GetBudgeStartDateBySchemeYearPeriod(Context ctx, int schemeId, int year, int period, string periodType)
        {
            DateTime time;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                time = service.GetBudgeStartDateBySchemeYearPeriod(ctx, schemeId, year, period, periodType);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return time;
        }

        public static List<CustomFuncData> GetBudgetFuncDatas(Context ctx)
        {
            List<CustomFuncData> budgetFuncDatas;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                budgetFuncDatas = service.GetBudgetFuncDatas(ctx);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return budgetFuncDatas;
        }

        public static List<int> GetBudgetOrgIdBySchemeId(Context ctx, int schemeId)
        {
            List<int> budgetOrgIdBySchemeId;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                budgetOrgIdBySchemeId = service.GetBudgetOrgIdBySchemeId(ctx, schemeId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return budgetOrgIdBySchemeId;
        }

        public static List<int> GetBudgetOrgIdBySchemeIdAndRuleId(Context ctx, int schemeId, int ruleId)
        {
            List<int> list;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                list = service.GetBudgetOrgIdBySchemeIdAndRuleId(ctx, schemeId, ruleId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return list;
        }

        public static TreeNode GetBudgetOrgTreeNode(Context ctx, int budgetOrgId = 0)
        {
            TreeNode budgetOrgTreeNode;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                budgetOrgTreeNode = service.GetBudgetOrgTreeNode(ctx, budgetOrgId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return budgetOrgTreeNode;
        }

        public static TreeNode GetBudgetOrgTreeNodeContainAuthority(Context ctx, List<long> lstOrgIds, long budgetOrgId = 0L)
        {
            TreeNode node;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                node = service.GetBudgetOrgTreeNodeContainAuthority(ctx, lstOrgIds, budgetOrgId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return node;
        }

        public static DynamicObjectCollection GetBusinessTypeByMonitor(Context ctx, SchemeMonitorEnify monitor)
        {
            DynamicObjectCollection businessTypeByMonitor;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                businessTypeByMonitor = service.GetBusinessTypeByMonitor(ctx, monitor);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return businessTypeByMonitor;
        }

        public static DynamicObjectCollection GetBusinessTypeByReportID(Context ctx, string reportID, bool isSample)
        {
            DynamicObjectCollection objects;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                objects = service.GetBusinessTypeByReportID(ctx, reportID, isSample);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return objects;
        }

        public static DynamicObjectCollection GetCtrlData(Context ctx, string schemeName)
        {
            DynamicObjectCollection ctrlData;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                ctrlData = service.GetCtrlData(ctx, schemeName);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return ctrlData;
        }

        public static DynamicObjectCollection GetCtrlDetail(Context ctx, string schemeName)
        {
            DynamicObjectCollection ctrlDetail;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                ctrlDetail = service.GetCtrlDetail(ctx, schemeName);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return ctrlDetail;
        }

        public static DynamicObject[] GetCtrlDetail(Context ctx, string formid, string key)
        {
            DynamicObject[] objArray;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                objArray = service.GetCtrlDetail(ctx, formid, key);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return objArray;
        }

        public static DynamicObjectCollection GetCurrencyCollection(Context ctx)
        {
            DynamicObjectCollection currencyCollection;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                currencyCollection = service.GetCurrencyCollection(ctx);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return currencyCollection;
        }

        public static int GetCurrencyIdDyDeptOrgID(Context ctx, long deptOrgID, int fid = 0)
        {
            int num;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                num = service.GetCurrencyIdDyDeptOrgID(ctx, deptOrgID, fid);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return num;
        }

        public static int GetDemoVersionBillCount(Context ctx, string formID)
        {
            int demoVersionBillCount;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                demoVersionBillCount = service.GetDemoVersionBillCount(ctx, formID);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return demoVersionBillCount;
        }

        public static DynamicObjectCollection GetDetailAndData(Context ctx, string schemeName)
        {
            DynamicObjectCollection detailAndData;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                detailAndData = service.GetDetailAndData(ctx, schemeName);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return detailAndData;
        }

        public static string GetLanByCode(Context ctx, string code)
        {
            return CommonHelper.GetLanByCode(code);
        }

        public static List<int> GetMainOrgUseDefAcctPLCYByCurrencyId(Context ctx, int currencyId)
        {
            List<int> mainOrgUseDefAcctPLCYByCurrencyId;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                mainOrgUseDefAcctPLCYByCurrencyId = service.GetMainOrgUseDefAcctPLCYByCurrencyId(ctx, currencyId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return mainOrgUseDefAcctPLCYByCurrencyId;
        }

        public static int GetOrgFidByID(Context ctx, int orgId)
        {
            int orgFidByID;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                orgFidByID = service.GetOrgFidByID(ctx, orgId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return orgFidByID;
        }

        public static DynamicObject GetParentOrgDyObject(Context ctx, long orgID, int fid = -1)
        {
            DynamicObject obj2;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                obj2 = service.GetParentOrgDyObject(ctx, orgID, fid);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return obj2;
        }

        public static List<EnumItem> GetSchemeInfo(Context ctx)
        {
            List<EnumItem> schemeInfo;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                schemeInfo = service.GetSchemeInfo(ctx);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return schemeInfo;
        }

        public static List<int> GetSubnOrgUseDefAcctPLCYByCurrencyId(Context ctx, int currencyId)
        {
            List<int> subnOrgUseDefAcctPLCYByCurrencyId;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                subnOrgUseDefAcctPLCYByCurrencyId = service.GetSubnOrgUseDefAcctPLCYByCurrencyId(ctx, currencyId);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return subnOrgUseDefAcctPLCYByCurrencyId;
        }

        public static DynamicObject LoadFormData(Context ctx, string formid, string pid)
        {
            DynamicObject obj2;
            ICommonService service = Kingdee.K3.FIN.BM.Contracts.ServiceFactory.GetService<ICommonService>(ctx);
            try
            {
                obj2 = service.LoadFormData(ctx, formid, pid);
            }
            finally
            {
                Kingdee.K3.FIN.BM.Contracts.ServiceFactory.CloseService(service);
            }
            return obj2;
        }

        public static DynamicObjectCollection QueryData(Context ctx, string formId, string strSelect, string strFilter)
        {
            DynamicObjectCollection objects;
            QueryBuilderParemeter paremeter2 = new QueryBuilderParemeter {
                FormId = formId,
                SelectItems = SelectorItemInfo.CreateItems(strSelect),
                FilterClauseWihtKey = strFilter
            };
            QueryBuilderParemeter para = paremeter2;
            IQueryService service = Kingdee.BOS.Contracts.ServiceFactory.GetService<IQueryService>(ctx);
            try
            {
                objects = service.GetDynamicObjectCollection(ctx, para, null);
            }
            finally
            {
                Kingdee.BOS.Contracts.ServiceFactory.CloseService(service);
            }
            return objects;
        }
    }
}

