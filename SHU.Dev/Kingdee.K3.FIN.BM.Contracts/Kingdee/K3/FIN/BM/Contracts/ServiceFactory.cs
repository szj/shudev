namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using System;

    public class ServiceFactory
    {
        public static ServicesContainer _mapServer;
        private static bool noRegistered = true;

        public static void CloseService(object service)
        {
            IDisposable disposable = service as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public static T GetService<T>(Context ctx)
        {
            if (noRegistered)
            {
                RegisterService();
            }
            T service = _mapServer.GetService<T>(typeof(T).AssemblyQualifiedName, ctx.ServerUrl);
            if (service == null)
            {
                throw new KDException("???", "instance == null");
            }
            return service;
        }

        public static void RegisterService()
        {
            _mapServer = new ServicesContainer();
            lock (_mapServer)
            {
                if (noRegistered)
                {
                    _mapServer.Add(typeof(ICommonService), "Kingdee.K3.FIN.BM.App.Core.CommonService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IDistributeService), "Kingdee.K3.FIN.BM.App.Core.DistributeService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IReportOperationService), "Kingdee.K3.FIN.BM.App.Core.PivotGrid.OperationService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetMonitorService), "Kingdee.K3.FIN.BM.App.Core.BudgetMonitorService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetReportPlateService), "Kingdee.K3.FIN.BM.App.Core.BudgetReportPlateService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IReportOrgService), "Kingdee.K3.FIN.BM.App.Core.ReportOrgService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetCtrlPlateformService), "Kingdee.K3.FIN.BM.App.Core.BudgetCtrlPlateformService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetAdjust), "Kingdee.K3.FIN.BM.App.Core.BudgetAdjustService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(ICaculateService), "Kingdee.K3.FIN.BM.App.Core.Caculate.CaculateService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetFormulaService), "Kingdee.K3.FIN.BM.App.Core.BudgetFormulaService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetOrgEditService), "Kingdee.K3.FIN.BM.App.Core.BudgetOrgEditService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetCalendarService), "Kingdee.K3.FIN.BM.App.Core.BudgetCalendarService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IReportSchemeService), "Kingdee.K3.FIN.BM.App.Core.ReportSchemeService,Kingdee.K3.FIN.BM.App.Core");
                    _mapServer.Add(typeof(IBudgetDimensionService), "Kingdee.K3.FIN.BM.App.Core.BudgetDimensionService,Kingdee.K3.FIN.BM.App.Core");
                    noRegistered = false;
                }
            }
        }
    }
}

