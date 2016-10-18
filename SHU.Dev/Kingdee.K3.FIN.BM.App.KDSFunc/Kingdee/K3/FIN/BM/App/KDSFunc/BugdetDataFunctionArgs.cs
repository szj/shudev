namespace Kingdee.K3.FIN.BM.App.KDSFunc
{
    using Kingdee.BOS;
    using Kingdee.BOS.App;
    using Kingdee.BOS.KDSReportCommon;
    using Kingdee.BOS.KDSReportEntity.Entity;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.Util;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BugdetDataFunctionArgs
    {
        public BudgetDataEntities ConvertToFunctionArgs(Kingdee.BOS.Context ctx, string[] args)
        {
            this.Context = ctx;
            ReportProperty namedSlotData = KdsCalcCacheMananger.GetNamedSlotData("ReportProperty") as ReportProperty;
            bool flag = namedSlotData != null;
            bool flag2 = false;
            if (flag)
            {
                flag2 = namedSlotData.AcctParameter != null;
            }
            BudgetDataEntities funcArgs = new BudgetDataEntities();
            int schemeIdByNumber = this.GetSchemeIdByNumber(args[0]);
            if ((schemeIdByNumber == 0) && flag)
            {
                if (flag && (namedSlotData.AcctParameter.SchemeId > 0))
                {
                    schemeIdByNumber = namedSlotData.AcctParameter.SchemeId;
                }
                else
                {
                    schemeIdByNumber = namedSlotData.SchemeId;
                }
            }
            funcArgs.SchemeId = schemeIdByNumber;
            string str = args[1];
            if (!string.IsNullOrWhiteSpace(str))
            {
                this.GetOrgIdByNumber(str, funcArgs);
            }
            else if (flag && (namedSlotData.AcctParameter.OrgId > 0L))
            {
                funcArgs.OrgId = namedSlotData.AcctParameter.OrgId;
                funcArgs.OrgType = namedSlotData.AcctParameter.OrgType;
            }
            else
            {
                funcArgs.OrgId = namedSlotData.OrgId;
                funcArgs.OrgType = namedSlotData.OrgType;
            }
            string periodTypeByNumber = args[2];
            if (!string.IsNullOrWhiteSpace(periodTypeByNumber))
            {
                periodTypeByNumber = this.GetPeriodTypeByNumber(periodTypeByNumber);
            }
            else if (flag)
            {
                periodTypeByNumber = namedSlotData.CycleType;
            }
            funcArgs.PeriodType = periodTypeByNumber;
            int year = args[3].IsNullOrEmptyOrWhiteSpace() ? 0 : Convert.ToInt32(args[3]);
            int startPeriod = args[4].IsNullOrEmptyOrWhiteSpace() ? 0 : Convert.ToInt32(args[4]);
            int endPeriod = args[5].IsNullOrEmptyOrWhiteSpace() ? 0 : Convert.ToInt32(args[5]);
            if ((year == 0) && flag)
            {
                if (flag)
                {
                    year = namedSlotData.AcctParameter.Year;
                }
                if (year == 0)
                {
                    year = namedSlotData.Year;
                }
            }
            if ((startPeriod == 0) && flag)
            {
                if (flag)
                {
                    startPeriod = namedSlotData.AcctParameter.StartPeriod;
                }
                if (startPeriod == 0)
                {
                    startPeriod = namedSlotData.Period;
                }
            }
            if ((endPeriod == 0) && flag)
            {
                if (flag)
                {
                    endPeriod = namedSlotData.AcctParameter.EndPeriod;
                }
                if (endPeriod == 0)
                {
                    endPeriod = namedSlotData.Period;
                }
            }
            DateTime relativePeriod = this.GetRelativePeriod(ctx);
            switch (year)
            {
                case 0:
                case -1:
                    year = relativePeriod.Year + year;
                    break;
            }
            if ((startPeriod == 0) || (startPeriod == -1))
            {
                startPeriod = relativePeriod.Month + startPeriod;
            }
            if ((endPeriod == 0) || (endPeriod == -1))
            {
                endPeriod = startPeriod;
            }
            funcArgs.Year = year;
            funcArgs.StartPeriod = startPeriod;
            funcArgs.EndPeriod = endPeriod;
            funcArgs.StartYearPeriod = (year * 0x3e8) + startPeriod;
            funcArgs.EndYearPeriod = (year * 0x3e8) + endPeriod;
            string dimissionInfo = args[6];
            funcArgs.DicDimissionFilter = this.GetScopeFilter(ctx, dimissionInfo);
            funcArgs.BusinessType = this.GetBusinessTypeIdByNumber(args[7]);
            int dataTypeIdByNumber = this.GetDataTypeIdByNumber(args[8]);
            if (dataTypeIdByNumber > 0)
            {
                switch (Convert.ToString(this.DicDataTypeInfo[args[8]]["FDATATYPE"]))
                {
                    case "0":
                    case "2":
                        funcArgs.IsAmount = true;
                        break;
                }
            }
            funcArgs.DataType = dataTypeIdByNumber;
            funcArgs.ValueType = args[9].IsNullOrEmptyOrWhiteSpace() ? 0 : Convert.ToInt32(args[9]);
            funcArgs.CurrencyId = this.GetCurrencyIdByNumber(args[10]);
            string amountUnitNumber = args[11].IsNullOrEmptyOrWhiteSpace() ? "JEDW01_SYS" : args[11];
            funcArgs.AmountUnitId = this.GetAmountUnitIdByNumber(amountUnitNumber);
            bool includeUnAuditReport = false;
            if (flag2)
            {
                includeUnAuditReport = namedSlotData.AcctParameter.IncludeUnAuditReport;
            }
            funcArgs.IncludeUnAuditReport = includeUnAuditReport;
            bool includeAuditAdjustData = false;
            if (flag2)
            {
                includeAuditAdjustData = namedSlotData.AcctParameter.IncludeAuditAdjustData;
            }
            funcArgs.IncludeAuditAdjustData = includeAuditAdjustData;
            funcArgs.CalcType = CalcType.Formula;
            return funcArgs;
        }

        public int GetAmountUnitIdByNumber(string amountUnitNumber)
        {
            if (!amountUnitNumber.IsNullOrEmptyOrWhiteSpace() && this.DicAmountUnitInfo.ContainsKey(amountUnitNumber))
            {
                return Convert.ToInt32(this.DicAmountUnitInfo[amountUnitNumber]);
            }
            return 0;
        }

        public int GetBusinessTypeIdByNumber(string businessTypeNumber)
        {
            if (!businessTypeNumber.IsNullOrEmptyOrWhiteSpace() && this.DicBusinessTypeInfo.ContainsKey(businessTypeNumber))
            {
                return this.DicBusinessTypeInfo[businessTypeNumber];
            }
            return 0;
        }

        public int GetCurrencyIdByNumber(string currencyNumber)
        {
            if (!currencyNumber.IsNullOrEmptyOrWhiteSpace() && this.DicCurrencyInfo.ContainsKey(currencyNumber))
            {
                return this.DicCurrencyInfo[currencyNumber];
            }
            return 0;
        }

        public int GetDataTypeIdByNumber(string dataTypeNumber)
        {
            if (!dataTypeNumber.IsNullOrEmptyOrWhiteSpace() && this.DicDataTypeInfo.ContainsKey(dataTypeNumber))
            {
                return Convert.ToInt32(this.DicDataTypeInfo[dataTypeNumber]["FDATATYPEID"]);
            }
            return 0;
        }

        public void GetOrgIdByNumber(string orgInfo, BudgetDataEntities funcArgs)
        {
            if (orgInfo.IsNullOrEmptyOrWhiteSpace())
            {
                funcArgs.OrgId = 0L;
                funcArgs.OrgType = "ORG";
            }
            else
            {
                string[] strArray = orgInfo.Split(new char[] { ':' });
                funcArgs.OrgType = strArray[0];
                string key = "";
                if (strArray.Length > 1)
                {
                    key = strArray[1];
                }
                if (this.DicBugdetOrgInfo.ContainsKey(key))
                {
                    funcArgs.OrgId = this.DicBugdetOrgInfo[key];
                }
                else
                {
                    funcArgs.OrgId = 0L;
                }
            }
        }

        public string GetPeriodTypeByNumber(string periodNumber)
        {
            switch (periodNumber)
            {
                case "YEAR":
                    return "0";

                case "HALFYEAR":
                    return "1";

                case "SEASON":
                    return "2";

                case "MONTH":
                    return "3";

                case "TENDAY":
                    return "4";

                case "WEEK":
                    return "5";

                case "DAY":
                    return "6";
            }
            return "";
        }

        public DateTime GetRelativePeriod(Kingdee.BOS.Context ctx)
        {
            return ServiceHelper.GetService<ITimeService>().GetSystemDateTime(ctx);
        }

        public int GetSchemeIdByNumber(string schemeNumber)
        {
            if (!schemeNumber.IsNullOrEmptyOrWhiteSpace() && this.DicBudgetSchemeInfo.ContainsKey(schemeNumber))
            {
                return this.DicBudgetSchemeInfo[schemeNumber];
            }
            return 0;
        }

        public Dictionary<string, List<string>> GetScopeFilter(Kingdee.BOS.Context ctx, string dimissionInfo)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            if (!string.IsNullOrWhiteSpace(dimissionInfo))
            {
                string[] strArray = dimissionInfo.Split(new char[] { '|' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    string[] strArray2 = strArray[i].Split(new char[] { '~' });
                    string key = strArray2[0];
                    if ((strArray2.Length == 2) && !string.IsNullOrWhiteSpace(strArray2[1]))
                    {
                        List<string> list = new List<string>();
                        foreach (string str2 in strArray2[1].Split(new char[] { ',' }))
                        {
                            if (!string.IsNullOrWhiteSpace(str2))
                            {
                                list.Add(str2);
                            }
                        }
                        if (list.Count > 0)
                        {
                            dictionary.Add(key, list);
                        }
                    }
                }
            }
            return dictionary;
        }

        public static List<string> Validate(BudgetDataEntities funcArgs, Kingdee.BOS.Context ctx)
        {
            List<string> list = new List<string>();
            if (funcArgs.SchemeId == 0)
            {
                list.Add("参数预算方案无效！");
            }
            if (funcArgs.Year <= 0)
            {
                list.Add(ResManager.LoadKDString("参数年度无效！", "003234000011765", SubSystemType.FIN, new object[0]));
            }
            if (funcArgs.StartYearPeriod <= 0)
            {
                list.Add(ResManager.LoadKDString("参数起始期间无效！", "003234000011766", SubSystemType.FIN, new object[0]));
            }
            if (funcArgs.EndYearPeriod <= 0)
            {
                list.Add(ResManager.LoadKDString("参数结束期间无效！", "003234000011767", SubSystemType.FIN, new object[0]));
            }
            return list;
        }

        public static bool ValidateDimissionFilter(Kingdee.BOS.Context ctx)
        {
            new CommonService().GetDimissionGroupInfo(ctx).ToDictionary<DynamicObject, string>(p => Convert.ToString(p["FNUMBER"]));
            return true;
        }

        public Kingdee.BOS.Context Context { get; set; }

        public Dictionary<string, int> DicAmountUnitInfo
        {
            get
            {
                return CommonFunction.GetAmountUnitInfo(this.Context);
            }
        }

        public Dictionary<string, int> DicBudgetSchemeInfo
        {
            get
            {
                return CommonFunction.GetBudgetSchemeInfo(this.Context);
            }
        }

        public Dictionary<string, long> DicBugdetOrgInfo
        {
            get
            {
                return CommonFunction.GetBugdetOrgInfo(this.Context);
            }
        }

        public Dictionary<string, int> DicBusinessTypeInfo
        {
            get
            {
                return CommonFunction.GetBusinessTypeInfo(this.Context);
            }
        }

        public Dictionary<string, int> DicCurrencyInfo
        {
            get
            {
                return CommonFunction.GetCurrencyInfo(this.Context);
            }
        }

        public Dictionary<string, DynamicObject> DicDataTypeInfo
        {
            get
            {
                return CommonFunction.GetDataTypeInfo(this.Context);
            }
        }
    }
}

