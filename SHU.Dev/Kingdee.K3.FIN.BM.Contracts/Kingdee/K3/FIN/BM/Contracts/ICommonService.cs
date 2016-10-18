namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.ServiceModel;

    [ServiceContract]
    public interface ICommonService
    {
        IList<string> CtrlRuleTest(Context ctx, DynamicObject ctrlrule, BusinessInfo businessInfo, DynamicObject dataObject);
        DynamicObjectCollection GetBillLastinfo(Context ctx, string billid);
        string GetBudgeCalYearPeriodByScheme(Context ctx, int schemeId, DateTime dtBudge);
        DateTime GetBudgeStartDateBySchemeYearPeriod(Context ctx, int schemeId, int year, int period, string periodType);
        List<CustomFuncData> GetBudgetFuncDatas(Context ctx);
        List<int> GetBudgetOrgIdBySchemeId(Context ctx, int schemeId);
        List<int> GetBudgetOrgIdBySchemeIdAndRuleId(Context ctx, int schemeId, int ruleId);
        TreeNode GetBudgetOrgTreeNode(Context ctx, int budgetOrgId = 0);
        TreeNode GetBudgetOrgTreeNodeContainAuthority(Context ctx, List<long> lstOrgIds, long budgetOrgId = 0L);
        DynamicObjectCollection GetBusinessTypeByMonitor(Context ctx, SchemeMonitorEnify monitor);
        DynamicObjectCollection GetBusinessTypeByReportID(Context ctx, string reportID, bool isSample);
        DynamicObjectCollection GetCtrlData(Context ctx, string schemeName);
        DynamicObjectCollection GetCtrlDetail(Context ctx, string schemeName);
        DynamicObject[] GetCtrlDetail(Context ctx, string formid, string key);
        DynamicObjectCollection GetCurrencyCollection(Context ctx);
        int GetCurrencyIdDyDeptOrgID(Context ctx, long deptOrgID, int fid);
        int GetDemoVersionBillCount(Context ctx, string formID);
        DynamicObjectCollection GetDetailAndData(Context ctx, string schemeName);
        List<int> GetMainOrgUseDefAcctPLCYByCurrencyId(Context ctx, int currencyId);
        int GetOrgFidByID(Context ctx, int orgId);
        DynamicObject GetParentOrgDyObject(Context ctx, long orgID, int fid = -1);
        List<EnumItem> GetSchemeInfo(Context ctx);
        List<int> GetSubnOrgUseDefAcctPLCYByCurrencyId(Context ctx, int currencyId);
        DynamicObject LoadFormData(Context ctx, string formid, string pid);
    }
}

