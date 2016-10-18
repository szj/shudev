namespace Kingdee.K3.FIN.BM.ServiceHelper
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using Kingdee.K3.FIN.BM.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class DistributeServiceHelper
    {
        public static IOperationResult CancleDistributeInfoByDistrId(Context ctx, IDictionary<string, List<long>> dicDistrId)
        {
            IOperationResult result;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                result = service.CancleDistributeInfoByDistrId(ctx, dicDistrId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static IDictionary<string, bool> CheckIsSampleDistributed(Context ctx, IList<string> sampleIdList, string fid = "")
        {
            IDictionary<string, bool> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.CheckIsSampleDistributed(ctx, sampleIdList, fid);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static IDictionary<string, bool> CheckIsSchemeDistributed(Context ctx, List<string> list)
        {
            IDictionary<string, bool> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.CheckIsSchemeDistributed(ctx, list);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static IDictionary<string, bool> CheckOrgMonitorExecuteStatus(Context ctx, IList<DistributeTarget> lstTagets, int fid, DateTime reportBeginDate)
        {
            IDictionary<string, bool> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.CheckOrgMonitorExecuteStatus(ctx, lstTagets, fid, reportBeginDate);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static bool CheckOrgSchemeIdByUserd(Context ctx, string fid)
        {
            bool flag;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                flag = service.CheckOrgSchemeIdByUserd(ctx, fid);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return flag;
        }

        public static void ClearInvalidDistributedData(Context ctx, IList<string> sampleIdList, int fid)
        {
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                service.ClearInvalidDistributedData(ctx, sampleIdList, fid);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
        }

        public static IOperationResult Distribute(Context ctx, IList<DistributeSource> lstSources, IList<DistributeTarget> lstTargets)
        {
            IOperationResult result;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                result = service.Distribute(ctx, lstSources, lstTargets);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return result;
        }

        public static DynamicObjectCollection GetBudgetPeriod(Context ctx, string fId)
        {
            DynamicObjectCollection budgetPeriod;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                budgetPeriod = service.GetBudgetPeriod(ctx, fId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return budgetPeriod;
        }

        public static DateTime GetBudgetReportDate(Context ctx, string newSampleId)
        {
            DateTime budgetReportDate;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                budgetReportDate = service.GetBudgetReportDate(ctx, newSampleId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return budgetReportDate;
        }

        public static Dictionary<string, DateTime> GetBudgetReportDate(Context ctx, List<string> sampleIdList, long fid)
        {
            Dictionary<string, DateTime> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.GetBudgetReportDate(ctx, sampleIdList, fid);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static Dictionary<string, DateTime> GetBudgetReportDateByOrg(Context ctx, List<string> sampleIdList, IList<DistributeTarget> lstTagets, long fid)
        {
            Dictionary<string, DateTime> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.GetBudgetReportDateByOrg(ctx, sampleIdList, lstTagets, fid);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static DataTable GetOrgDtByDistrId(Context ctx, string dicDistrId)
        {
            DataTable orgDtByDistrId;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                orgDtByDistrId = service.GetOrgDtByDistrId(ctx, dicDistrId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return orgDtByDistrId;
        }

        public static IList<DistributeDetail_Extend> GetReportBuilStatusdByDistrId(Context ctx, IDictionary<string, List<long>> dicDistrId)
        {
            IList<DistributeDetail_Extend> reportBuilStatusdByDistrId;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                reportBuilStatusdByDistrId = service.GetReportBuilStatusdByDistrId(ctx, dicDistrId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return reportBuilStatusdByDistrId;
        }

        public static Tuple<string, bool> GetReportSampleIsMulOrg(Context ctx, string reportSampleId)
        {
            Tuple<string, bool> reportSampleIsMulOrg;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                reportSampleIsMulOrg = service.GetReportSampleIsMulOrg(ctx, reportSampleId);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return reportSampleIsMulOrg;
        }

        public static IDictionary<long, List<string>> GetSameRPTByDetail(Context ctx, IList<DistributeDetail> lstValidDetails, IList<DistributeDetail> lstValidRepeated = null)
        {
            IDictionary<long, List<string>> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.GetSameRPTByDetail(ctx, lstValidDetails, lstValidRepeated);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static Dictionary<string, ReportSample> GetSampleCycle(Context ctx, List<string> sampleList)
        {
            Dictionary<string, ReportSample> dictionary;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                dictionary = service.GetSampleCycle(ctx, sampleList, true);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return dictionary;
        }

        public static SchemeEntityExtend GetSchemDetailInfo(Context ctx, int fid)
        {
            SchemeEntityExtend extend;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                extend = service.GetSchemDetailInfo(ctx, fid, false);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return extend;
        }

        public static int SetBudgetReportBeginDate(Context ctx, IList<SchemeEnifyReportBefortSaveDto> saveDtoList)
        {
            int num;
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                num = service.SetBudgetReportBeginDate(ctx, saveDtoList);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
            return num;
        }

        public static void SetEntifySample(Context ctx, IList<DistributeSource> lstSources, long fid, string setMode = "0")
        {
            IDistributeService service = ServiceFactory.GetService<IDistributeService>(ctx);
            try
            {
                service.SetEntifySample(ctx, lstSources, fid, setMode);
            }
            finally
            {
                ServiceFactory.CloseService(service);
            }
        }
    }
}

