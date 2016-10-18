namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Orm.DataEntity;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IReportSchemeService
    {
        bool CheckSampleDistribute(Context ctx, string sampleId);
        void DeleteReportScheme(Context ctx, long reportSchemeId);
        bool GetCtrlRuleBySchemeId(Context ctx, long schemeId);
        DynamicObjectCollection GetDetailAndData(Context ctx, string sampleId);
        bool GetDistributeByTempId(Context ctx, string tempId);
        Tuple<bool, int> GetVersionInfo(Context ctx, long reportSchemeId);
        bool IsReportSchemeUsed(Context ctx, long reportSchemeId);
        OperateResult RollBackReportSchemeVersion(Context ctx, long versionGroupId, int toVersion);
        IOperationResult SaveNewReportSchemeVersion(Context ctx, BusinessInfo bizInfo, DynamicObject changedObj, object editObj);
        void SetVersionGroupId(Context ctx, long rptSchemeId);
        bool UpdateDistributeTempStatus(Context ctx, string tempId, string tempStatu);
    }
}

