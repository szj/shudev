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
    using Kingdee.BOS.Core.Metadata.FormElement;
    using Kingdee.BOS.Core.Report;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class BudgetExecutionReport : SysReportBaseService
    {
        private BudgetBillUsedService billUsedService = new BudgetBillUsedService();
        private BudgetUsedArgs budgetUsedArgs;
        private int calendarId;
        private CommonService commonService = new CommonService();
        private string dept = ResManager.LoadKDString("部门", "0032055000021896", SubSystemType.FIN, new object[0]);
        private Dictionary<string, string> dicDimensionField = new Dictionary<string, string>();
        private Dictionary<int, DimensionInfo> dicDimensionInfo = new Dictionary<int, DimensionInfo>();
        private Dictionary<string, BusinessInfo> dicDimensionMetaData = new Dictionary<string, BusinessInfo>();
        private Dictionary<int, int> dicSumPeriodRef = new Dictionary<int, int>();
        private DynamicObject dyCalendar;
        private DynamicObject dyCtrlRule;
        private DynamicObject dyFilter;
        private DynamicObject dyRptScheme;
        private DynamicObject dyScheme;
        private BudgetFilterParameter filterParameter;
        private int localeId = 0x804;
        private List<BudgetOrgDept> lstOrgDeptInfo;
        private List<string> lstSelectField = new List<string> { "FDEPTORGID", "FORGTYPEID", "FYEAR", "FPERIOD", "FPERIODTYPE", "FDATATYPEID" };
        private long operationId;
        private string org = ResManager.LoadKDString("组织", "0032055000021897", SubSystemType.FIN, new object[0]);
        private int precision = 2;
        private int showDimensionType;
        private const string TM_BM_BUDGETBILLUSED_FIELD = "(\r\n                                                    FORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FDEPTID INT DEFAULT 0 NOT NULL,\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FDATE DATETIME,\r\n                                                    FYEAR INT DEFAULT 0,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0,\r\n                                                    FDATATYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FCTRLTIME CHAR(1) DEFAULT '0' NOT NULL,\r\n                                                    FUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL\r\n                                                    )";
        private const string TM_BM_BUDGETEXECUTION_FIELD = "(\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FYEAR INT DEFAULT 0,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0,\r\n                                                    FBUSINESSTYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FDATATYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FBUDGETVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FADJUSTVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FFINALVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FLOCKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FLOCKBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FREALUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FUSABLEVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FPERCENT DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME1 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME2 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME3 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME4 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME5 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME6 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME7 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME8 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FPRECISION INT DEFAULT 0 NOT NULL\r\n                                                    )";
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

        public void BudgetDimensionDeal(string tableName)
        {
            this.BuildDimensionField(tableName);
            foreach (KeyValuePair<int, DimensionInfo> pair in this.dicDimensionInfo)
            {
                if (pair.Value.IsShowField)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("/*dialect*/ Merge Into {0} A ", tableName);
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
        }

        public void BuildDimensionField(string tableName)
        {
            string format = "IF NOT EXISTS (SELECT 1 FROM KSQL_USERCOLUMNS WHERE KSQL_COL_TABNAME = '{0}' AND KSQL_COL_NAME='{1}')";
            format = format + " ALTER TABLE {0} ADD ( {1} {2});";
            List<string> sqlArray = new List<string>();
            foreach (KeyValuePair<int, DimensionInfo> pair in this.dicDimensionInfo)
            {
                if (pair.Value.IsShowField)
                {
                    if (pair.Value.Category == "10")
                    {
                        sqlArray.Add(string.Format(format, tableName, pair.Value.ShowFieldId, "VARCHAR(36)"));
                    }
                    else
                    {
                        sqlArray.Add(string.Format(format, tableName, pair.Value.ShowFieldId, "INT DEFAULT 0 NOT NULL"));
                    }
                    sqlArray.Add(string.Format(format, tableName, pair.Value.ShowFieldName, "NVARCHAR(2000)"));
                }
            }
            DBUtils.ExecuteBatch(base.Context, sqlArray, 5);
        }

        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            this.dyFilter = filter.FilterParameter.CustomFilter;
            this.GetReportFilter();
            this.GetReportTempData(filter, tableName);
        }

        private void DeletedTempTable(Context ctx, Dictionary<string, string> dtTableName, Dictionary<string, object> dParam)
        {
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
            dictionary.Add(0L, "ORG");
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

        public string GetBudgetUsedTable()
        {
            string tableName = DBUtils.CreateSessionTemplateTable(base.Context, "TM_BM_BUDGETBILLUSED", "(\r\n                                                    FORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FDEPTID INT DEFAULT 0 NOT NULL,\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FDATE DATETIME,\r\n                                                    FYEAR INT DEFAULT 0,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0,\r\n                                                    FDATATYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FCTRLTIME CHAR(1) DEFAULT '0' NOT NULL,\r\n                                                    FUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL\r\n                                                    )");
            List<SqlObject> lstSqlObj = this.billUsedService.GetBudgetBillUsedTable(base.Context, this.lstOrgDeptInfo, this.budgetUsedArgs, this.operationId, tableName);
            DBUtils.ExecuteBatch(base.Context, lstSqlObj);
            string str2 = this.filterParameter.IsPeriodSummary ? this.filterParameter.SumPeriodType : this.filterParameter.PeriodType;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("/*dialect*/ Merge Into {0} A ", tableName);
            builder.AppendLine(" Using (SELECT B.FPERIODTYPE,B.FPERIODSTARTDATE,B.FPERIODENDDATE,B.FPERIODYEAR AS FYEAR,B.FPERIOD ");
            builder.AppendLine(" FROM T_BM_BUDGETPERIOD B ");
            builder.AppendFormat(" WHERE B.FID={0} AND B.FPERIODTYPE={1} ) T", this.calendarId, str2);
            builder.AppendLine("  ON (A.FDATE>=T.FPERIODSTARTDATE AND A.FDATE<=T.FPERIODENDDATE) WHEN MATCHED THEN UPDATE SET  ");
            builder.AppendLine(" A.FYEAR=T.FYEAR,A.FPERIOD =T.FPERIOD,A.FPERIODTYPE=T.FPERIODTYPE");
            if (base.Context.DatabaseType == DatabaseType.MS_SQL_Server)
            {
                builder.AppendLine(";");
            }
            DBUtils.Execute(base.Context, builder.ToString());
            builder.Clear();
            builder.AppendFormat(" UPDATE {0} SET FORGTYPEID='ORG',FDEPTORGID=FORGID WHERE FDEPTID=0  ", tableName);
            DBUtils.Execute(base.Context, builder.ToString());
            if (this.lstOrgDeptInfo.Any<BudgetOrgDept>(p => p.DeptInfo.Id > 0))
            {
                builder.Clear();
                builder.AppendFormat(" UPDATE {0} AS A SET (FORGTYPEID,FDEPTORGID,FDEPTID)=(SELECT 'DEPT' AS FORGTYPEID,FMASTERID AS FDEPTORGID,FMASTERID AS FDEPTID FROM T_BD_DEPARTMENT B WHERE A.FDEPTID>0 AND A.FDEPTID=B.FDEPTID) ", tableName);
                DBUtils.Execute(base.Context, builder.ToString());
            }
            this.UpdateDimensionMasterId(tableName);
            return tableName;
        }

        private ReportHeader GetBudgetValueHeader(ReportHeader header)
        {
            if (this.filterParameter.IsShowAdjustData)
            {
                ListHeader header2 = header.AddChild();
                header2 = header.AddChild();
                int num = header.GetChildCount() + 1;
                header2.ColIndex = num + 1;
                header2.Caption = new LocaleValue(ResManager.LoadKDString("预算数", "0032055000021888", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID);
                header2.AddChild("FBUDGETVALUE", new LocaleValue(ResManager.LoadKDString("原始数", "0032055000021889", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
                header2.AddChild("FADJUSTVALUE", new LocaleValue(ResManager.LoadKDString("调整数", "0032055000021892", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
                header2.AddChild("FFINALVALUE", new LocaleValue(ResManager.LoadKDString("调整后", "0032055000021890", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
                return header;
            }
            header.AddChild("FFINALVALUE", new LocaleValue(ResManager.LoadKDString("预算数", "0032055000021888", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            return header;
        }

        public BaseDataInfo GetCurrencyInfo()
        {
            return new BaseDataInfo { Id = this.filterParameter.LstCurrencyId.FirstOrDefault<int>() };
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
            int num = header.GetChildCount() + 1;
            header2.ColIndex = num + 1;
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
            if (Convert.ToInt32(this.dyFilter["FCURRENCYID"]) == 0)
            {
                this.filterParameter.IsStandardCurrency = true;
            }
            else
            {
                this.filterParameter.IsStandardCurrency = false;
                this.filterParameter.LstCurrencyId.Add(Convert.ToInt32(this.dyFilter["FCURRENCYID"]));
            }
            this.filterParameter.OrgIds = this.GetBudgetOrgIDs();
            this.filterParameter.StartYear = Convert.ToInt32(this.dyFilter["FSTARTYEAR"]);
            this.filterParameter.EndYear = Convert.ToInt32(this.dyFilter["FENDYEAR"]);
            this.filterParameter.StartPeriod = Convert.ToInt32(this.dyFilter["FSTARTPERIOD"]);
            this.filterParameter.EndPeriod = Convert.ToInt32(this.dyFilter["FENDPERIOD"]);
            this.filterParameter.PeriodType = Convert.ToString(this.dyFilter["FCtrlPeriod"]);
            this.filterParameter.IncludeUnAuditBill = Convert.ToBoolean(this.dyFilter["ContianSubmitBill"]);
            this.filterParameter.IsShowAdjustData = Convert.ToBoolean(this.dyFilter["ShowAdjustData"]);
            this.filterParameter.DicDimissionFilter = this.GetDimensionFilter();
            this.filterParameter.ItemDataAndBusinessType = this.GetItemDataAndBusinessType();
            this.filterParameter.StartYearPeriod = (this.filterParameter.StartYear * 0x3e8) + this.filterParameter.StartPeriod;
            this.filterParameter.EndYearPeriod = (this.filterParameter.EndYear * 0x3e8) + this.filterParameter.EndPeriod;
            this.filterParameter.IsDimissionSumCtrl = Convert.ToBoolean(this.dyFilter["IsDimissionSumCtrl"]);
            this.filterParameter.IsPeriodSummary = Convert.ToBoolean(this.dyFilter["FIsSummaryCtrl"]);
            if (this.filterParameter.IsPeriodSummary)
            {
                this.filterParameter.SumPeriodType = Convert.ToString(this.dyFilter["FSummaryPeriod"]);
            }
            int ruleId = Convert.ToInt32(this.dyFilter["RuleId_Id"]);
            this.filterParameter.RuleId = ruleId;
            this.dyCtrlRule = this.commonService.GetCtrlRule(base.Context, ruleId);
            if (this.dyCtrlRule != null)
            {
                this.filterParameter.RptSchemeId = Convert.ToInt32(this.dyCtrlRule["patternScheme_Id"]);
            }
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
                if ((this.dyRptScheme == null) || (this.dyScheme == null))
                {
                    DBUtils.Execute(base.Context, "SELECT 1 AS fidentityid  INTO " + tableName + " FROM  T_BM_BUDGETVALUE  WHERE 1=2 ");
                }
                else
                {
                    string str8;
                    this.GetBudgetDimensionInfo();
                    this.budgetUsedArgs = this.billUsedService.GetBudgetDataArgs(base.Context, this.dyCtrlRule);
                    this.budgetUsedArgs.CurrencyInfo = this.GetCurrencyInfo();
                    this.budgetUsedArgs.SchemePeriodInfo = this.GetSchemePeriodInfo();
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
                    string str2 = DBUtils.CreateSessionTemplateTable(base.Context, "TM_BM_BUDGETEXECUTION", "(\r\n                                                    FDEPTORGID INT DEFAULT 0 NOT NULL,\r\n                                                    FORGTYPEID VARCHAR(20) DEFAULT ' ' NOT NULL,\r\n                                                    FYEAR INT DEFAULT 0,\r\n                                                    FPERIODTYPE INT DEFAULT 0,\r\n                                                    FPERIOD INT DEFAULT 0,\r\n                                                    FBUSINESSTYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FDATATYPEID INT DEFAULT 0 NOT NULL,\r\n                                                    FGROUPID VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FBUDGETVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FADJUSTVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FFINALVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FLOCKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FLOCKBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FBACKVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FREALUSEDVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FUSABLEVALUE DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FPERCENT DECIMAL(23,10) DEFAULT 0 NULL,\r\n                                                    FDIMENSIONID1 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID2 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID3 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID4 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID5 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID6 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID7 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONID8 VARCHAR(36) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME1 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME2 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME3 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME4 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME5 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME6 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME7 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FDIMENSIONNAME8 NVARCHAR(2000) DEFAULT ' ' NULL,\r\n                                                    FPRECISION INT DEFAULT 0 NOT NULL\r\n                                                    )");
                    SqlObject budgetValueInfos = this.billUsedService.GetBudgetValueInfos(this.filterParameter, tempTableName);
                    DBUtils.ExecuteBatch(base.Context, new List<SqlObject> { budgetValueInfos });
                    this.UpdateSumPeriod(tempTableName);
                    string str3 = this.BudgetDimensionConvert(tempTableName);
                    StringBuilder builder = new StringBuilder();
                    new List<string>();
                    string str4 = "";
                    string str5 = "";
                    if (this.showDimensionType > 0)
                    {
                        int num = 0;
                        foreach (KeyValuePair<string, string> pair in this.dicDimensionField)
                        {
                            if (num == 0)
                            {
                                str4 = str4 + "B." + pair.Key;
                                str4 = str4 + ",B." + pair.Value;
                                str5 = str5 + pair.Key;
                                str5 = str5 + "," + pair.Value;
                            }
                            else
                            {
                                str4 = str4 + ",B." + pair.Key;
                                str4 = str4 + ",B." + pair.Value;
                                str5 = str5 + "," + pair.Key;
                                str5 = str5 + "," + pair.Value;
                            }
                            num++;
                        }
                    }
                    string str6 = string.Join(",", this.lstSelectField) + ",FBUSINESSTYPEID";
                    builder.AppendFormat(" INSERT INTO {0}({1},FGROUPID,{2}FBUDGETVALUE,FADJUSTVALUE,FFINALVALUE)", str2, str6, (this.showDimensionType > 0) ? (str5 + ",") : "");
                    builder.AppendFormat(" SELECT {0}", str6);
                    if (this.showDimensionType == 0)
                    {
                        builder.AppendFormat(",' ' FGROUPID,SUM(A.FBUDGETVALUE) AS FBUDGETVALUE,SUM(A.FADJUSTVALUE) AS FADJUSTVALUE,SUM(A.FFINALVALUE) FFINALVALUE", new object[0]);
                    }
                    if (this.showDimensionType == 1)
                    {
                        builder.AppendFormat(",' ' FGROUPID,{0},SUM(A.FBUDGETVALUE) AS FBUDGETVALUE,SUM(A.FADJUSTVALUE) AS FADJUSTVALUE,SUM(A.FFINALVALUE) FFINALVALUE", str4);
                    }
                    else if (this.showDimensionType == 2)
                    {
                        builder.AppendFormat(",A.FGROUPID,{0},SUM(A.FBUDGETVALUE) AS FBUDGETVALUE,SUM(A.FADJUSTVALUE) AS FADJUSTVALUE,SUM(A.FFINALVALUE) FFINALVALUE", str4);
                    }
                    builder.AppendFormat(" FROM {0} A ", tempTableName);
                    if (this.showDimensionType > 0)
                    {
                        builder.AppendFormat(" JOIN {0} B ON B.FGROUPID=A.FGROUPID ", str3);
                        if (this.showDimensionType == 1)
                        {
                            builder.AppendFormat(" GROUP BY {0},{1}", str6, str4);
                        }
                        if (this.showDimensionType == 2)
                        {
                            builder.AppendFormat(" GROUP BY {0},A.FGROUPID,{1}", str6, str4);
                        }
                    }
                    else
                    {
                        builder.AppendFormat(" GROUP BY {0}", str6);
                    }
                    DBUtils.Execute(base.Context, builder.ToString());
                    if (this.filterParameter.IncludeUnAuditBill)
                    {
                        this.operationId = FormOperation.Operation_Submit;
                    }
                    else
                    {
                        this.operationId = FormOperation.Operation_Audit;
                    }
                    string budgetUsedTable = this.GetBudgetUsedTable();
                    this.UpdateBudgetUsedInfo(str2, budgetUsedTable, "1");
                    this.UpdateBudgetUsedInfo(str2, budgetUsedTable, "2");
                    this.UpdateBudgetUsedInfo(str2, budgetUsedTable, "3");
                    this.UpdateBudgetLockBackInfo(str2, budgetUsedTable);
                    if (!string.IsNullOrWhiteSpace(filter.FilterParameter.SortString))
                    {
                        str8 = " ORDER BY " + filter.FilterParameter.SortString.Trim();
                    }
                    else
                    {
                        str8 = " Order By A.FDEPTORGID,A.FYEAR,A.FPERIOD,A.FDATATYPEID,A.FBUSINESSTYPEID";
                        if (this.showDimensionType > 0)
                        {
                            foreach (KeyValuePair<int, DimensionInfo> pair2 in this.dicDimensionInfo)
                            {
                                if (pair2.Value.IsShowField)
                                {
                                    str8 = str8 + string.Format(",A.{0}", pair2.Value.ShowFieldName);
                                }
                            }
                        }
                    }
                    builder.Clear();
                    builder.AppendFormat("select A.FDEPTORGID,A.FORGTYPEID,A.FYEAR,A.FPERIODTYPE,A.FPERIOD,A.FBUSINESSTYPEID,A.FDATATYPEID,A.FGROUPID", new object[0]);
                    builder.AppendLine(",A.FBUDGETVALUE,A.FADJUSTVALUE,A.FFINALVALUE,A.FLOCKVALUE,A.FLOCKBACKVALUE,A.FUSEDVALUE,A.FBACKVALUE");
                    builder.AppendLine(",(A.FLOCKVALUE-A.FLOCKBACKVALUE+A.FUSEDVALUE-A.FBACKVALUE) AS FREALUSEDVALUE");
                    builder.AppendLine(",(A.FFINALVALUE-A.FLOCKVALUE+A.FLOCKBACKVALUE-A.FUSEDVALUE+A.FBACKVALUE) AS FUSABLEVALUE,(CASE WHEN A.FFINALVALUE=0 THEN 0 ELSE ROUND(100.0*(A.FLOCKVALUE-A.FLOCKBACKVALUE+A.FUSEDVALUE-A.FBACKVALUE)/A.FFINALVALUE,4) END) AS FPERCENT");
                    builder.AppendLine(",(CASE WHEN A.FORGTYPEID='ORG' THEN B.FNAME ELSE C.FNAME END) AS FDEPTORG");
                    builder.AppendFormat(",(CASE WHEN A.FORGTYPEID='ORG' THEN '{0}' ELSE '{1}' END) AS FORGTYPE", this.org, this.dept);
                    builder.AppendLine(",D.FNAME AS FBUSINESSTYPE,E.FNAME AS FDATATYPE");
                    if (this.showDimensionType > 0)
                    {
                        builder.AppendFormat(",{0}", str5);
                    }
                    builder.AppendFormat(",{0} FPRECISION,ROW_NUMBER() OVER({1}) fidentityid", this.precision, str8);
                    builder.AppendFormat(" INTO {0}", tableName);
                    builder.AppendFormat(" FROM {0} A", str2);
                    builder.AppendFormat(" LEFT JOIN T_ORG_ORGANIZATIONS_L B ON B.FORGID=A.FDEPTORGID AND B.FLOCALEID={0} ", this.localeId);
                    builder.AppendFormat(" LEFT JOIN T_BD_DEPARTMENT_L C ON C.FDEPTID=A.FDEPTORGID AND C.FLOCALEID={0} ", this.localeId);
                    builder.AppendFormat(" LEFT JOIN T_BM_BUSINESSTYPE_L D ON D.FID=A.FBUSINESSTYPEID AND D.FLOCALEID={0} ", this.localeId);
                    builder.AppendFormat(" LEFT JOIN t_BD_RptItemDataType_L E ON E.FDATATYPEID=A.FDATATYPEID AND E.FLOCALEID={0} ", this.localeId);
                    DBUtils.Execute(base.Context, builder.ToString());
                    DBUtils.DropSessionTemplateTable(base.Context, tempTableName);
                    DBUtils.DropSessionTemplateTable(base.Context, str2);
                    DBUtils.DropSessionTemplateTable(base.Context, budgetUsedTable);
                    DBUtils.DropSessionTemplateTable(base.Context, str3);
                }
            }
        }

        public override ReportTitles GetReportTitles(IRptParams filter)
        {
            return this.ReportTitlesDeal(filter, null);
        }

        private SchemePeriodInfo GetSchemePeriodInfo()
        {
            SchemePeriodInfo info = new SchemePeriodInfo {
                SchemeId = this.filterParameter.SchemeId,
                StartYear = this.filterParameter.StartYear,
                EndYear = this.filterParameter.EndYear,
                StartPeriod = this.filterParameter.StartPeriod,
                EndPeriod = this.filterParameter.EndPeriod,
                PeriodType = (CycleType) Convert.ToInt32(this.filterParameter.PeriodType)
            };
            if (this.dyScheme != null)
            {
                this.calendarId = Convert.ToInt32(this.dyScheme["CalendarId_Id"]);
            }
            List<object> fids = new List<object> {
                this.calendarId
            };
            this.dyCalendar = this.commonService.GetBillData(base.Context, "BM_BUDGETCALENDAR", fids).FirstOrDefault<DynamicObject>();
            Dictionary<int, DynamicObject> dictionary = new Dictionary<int, DynamicObject>();
            Dictionary<int, DynamicObject> dictionary2 = new Dictionary<int, DynamicObject>();
            DateTime minValue = DateTime.MinValue;
            DateTime time2 = DateTime.MinValue;
            if (this.dyCalendar != null)
            {
                this.calendarId = Convert.ToInt32(this.dyCalendar["Id"]);
                DynamicObjectCollection objects = this.dyCalendar["BudgetPeriodEntity"] as DynamicObjectCollection;
                foreach (DynamicObject obj2 in objects)
                {
                    string str = Convert.ToString(obj2["PeriodType"]);
                    int num = Convert.ToInt32(obj2["PeriodYear"]);
                    int key = (num * 0x3e8) + Convert.ToInt32(obj2["Period"]);
                    if (((str == this.filterParameter.PeriodType) && (key >= this.filterParameter.StartYearPeriod)) && (key <= this.filterParameter.EndYearPeriod))
                    {
                        dictionary.Add(key, obj2);
                        if (key == this.filterParameter.StartYearPeriod)
                        {
                            minValue = Convert.ToDateTime(obj2["PeriodStartDate"]);
                        }
                        if (key == this.filterParameter.EndYearPeriod)
                        {
                            time2 = Convert.ToDateTime(obj2["PeriodEndDate"]);
                        }
                    }
                    if ((this.filterParameter.IsPeriodSummary && (str == this.filterParameter.SumPeriodType)) && ((num >= this.filterParameter.StartYear) && (num <= this.filterParameter.EndYear)))
                    {
                        dictionary2.Add(key, obj2);
                    }
                }
            }
            if (this.filterParameter.IsPeriodSummary)
            {
                this.dicSumPeriodRef.Clear();
                foreach (KeyValuePair<int, DynamicObject> pair in dictionary)
                {
                    DateTime time3 = Convert.ToDateTime(pair.Value["PeriodStartDate"]);
                    DateTime time4 = Convert.ToDateTime(pair.Value["PeriodEndDate"]);
                    foreach (KeyValuePair<int, DynamicObject> pair2 in dictionary2)
                    {
                        DateTime time5 = Convert.ToDateTime(pair2.Value["PeriodStartDate"]);
                        DateTime time6 = Convert.ToDateTime(pair2.Value["PeriodEndDate"]);
                        if ((DateTime.Compare(time5, time3) <= 0) && (DateTime.Compare(time6, time4) >= 0))
                        {
                            this.dicSumPeriodRef.Add(pair.Key, pair2.Key);
                            break;
                        }
                    }
                }
            }
            info.PeriodStartDate = minValue;
            info.PeriodEndDate = time2;
            info.StartYearPeriod = this.filterParameter.StartYearPeriod;
            info.EndYearPeriod = this.filterParameter.StartYearPeriod;
            return info;
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
            int num = 0;
            ReportHeader header = new ReportHeader();
            header.AddChild("FYEAR", new LocaleValue(ResManager.LoadKDString("预算年度", "0032055000021893", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FPERIOD", new LocaleValue(ResManager.LoadKDString("预算期间", "0032055000021894", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FDEPTORG", new LocaleValue(ResManager.LoadKDString("预算组织", "0032055000021891", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FORGTYPE", new LocaleValue(ResManager.LoadKDString("组织类型", "0032055000021879", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FBUSINESSTYPE", new LocaleValue(ResManager.LoadKDString("预算业务类型", "0032055000021880", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            header.AddChild("FDATATYPE", new LocaleValue(ResManager.LoadKDString("项目数据类型", "0032055000021881", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID));
            if (this.showDimensionType > 0)
            {
                this.GetDimensionHeader(header);
            }
            this.GetBudgetValueHeader(header);
            ListHeader header2 = header.AddChild();
            header2 = header.AddChild();
            num = header.GetChildCount() + 1;
            header2.ColIndex = num + 1;
            header2.Caption = new LocaleValue(ResManager.LoadKDString("预算执行", "0032055000021882", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID);
            header2.AddChild("FLOCKVALUE", new LocaleValue(ResManager.LoadKDString("申请占用", "0032055000021883", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FLOCKBACKVALUE", new LocaleValue(ResManager.LoadKDString("申请冲回", "0032055000021884", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FUSEDVALUE", new LocaleValue(ResManager.LoadKDString("预算执行", "0032055000021882", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FBACKVALUE", new LocaleValue(ResManager.LoadKDString("执行冲回", "0032055000021885", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FREALUSEDVALUE", new LocaleValue(ResManager.LoadKDString("实际执行", "0032055000021886", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FUSABLEVALUE", new LocaleValue(ResManager.LoadKDString("可用预算", "0032055000021887", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
            header2.AddChild("FPERCENT", new LocaleValue(ResManager.LoadKDString("预算执行进度%", "0032055000021877", SubSystemType.FIN, new object[0]), base.Context.UserLocale.LCID), SqlStorageType.SqlDecimal, true);
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

        public void UpdateBudgetLockBackInfo(string tableName, string billUsedTable)
        {
            string str = "FLOCKBACKVALUE";
            string str2 = string.Join(",", this.lstSelectField);
            if ((this.showDimensionType > 0) && (this.dicDimensionField.Count > 0))
            {
                str2 = str2 + "," + string.Join(",", this.dicDimensionField.Keys);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("/*dialect*/ Merge Into {0} A ", tableName);
            builder.AppendFormat(" Using (SELECT {0} ", str2);
            builder.AppendLine(" ,SUM(B.FBACKVALUE) AS FLOCKBACKVALUE ");
            builder.AppendFormat(" FROM {0} B ", billUsedTable);
            builder.AppendLine(" WHERE B.FCTRLTIME IN ('1','2') ");
            builder.AppendFormat(" GROUP BY {0}) T", str2);
            builder.AppendLine("  ON (A.FDEPTORGID=T.FDEPTORGID AND A.FORGTYPEID=T.FORGTYPEID AND A.FYEAR=T.FYEAR AND A.FPERIOD=T.FPERIOD AND A.FPERIODTYPE=T.FPERIODTYPE AND A.FDATATYPEID=T.FDATATYPEID ");
            if (this.showDimensionType > 0)
            {
                foreach (KeyValuePair<string, string> pair in this.dicDimensionField)
                {
                    builder.AppendFormat(" AND A.{0}=T.{0}", pair.Key);
                }
            }
            builder.AppendLine(" ) WHEN MATCHED THEN UPDATE SET  ");
            builder.AppendFormat(" A.{0}=T.FLOCKBACKVALUE ", str);
            if (base.Context.DatabaseType == DatabaseType.MS_SQL_Server)
            {
                builder.AppendLine(";");
            }
            DBUtils.Execute(base.Context, builder.ToString());
        }

        public void UpdateBudgetUsedInfo(string tableName, string billUsedTable, string ctrlTime)
        {
            string str = "FUSEDVALUE";
            string str3 = ctrlTime;
            if (str3 != null)
            {
                if (!(str3 == "1"))
                {
                    if (str3 == "2")
                    {
                        str = "FUSEDVALUE";
                    }
                    else if (str3 == "3")
                    {
                        str = "FBACKVALUE";
                    }
                }
                else
                {
                    str = "FLOCKVALUE";
                }
            }
            string str2 = string.Join(",", this.lstSelectField);
            if ((this.showDimensionType > 0) && (this.dicDimensionField.Count > 0))
            {
                str2 = str2 + "," + string.Join(",", this.dicDimensionField.Keys);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("/*dialect*/ Merge Into {0} A ", tableName);
            builder.AppendFormat(" Using (SELECT {0} ", str2);
            builder.AppendLine(" ,SUM(B.FUSEDVALUE) AS FUSEDVALUE ");
            builder.AppendFormat(" FROM {0} B ", billUsedTable);
            builder.AppendFormat(" WHERE B.FCTRLTIME={0} ", ctrlTime);
            builder.AppendFormat(" GROUP BY {0}) T", str2);
            builder.AppendLine("  ON (A.FDEPTORGID=T.FDEPTORGID AND A.FORGTYPEID=T.FORGTYPEID AND A.FYEAR=T.FYEAR AND A.FPERIOD=T.FPERIOD AND A.FPERIODTYPE=T.FPERIODTYPE AND A.FDATATYPEID=T.FDATATYPEID ");
            if (this.showDimensionType > 0)
            {
                foreach (KeyValuePair<string, string> pair in this.dicDimensionField)
                {
                    builder.AppendFormat(" AND A.{0}=T.{0}", pair.Key);
                }
            }
            builder.AppendLine(" ) WHEN MATCHED THEN UPDATE SET  ");
            builder.AppendFormat(" A.{0}=T.FUSEDVALUE ", str);
            if (base.Context.DatabaseType == DatabaseType.MS_SQL_Server)
            {
                builder.AppendLine(";");
            }
            DBUtils.Execute(base.Context, builder.ToString());
        }

        public void UpdateDimensionMasterId(string tempTableName)
        {
            List<string> sqlArray = new List<string>();
            string format = " UPDATE {0} AS A SET ({1})=(SELECT {2} AS FMASTERID FROM {3} B WHERE A.{1}=B.{4}) ";
            foreach (KeyValuePair<int, DimensionInfo> pair in this.dicDimensionInfo)
            {
                DimensionInfo info = pair.Value;
                if (info.IsShowField && (info.DataControlType == 2))
                {
                    sqlArray.Add(string.Format(format, new object[] { tempTableName, info.ShowFieldId, info.MasterIDFieldName, info.TableName, info.PKFieldName }));
                }
            }
            if (sqlArray.Count > 0)
            {
                DBUtils.ExecuteBatch(base.Context, sqlArray, sqlArray.Count);
            }
        }

        public void UpdateSumPeriod(string tableName)
        {
            string format = "UPDATE {0} SET FYEAR={1},FPERIOD={2},FPERIODTYPE={3} WHERE FYEAR={4} AND FPERIOD={5} AND FPERIODTYPE={6}";
            List<string> sqlArray = new List<string>();
            if (this.filterParameter.IsPeriodSummary && (this.dicSumPeriodRef.Count > 0))
            {
                foreach (KeyValuePair<int, int> pair in this.dicSumPeriodRef)
                {
                    sqlArray.Add(string.Format(format, new object[] { tableName, pair.Value / 0x3e8, pair.Value % 0x3e8, this.filterParameter.SumPeriodType, pair.Key / 0x3e8, pair.Key % 0x3e8, this.filterParameter.PeriodType }));
                }
                DBUtils.ExecuteBatch(base.Context, sqlArray, 8);
            }
        }

        private List<string> lstQutityField
        {
            get
            {
                return new List<string> { "FBUDGETVALUE", "FADJUSTVALUE", "FFINALVALUE", "FLOCKVALUE", "FUSEDVALUE", "FBACKVALUE", "FUSABLEVALUE", "FPERCENT" };
            }
        }
    }
}

