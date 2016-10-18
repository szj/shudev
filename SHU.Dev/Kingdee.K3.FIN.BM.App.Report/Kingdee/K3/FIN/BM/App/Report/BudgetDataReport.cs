namespace Kingdee.K3.FIN.BM.App.Report
{
    using Kingdee.BOS;
    using Kingdee.BOS.App;
    using Kingdee.BOS.App.Data;
    using Kingdee.BOS.Contracts;
    using Kingdee.BOS.Contracts.Report;
    using Kingdee.BOS.Core.List;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Metadata.FieldElement;
    using Kingdee.BOS.Core.Report;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    [Description("预算数据查询服务端插件")]
    public class BudgetDataReport : SysReportBaseService
    {
        private BudgetBillUsedService billUsedService = new BudgetBillUsedService();
        private CommonService commonService = new CommonService();
        private Dictionary<string, string> dicDimensionField = new Dictionary<string, string>();
        private Dictionary<int, DimensionInfo> dicDimensionInfo = new Dictionary<int, DimensionInfo>();
        private Dictionary<string, BusinessInfo> dicDimensionMetaData = new Dictionary<string, BusinessInfo>();
        private DynamicObject dyFilter;
        private DynamicObject dyRptScheme;
        private DynamicObject dyScheme;
        private BudgetFilterParameter filterParameter;
        private int localeId = 0x804;
        private List<BudgetOrgDept> lstOrgDeptInfo;
        private List<string> lstSelectField = new List<string> { "FDEPTORGID", "FORGTYPEID", "FYEAR", "FPERIOD" };
        private long operationId;
        private int precision = 2;
        private int showDimensionType;
        private const string TM_BM_BUDGETVALUE_FIELD = "(\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FYEAR INT DEFAULT 0 NULL,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0 ,\r\n                                                    FBUSINESSTYPEID INT DEFAULT 0  ,\r\n                                                    FDATATYPEID INT DEFAULT 0  ,\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' ,\r\n                                                    FCURRENCYID INT DEFAULT 0,\r\n                                                    FBWBCURRENCYID INT DEFAULT 0 NULL,\r\n                                                    FISADJUST CHAR(1) DEFAULT '0' NOT NULL,\r\n                                                    FADJUSTNUM INT DEFAULT 0 NOT NULL,\r\n                                                    FVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FBASEVALUE DECIMAL(23,10) DEFAULT 0  NOT NULL,\r\n                                                    FBUDGETVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FADJUSTVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FFINALVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL\r\n                                                    )";
        private const string TM_BM_DIMENSION_FIELD = "(\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME1 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME2 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME3 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME4 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME5 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME6 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME7 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME8 NVARCHAR(2000) DEFAULT ' ' NULL\r\n                                                    )";

        public string BudgetDimensionConvert(string budgetValueTableName)
        {
            string str = DBUtils.CreateSessionTemplateTable(base.Context, "TM_BM_DIMENSION", "(\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME1 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME2 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME3 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME4 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME5 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME6 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME7 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME8 NVARCHAR(2000) DEFAULT ' ' NULL\r\n                                                    )");
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("INSERT INTO {0}(FGROUPID) SELECT DISTINCT FGROUPID FROM {1}", str, budgetValueTableName);
            DBUtils.Execute(base.Context, builder.ToString());
            foreach (KeyValuePair<int, DimensionInfo> pair in this.dicDimensionInfo)
            {
                if (pair.Value.IsShowField)
                {
                    builder.Clear();
                    builder.AppendFormat("/*dialect*/ Merge Into {0} A ", str);
                    builder.AppendLine(" Using (SELECT B.FGROUPID,B.FDIMENSIONID AS FID ");
                    if (pair.Value.TableName == pair.Value.NameTableName)
                    {
                        builder.AppendFormat(",C.{0} AS FNAME ", pair.Value.TableName);
                    }
                    else
                    {
                        builder.AppendFormat(",D.{0} AS FNAME ", pair.Value.NameFieldName);
                    }
                    builder.AppendFormat(" FROM T_BM_DIMENSIONDETAIL B ", new object[0]);
                    builder.AppendFormat(" JOIN {0} C on B.FDIMENSIONID=C.{1} ", pair.Value.TableName, pair.Value.PKFieldName);
                    if (pair.Value.TableName != pair.Value.NameTableName)
                    {
                        builder.AppendFormat("  LEFT JOIN {0} D on D.{1} = C.{1} and d.FLOCALEID={2} ", pair.Value.NameTableName, pair.Value.PKFieldName, base.Context.UserLocale.LCID);
                    }
                    builder.AppendFormat(" WHERE B.FDIMENSIONTYPE={0}) T", pair.Value.Id);
                    builder.AppendLine("  ON (T.FGROUPID=A.FGROUPID) WHEN MATCHED THEN UPDATE SET  ");
                    builder.AppendFormat(" A.{0}=T.FID,A.{1} =T.FNAME", pair.Value.ShowFieldId, pair.Value.ShowFieldName);
                    if (base.Context.DatabaseType == DatabaseType.MS_SQL_Server)
                    {
                        builder.AppendLine(";");
                    }
                    DBUtils.Execute(base.Context, builder.ToString());
                }
            }
            return str;
        }

        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            this.dyFilter = filter.FilterParameter.CustomFilter;
            this.GetReportFilter();
            this.GetReportTempData(filter, tableName);
        }

        public void GetBudgetDimensionInfo()
        {
            DynamicObjectCollection objects = this.dyRptScheme["DimensionEntity"] as DynamicObjectCollection;
            this.dicDimensionInfo.Clear();
            this.dicDimensionField.Clear();
            int num = 1;
            foreach (DynamicObject obj2 in objects)
            {
                DynamicObject obj3 = obj2["DimensionID"] as DynamicObject;
                if (((obj3 != null) && (Convert.ToString(obj3["BaseDataType_Id"]) != "BM_BUDGETCALENDAR")) && (Convert.ToString(obj3["BaseDataType_Id"]) != "BM_DEPTORG"))
                {
                    string str = Convert.ToString(obj3["Category"]);
                    string formId = Convert.ToString(obj3["BaseDataType_Id"]);
                    int key = Convert.ToInt32(obj3["Id"]);
                    DimensionInfo info = new DimensionInfo {
                        Id = key,
                        Name = Convert.ToString(obj3["Name"]),
                        Category = str,
                        DataControlType = 1
                    };
                    if (str == "10")
                    {
                        formId = "BOS_ASSISTANTDATA_SELECT";
                        info.PKFieldName = "FEntryID";
                        info.TableName = "T_BAS_ASSISTANTDATAENTRY";
                        info.NumberFieldName = "FNUMBER";
                        info.NameFieldName = "FDATAVALUE";
                        info.NameTableName = "T_BAS_ASSISTANTDATAENTRY_L";
                    }
                    else
                    {
                        info.FormId = formId;
                        BusinessInfo demensionMetaData = this.GetDemensionMetaData(formId);
                        info.PKFieldName = demensionMetaData.GetForm().PkFieldName;
                        string nameFieldKey = demensionMetaData.GetForm().NameFieldKey;
                        if (!string.IsNullOrWhiteSpace(nameFieldKey))
                        {
                            info.NameFieldName = nameFieldKey;
                            if (demensionMetaData.GetField(nameFieldKey) is MultiLangTextField)
                            {
                                info.NameTableName = demensionMetaData.GetField(nameFieldKey).TableName + "_L";
                            }
                            else
                            {
                                info.NameTableName = demensionMetaData.GetField(nameFieldKey).TableName;
                            }
                        }
                        info.NumberFieldName = demensionMetaData.GetForm().NumberFieldKey;
                        info.TableName = demensionMetaData.GetField(demensionMetaData.GetForm().NumberFieldKey).TableName;
                        int baseDataType = ServiceHelper.GetService<IOrganizationService>().GetBaseDataType(base.Context, formId);
                        if (baseDataType == 2)
                        {
                            string masterPKFieldName = demensionMetaData.GetForm().MasterPKFieldName;
                            if (!string.IsNullOrWhiteSpace(masterPKFieldName))
                            {
                                info.DataControlType = baseDataType;
                                info.MasterIDFieldName = masterPKFieldName;
                            }
                        }
                    }
                    if (this.filterParameter.DicDimissionFilter.ContainsKey(key))
                    {
                        info.IsShowField = true;
                        string str5 = string.Format("FDIMENSIONID{0}", num);
                        string str6 = string.Format("FDIMENSIONNAME{0}", num);
                        info.ShowFieldId = str5;
                        info.ShowFieldName = str6;
                        this.dicDimensionField.Add(str5, str6);
                        num++;
                    }
                    else
                    {
                        info.IsShowField = false;
                    }
                    this.dicDimensionInfo.Add(key, info);
                }
            }
        }

        public Dictionary<long, string> GetBudgetOrgByEntryID(List<int> lstBugdetOrgId)
        {
            this.lstOrgDeptInfo = new List<BudgetOrgDept>();
            Dictionary<long, string> dictionary = new Dictionary<long, string>();
            if (lstBugdetOrgId.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT FORGID,FORGTYPE,FPARENTORGID FROM T_BM_BUDGETORGENTRY ");
                builder.AppendFormat(" WHERE FENTRYID IN ({0}) ", string.Join<int>(",", lstBugdetOrgId));
                using (IDataReader reader = DBUtils.ExecuteReader(base.Context, builder.ToString()))
                {
                    while (reader.Read())
                    {
                        long num = Convert.ToInt64(reader["FORGID"]);
                        string str = Convert.ToString(reader["FORGTYPE"]);
                        BudgetOrgDept item = new BudgetOrgDept();
                        if (str.ToUpperInvariant() == "ORG")
                        {
                            item.OrgInfo.Id = Convert.ToInt32(num);
                        }
                        else if (str.ToUpperInvariant() == "DEPT")
                        {
                            item.OrgInfo.Id = Convert.ToInt32(reader["FPARENTORGID"]);
                            item.DeptInfo.Id = Convert.ToInt32(num);
                        }
                        this.lstOrgDeptInfo.Add(item);
                        dictionary.Add(num, str);
                    }
                    reader.Close();
                }
            }
            return dictionary;
        }

        public Dictionary<long, string> GetBudgetOrgIDs()
        {
            DynamicObjectCollection objects = this.dyFilter["OrgIds"] as DynamicObjectCollection;
            List<int> lstBugdetOrgId = new List<int>();
            foreach (DynamicObject obj2 in objects)
            {
                lstBugdetOrgId.Add(Convert.ToInt32(obj2["OrgIds_Id"]));
            }
            return this.GetBudgetOrgByEntryID(lstBugdetOrgId);
        }

        public BusinessInfo GetDemensionMetaData(string formId)
        {
            BusinessInfo businessInfo;
            IMetaDataService service = ServiceHelper.GetService<IMetaDataService>();
            if (!this.dicDimensionMetaData.TryGetValue(formId, out businessInfo))
            {
                FormMetadata metadata = service.Load(base.Context, formId, true) as FormMetadata;
                businessInfo = metadata.BusinessInfo;
                this.dicDimensionMetaData.Add(formId, businessInfo);
            }
            return businessInfo;
        }

        public Dictionary<int, List<string>> GetDimensionFilter()
        {
            Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
            DynamicObjectCollection objects = this.dyFilter["CtrlDimensionEntry"] as DynamicObjectCollection;
            foreach (DynamicObject obj2 in objects)
            {
                List<string> list = new List<string>();
                string str = Convert.ToString(obj2["FilterKey"]);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    list = str.Split(new char[] { ',' }).ToList<string>();
                }
                dictionary.Add(Convert.ToInt32(obj2["Dimension_Id"]), list);
            }
            return dictionary;
        }

        private ReportHeader GetDimensionHeader(ReportHeader header)
        {
            ListHeader header2 = header.AddChild();
            int childCount = header.GetChildCount();
            header2.ColIndex = childCount + 1;
            header2.Caption = new LocaleValue(ResManager.LoadKDString("预算维度", "0032055000021878", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID);
            foreach (KeyValuePair<int, DimensionInfo> pair in this.dicDimensionInfo)
            {
                if (pair.Value.IsShowField)
                {
                    header2.AddChild(pair.Value.ShowFieldName, new LocaleValue(pair.Value.Name, base.Context.UserLocale.LCID));
                }
            }
            return header;
        }

        public Dictionary<int, List<int>> GetItemDataAndBusinessType()
        {
            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
            DynamicObjectCollection objects = this.dyFilter["ItemDataTypeIds"] as DynamicObjectCollection;
            DynamicObjectCollection objects2 = this.dyFilter["FBusinessTypeIds"] as DynamicObjectCollection;
            int num = 0;
            foreach (DynamicObject obj2 in objects)
            {
                int key = Convert.ToInt32(obj2["ItemDataTypeIds_Id"]);
                int item = 0;
                if (objects2.Count > num)
                {
                    item = Convert.ToInt32(objects2[num]["FBusinessTypeIds_Id"]);
                }
                List<int> list = new List<int>();
                if (dictionary.TryGetValue(key, out list))
                {
                    list.Add(item);
                }
                else
                {
                    List<int> list2 = new List<int> {
                        item
                    };
                    dictionary.Add(key, list2);
                }
                num++;
            }
            return dictionary;
        }

        private void GetReportFilter()
        {
            this.filterParameter = new BudgetFilterParameter();
            this.filterParameter.SchemeId = Convert.ToInt32(this.dyFilter["SchemeId_Id"]);
            DynamicObjectCollection objects = this.dyFilter["FCURRENCYIDS"] as DynamicObjectCollection;
            foreach (DynamicObject obj2 in objects)
            {
                this.filterParameter.LstCurrencyId.Add(Convert.ToInt32(obj2["FCURRENCYIDS_ID"]));
            }
            this.filterParameter.OrgIds = this.GetBudgetOrgIDs();
            this.filterParameter.StartYear = Convert.ToInt32(this.dyFilter["FSTARTYEAR"]);
            this.filterParameter.EndYear = Convert.ToInt32(this.dyFilter["FENDYEAR"]);
            this.filterParameter.StartPeriod = Convert.ToInt32(this.dyFilter["FSTARTPERIOD"]);
            this.filterParameter.EndPeriod = Convert.ToInt32(this.dyFilter["FENDPERIOD"]);
            this.filterParameter.PeriodType = Convert.ToString(this.dyFilter["FCtrlPeriod"]);
            this.filterParameter.IncludeUnAuditBill = Convert.ToBoolean(this.dyFilter["ContianSubmitBill"]);
            this.filterParameter.IsShowAdjustSumdata = Convert.ToBoolean(this.dyFilter["ShowAdjustSumdata"]);
            this.filterParameter.IsShowAdjustDetaildata = Convert.ToBoolean(this.dyFilter["ShowAdjustDetaildata"]);
            this.filterParameter.IsStandardCurrency = Convert.ToBoolean(this.dyFilter["ShowLocalCurrency"]);
            this.filterParameter.IsMergeCell = Convert.ToBoolean(this.dyFilter["MergeCell"]);
            this.filterParameter.DicDimissionFilter = this.GetDimensionFilter();
            this.filterParameter.ItemDataAndBusinessType = this.GetItemDataAndBusinessType();
            this.filterParameter.StartYearPeriod = (this.filterParameter.StartYear * 0x3e8) + this.filterParameter.StartPeriod;
            this.filterParameter.EndYearPeriod = (this.filterParameter.EndYear * 0x3e8) + this.filterParameter.EndPeriod;
            this.filterParameter.RptSchemeId = Convert.ToInt32(this.dyFilter["WizardSchemeId_Id"]);
        }

        public override ReportHeader GetReportHeaders(IRptParams filter)
        {
            return this.ReportHeadersDeal(filter);
        }

        public void GetReportTempData(IRptParams filter, string tableName)
        {
            using (new SessionScope())
            {
                string tempTableName = DBUtils.CreateSessionTemplateTable(base.Context, "TM_BM_BUDGETVALUE", "(\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FYEAR INT DEFAULT 0 NULL,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0 ,\r\n                                                    FBUSINESSTYPEID INT DEFAULT 0  ,\r\n                                                    FDATATYPEID INT DEFAULT 0  ,\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' ,\r\n                                                    FCURRENCYID INT DEFAULT 0,\r\n                                                    FBWBCURRENCYID INT DEFAULT 0 NULL,\r\n                                                    FISADJUST CHAR(1) DEFAULT '0' NOT NULL,\r\n                                                    FADJUSTNUM INT DEFAULT 0 NOT NULL,\r\n                                                    FVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FBASEVALUE DECIMAL(23,10) DEFAULT 0  NOT NULL,\r\n                                                    FBUDGETVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FADJUSTVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL,\r\n                                                    FFINALVALUE DECIMAL(23,10) DEFAULT 0 NOT NULL\r\n                                                    )");
                this.dyRptScheme = this.commonService.GetBillData(base.Context, "BM_RPTSCHEME", new List<object> { this.filterParameter.RptSchemeId }).FirstOrDefault<DynamicObject>();
                this.dyScheme = this.commonService.GetBillData(base.Context, "BM_SCHEME", new List<object> { this.filterParameter.SchemeId }).FirstOrDefault<DynamicObject>();
                this.GetBudgetDimensionInfo();
                if ((this.dyRptScheme == null) || (this.dyScheme == null))
                {
                    DBUtils.Execute(base.Context, "SELECT 1 AS fidentityid  INTO " + tableName + " FROM  T_BM_BUDGETVALUE WHERE 1=2 ");
                }
                else
                {
                    string str7;
                    if (this.dicDimensionField.Count == this.dicDimensionInfo.Count)
                    {
                        if (this.filterParameter.IsDimissionSumCtrl)
                        {
                            this.showDimensionType = 0;
                        }
                        else
                        {
                            this.showDimensionType = 2;
                        }
                    }
                    else if (this.dicDimensionField.Count < this.dicDimensionInfo.Count)
                    {
                        this.showDimensionType = 1;
                    }
                    string format = "";
                    if (this.showDimensionType > 0)
                    {
                        int num = 0;
                        foreach (KeyValuePair<string, string> pair in this.dicDimensionField)
                        {
                            if (num == 0)
                            {
                                format = format + ",A." + pair.Key;
                                format = format + ",A." + pair.Value;
                            }
                            else
                            {
                                format = format + ",A." + pair.Key;
                                format = format + ",A." + pair.Value;
                            }
                            num++;
                        }
                    }
                    string str3 = ResManager.LoadKDString("调整数", "0032054000022001", SubSystemType.FIN, new object[0]);
                    string str4 = ResManager.LoadKDString("原始数", "0032054000022002", SubSystemType.FIN, new object[0]);
                    SqlObject obj2 = this.billUsedService.GetBudgetValueInfos(base.Context, this.filterParameter, tempTableName);
                    DBUtils.ExecuteBatch(base.Context, new List<SqlObject> { obj2 });
                    if (this.filterParameter.IsStandardCurrency)
                    {
                        long num2 = 0L;
                        string str5 = " UPDATE {0} SET FBWBCURRENCYID={1}  WHERE FDEPTORGID={2} ";
                        List<string> sqlArray = new List<string>();
                        foreach (KeyValuePair<long, string> pair2 in this.filterParameter.OrgIds)
                        {
                            num2 = this.commonService.GetCurrencyIdDyDeptOrgID(base.Context, pair2.Key, 0);
                            sqlArray.Add(string.Format(str5, tempTableName, num2, pair2.Key));
                        }
                        DBUtils.ExecuteBatch(base.Context, sqlArray, sqlArray.Count);
                    }
                    string str6 = this.BudgetDimensionConvert(tempTableName);
                    StringBuilder builder = new StringBuilder();
                    builder.Clear();
                    builder.AppendFormat(" SELECT   BV.FYEAR,BV.FPERIOD,BV.FPERIODTYPE,BV.FDEPTORGID,ORG_L.FORGNAME AS FDEPTORG,BV.FORGTYPEID,BV.FBUSINESSTYPEID,BZ_L.FNAME AS FBUSINESSTYPE,BV.FDATATYPEID,DT_L.FNAME AS FDATATYPE,BV.FGROUPID,BV.FCURRENCYID,CYL.FNAME AS FCURRENCY,BV.FBWBCURRENCYID,BWBCYL.FNAME AS FBWBCURRENCY", new object[0]);
                    builder.AppendFormat(",(CASE WHEN BV.FISADJUST='1' THEN '{0}' ELSE '{1}' END) AS FADJUSTTYPE", str3, str4, this.localeId);
                    if (this.filterParameter.IsShowAdjustDetaildata)
                    {
                        builder.AppendLine(",SUM(BV.FVALUE) AS FVALUE ,SUM(BV.FBASEVALUE) AS FBASEVALUE");
                    }
                    else
                    {
                        builder.AppendFormat(",BV.FVALUE,BV.FBASEVALUE ", new object[0]);
                    }
                    builder.AppendFormat(format, new object[0]);
                    if (!string.IsNullOrWhiteSpace(filter.FilterParameter.SortString))
                    {
                        str7 = " ORDER BY " + filter.FilterParameter.SortString.Trim();
                    }
                    else
                    {
                        str7 = " Order By BV.FDEPTORGID,BV.FYEAR,BV.FPERIOD" + format + ",BV.FBUSINESSTYPEID,BV.FDATATYPEID,BV.FISADJUST ";
                    }
                    builder.AppendFormat(",{0} FPRECISION,ROW_NUMBER() OVER({1}) fidentityid", this.precision, str7);
                    builder.AppendFormat(" INTO {0}", tableName);
                    builder.AppendFormat(" FROM {0} BV", tempTableName);
                    builder.AppendFormat(" LEFT JOIN T_BD_Currency_L CYL ON BV.FCURRENCYID=CYL.FCURRENCYID AND CYL.FLOCALEID={0}", this.localeId);
                    builder.AppendFormat(" LEFT JOIN T_BD_Currency_L BWBCYL ON BV.FBWBCURRENCYID=BWBCYL.FCURRENCYID AND BWBCYL.FLOCALEID={0}", this.localeId);
                    builder.AppendFormat(" left JOIN  T_BM_BUDGETORGENTRY ORG ON BV.FDEPTORGID=ORG.FORGID ", this.localeId);
                    builder.AppendFormat("  LEFT JOIN T_BM_BUDGETORGENTRY_L ORG_L ON ORG.FENTRYID=ORG_L.FENTRYID  AND ORG_L.FLOCALEID={0}", this.localeId);
                    builder.AppendFormat("   LEFT JOIN T_BM_BUSINESSTYPE_L BZ_L ON BZ_L.FID=BV.FBUSINESSTYPEID AND BZ_L.FLOCALEID={0}", this.localeId);
                    builder.AppendFormat("  LEFT JOIN T_BD_RPTITEMDATATYPE_L DT_L ON DT_L.FDATATYPEID=BV.FDATATYPEID AND DT_L.FLOCALEID={0}", this.localeId);
                    string.Join(",", this.lstSelectField);
                    if (this.showDimensionType > 0)
                    {
                        builder.AppendFormat(" JOIN {0} A ON BV.FGROUPID=A.FGROUPID ", str6);
                    }
                    if (this.filterParameter.IsShowAdjustDetaildata)
                    {
                        string str8 = "GROUP BY BV.FYEAR, BV.FPERIOD, BV.FPERIODTYPE, BV.FDEPTORGID,  ORG_L.FORGNAME,BV.FORGTYPEID,BV.FBUSINESSTYPEID, BZ_L.FNAME, BV.FDATATYPEID, DT_L.FNAME,  BV.FGROUPID, BV.FCURRENCYID, CYL.FNAME,BV.FBWBCURRENCYID,BWBCYL.FNAME, BV.FISADJUST, BV.FADJUSTNUM ";
                        str8 = str8 + format;
                        builder.AppendFormat(str8, new object[0]);
                    }
                    DBUtils.Execute(base.Context, builder.ToString());
                    if (this.filterParameter.IsMergeCell)
                    {
                        builder.Clear();
                        if (base.Context.DatabaseType == DatabaseType.MS_SQL_Server)
                        {
                            builder.AppendFormat(" MERGE INTO {0} A USING ( ", tableName);
                            builder.AppendFormat(" SELECT FYEAR,FPERIOD,FDEPTORGID,MIN(fidentityid) AS FID FROM {0} ", tableName);
                            builder.AppendFormat(" GROUP BY FYEAR,FPERIOD,FDEPTORGID) B ", new object[0]);
                            builder.AppendFormat(" ON (A.FYEAR=B.FYEAR AND A.FPERIOD=B.FPERIOD AND A.FDEPTORGID=B.FDEPTORGID AND A.fidentityid>B.FID) ", new object[0]);
                            builder.AppendFormat(" WHEN MATCHED THEN UPDATE SET FYEAR=NULL , FPERIOD=NULL,FDEPTORG=NULL ; ", new object[0]);
                        }
                        else
                        {
                            builder.AppendFormat(" UPDATE {0} ", tableName);
                            builder.AppendFormat("  SET FYEAR=NULL ,FPERIOD=NULL,FDEPTORG=' ' ", new object[0]);
                            builder.AppendFormat("   WHERE fidentityid NOT IN (SELECT MIN(fidentityid) AS FID FROM {0} B ", tableName);
                            builder.AppendFormat("   GROUP BY FYEAR,FPERIOD,FDEPTORGID) ", new object[0]);
                        }
                        DBUtils.Execute(base.Context, builder.ToString());
                    }
                    DBUtils.DropSessionTemplateTable(base.Context, tempTableName);
                    DBUtils.DropSessionTemplateTable(base.Context, str6);
                }
            }
        }

        public override ReportTitles GetReportTitles(IRptParams filter)
        {
            return this.ReportTitlesDeal(filter, null);
        }

        public override void Initialize()
        {
            base.ReportProperty.ReportType = ReportType.REPORTTYPE_NORMAL;
            base.ReportProperty.IsUIDesignerColumns = false;
            base.ReportProperty.IsGroupSummary = true;
            this.SetDecimalControl();
            this.localeId = base.Context.UserLocale.LCID;
        }

        private ReportHeader ReportHeadersDeal(IRptParams filter)
        {
            int childCount = 0;
            ReportHeader header = new ReportHeader();
            header.AddChild("FDEPTORG", new LocaleValue(ResManager.LoadKDString("预算组织", "0032055000021891", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FYEAR", new LocaleValue(ResManager.LoadKDString("预算年度", "0032055000021893", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FPERIOD", new LocaleValue(ResManager.LoadKDString("预算期间", "0032055000021894", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FBUSINESSTYPE", new LocaleValue(ResManager.LoadKDString("预算业务类型", "0032055000021880", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FDATATYPE", new LocaleValue(ResManager.LoadKDString("项目数据类型", "0032055000021881", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            this.GetDimensionHeader(header);
            if (this.filterParameter.IsStandardCurrency)
            {
                ListHeader header2 = header.AddChild();
                childCount = header.GetChildCount();
                header2.ColIndex = childCount + 1;
                header2.Caption = new LocaleValue(ResManager.LoadKDString("原币", "0032055000022021", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID);
                header2.AddChild("FCURRENCY", new LocaleValue(ResManager.LoadKDString("币别", "0032055000022022", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
                header2.AddChild("FVALUE", new LocaleValue(ResManager.LoadKDString("预算数", "0032055000021888", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
                ListHeader header3 = header.AddChild();
                childCount = header.GetChildCount();
                header3.ColIndex = childCount + 1;
                header3.Caption = new LocaleValue(ResManager.LoadKDString("本位币", "0032055000022023", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID);
                header3.AddChild("FBWBCURRENCY", new LocaleValue(ResManager.LoadKDString("币别", "0032055000022022", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
                header3.AddChild("FBASEVALUE", new LocaleValue(ResManager.LoadKDString("预算数", "0032055000021888", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            }
            else
            {
                header.AddChild("FCURRENCY", new LocaleValue(ResManager.LoadKDString("币别", "0032055000022022", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
                header.AddChild("FVALUE", new LocaleValue(ResManager.LoadKDString("预算数", "0032055000021888", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            }
            if (this.filterParameter.IsShowAdjustDetaildata)
            {
                header.AddChild("FADJUSTTYPE", new LocaleValue(ResManager.LoadKDString("数据类型", "0032055000022024", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            }
            return header;
        }

        private ReportTitles ReportTitlesDeal(IRptParams filter, Dictionary<string, object> dic = null)
        {
            ReportTitles titles = new ReportTitles();
            DynamicObject customFilter = filter.FilterParameter.CustomFilter;
            return titles;
        }

        public void SetDecimalControl()
        {
            List<DecimalControlField> list = new List<DecimalControlField>();
            foreach (string str in this.lstQutityField)
            {
                DecimalControlField item = new DecimalControlField {
                    DecimalControlFieldName = "FPRECISION",
                    ByDecimalControlFieldName = str
                };
                list.Add(item);
            }
            base.ReportProperty.DecimalControlFieldList = list;
        }

        private List<string> lstQutityField
        {
            get
            {
                return new List<string> { "FVALUE", "FBASEVALUE" };
            }
        }
    }
}

