namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Rpc;
    using Kingdee.K3.FIN.BM.Common.BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.ServiceModel;

    [ServiceContract, RpcServiceError]
    public interface IDistributeService
    {
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IOperationResult CancleDistributeInfoByDistrId(Context ctx, IDictionary<string, List<long>> dicDistrId);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        IDictionary<string, bool> CheckIsSampleDistributed(Context ctx, IList<string> sampleIdList, string fid = "");
        [OperationContract, FaultContract(typeof(ServiceFault))]
        IDictionary<string, bool> CheckIsSchemeDistributed(Context ctx, List<string> list);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        IDictionary<string, bool> CheckOrgMonitorExecuteStatus(Context ctx, IList<DistributeTarget> lstTagets, int fid, DateTime reportBeginDate);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        bool CheckOrgSchemeIdByUserd(Context ctx, string fid);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void ClearInvalidDistributedData(Context ctx, IList<string> sampleIdList, int fid);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        IOperationResult Distribute(Context ctx, IList<DistributeSource> lstSources, IList<DistributeTarget> lstTagets);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        DynamicObjectCollection GetBudgetPeriod(Context ctx, string fId);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        DateTime GetBudgetReportDate(Context ctx, string newSampleId);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        Dictionary<string, DateTime> GetBudgetReportDate(Context ctx, List<string> sampleIdList, long fid);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        Dictionary<string, DateTime> GetBudgetReportDateByOrg(Context ctx, List<string> sampleIdList, IList<DistributeTarget> lstTagets, long fid);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IList<DistributeSampleDetail> GetDistributeSampleDetailBySample(Context ctx, IList<string> sampleIdlist);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        DataTable GetOrgDtByDistrId(Context ctx, string dicDistrId);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IList<DistributeDetail_Extend> GetReportBuilStatusdByDistrId(Context ctx, IDictionary<string, List<long>> dicDistrId);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        Tuple<string, bool> GetReportSampleIsMulOrg(Context ctx, string reportSampleId);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        IDictionary<long, List<string>> GetSameRPTByDetail(Context ctx, IList<DistributeDetail> lstValidDetails, IList<DistributeDetail> lstValidRepeated = null);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        Dictionary<string, ReportSample> GetSampleCycle(Context ctx, List<string> sampleList, bool contailMulOrg = true);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        SchemeEntityExtend GetSchemDetailInfo(Context ctx, int fid, bool visualFlag = false);
        [FaultContract(typeof(ServiceFault)), OperationContract]
        int SetBudgetReportBeginDate(Context ctx, IList<SchemeEnifyReportBefortSaveDto> saveDtoList);
        [OperationContract, FaultContract(typeof(ServiceFault))]
        void SetEntifySample(Context ctx, IList<DistributeSource> lstSources, long fid, string setMode = "0");
    }
}

