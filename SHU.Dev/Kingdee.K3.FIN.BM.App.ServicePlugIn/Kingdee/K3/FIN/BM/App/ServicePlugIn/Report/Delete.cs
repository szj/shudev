namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
{
    using Kingdee.BOS.Core.DynamicForm.PlugIn;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.BM.Common.Core;
    using Kingdee.K3.FIN.ReportEntity.PivotGrid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    [Description("报表基础-删除")]
    public class Delete : AbstractOperationServicePlugIn
    {
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {
            Action<DynamicObject> action = null;
            if (base.Context.IsMultiOrg)
            {
                if (action == null)
                {
                    action = delegate (DynamicObject entiry) {
                        Func<SchemeOrgDetail, bool> func = null;
                        string reportId = Convert.ToString(entiry["Id"]);
                        int schemeId = Convert.ToInt32(entiry["SchemeID_Id"]);
                        int deptOrgID = Convert.ToInt32(entiry["DeptOrgID_Id"]);
                        DynamicObjectCollection objects = entiry["BM_Sheet"] as DynamicObjectCollection;
                        if ((objects != null) && (objects.Count > 0))
                        {
                            DistributeService distributeService = new DistributeService();
                            List<string> sheetIds = (from dy in objects select dy["Id"].ToString()).ToList<string>();
                            Dictionary<string, List<int>> cycleSheetDic = distributeService.GetSheetCycles(base.Context, sheetIds, true);
                            Dictionary<string, List<DimensionValue>> dicOrgSheetDimension = new BudgetReportPlateService().GetDimesionValueBySheets(base.Context, schemeId, sheetIds, false);
                            Dictionary<int, DynamicObject> orgIdDic = CommonHelper.EntiryId2BudgetOrgId(base.Context, null, null);
                            SchemeEntityExtend schememEntify = distributeService.GetSchemDetailInfo(base.Context, schemeId, true);
                            if (func == null)
                            {
                                func = org => org.DeptOrgId == deptOrgID;
                            }
                            SchemeOrgDetail detail = schememEntify.schemeDetailList.First<SchemeOrgDetail>(func);
                            string sampleId = new BudgetReportPlateService().GetSampleId(base.Context, reportId);
                            DistributeSample sampleDetail = detail.DistributeSampleList.FirstOrDefault<DistributeSample>(sample => sample.SampleId == sampleId);
                            Dictionary<string, Dictionary<string, List<SchemeMonitorEnify>>> sheetDeleteMonitorDic = new Dictionary<string, Dictionary<string, List<SchemeMonitorEnify>>>();
                            sheetIds.ForEach(delegate (string sheetId) {
                                Func<int, bool> predicate = null;
                                Func<int, int> selector = null;
                                Func<int, DistributeDetail> func3 = null;
                                List<int> source = (from value in dicOrgSheetDimension[sheetId]
                                    where value.IsBudgetOrg
                                    select Convert.ToInt32(value.DimValue)).ToList<int>();
                                if (source.Count > 0)
                                {
                                    if (predicate == null)
                                    {
                                        predicate = id => orgIdDic[id] != null;
                                    }
                                    if (selector == null)
                                    {
                                        selector = id => Convert.ToInt32(orgIdDic[id]["orgId"]);
                                    }
                                    if (func3 == null)
                                    {
                                        func3 = detail => new DistributeDetail { CompanyId = detail, DeptOrgID = detail, OrgType = "ORG", SampleId = sampleDetail.SampleId, ReportBeginDate = sampleDetail.ReportBeginDate };
                                    }
                                    List<DistributeDetail> distrDetailList = source.Where<int>(predicate).Select<int, int>(selector).ToList<int>().Select<int, DistributeDetail>(func3).ToList<DistributeDetail>();
                                    IList<SchemeOrgDetail> schemeDetailList = distributeService.GetSchemeDetailList(this.Context, schememEntify, distrDetailList);
                                    Dictionary<string, List<SchemeMonitorEnify>> dictionary = distributeService.GetDeleteSchemeMonitor(this.Context, schememEntify, schemeDetailList, cycleSheetDic[sheetId]);
                                    sheetDeleteMonitorDic.Add(sheetId, dictionary);
                                }
                            });
                            sheetDeleteMonitorDic.ToList<KeyValuePair<string, Dictionary<string, List<SchemeMonitorEnify>>>>().ForEach(kv => distributeService.DeleteSchemeMonitor(this.Context, kv.Value, "0", false));
                        }
                    };
                }
                e.DataEntitys.ToList<DynamicObject>().ForEach(action);
            }
            base.BeginOperationTransaction(e);
        }

        public override void EndOperationTransaction(EndOperationTransactionArgs e)
        {
            List<string> list = (from obj in e.DataEntitys select Convert.ToString(obj["Id"])).ToList<string>();
            if (list.Count > 0)
            {
                new BudgetReportPlateService().DeleteBudgetValueById(base.Context, list);
            }
            base.EndOperationTransaction(e);
        }

        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            base.OnPreparePropertys(e);
            e.FieldKeys.Add("FSampleID");
            e.FieldKeys.Add("FSampleSheetId");
            e.FieldKeys.Add("FSchemeID");
            e.FieldKeys.Add("FDeptOrgID");
        }
    }
}

