namespace Kingdee.K3.FIN.BM.Contracts
{
    using Kingdee.BOS;
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBudgetCalendarService
    {
        bool GetCalenderBudgetState(Context ctx, int calendarId);
    }
}

