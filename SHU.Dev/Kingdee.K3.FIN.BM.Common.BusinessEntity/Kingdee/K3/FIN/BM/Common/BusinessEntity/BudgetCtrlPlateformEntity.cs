namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudgetCtrlPlateformEntity
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!obj.GetType().Equals(base.GetType()))
            {
                return false;
            }
            BudgetCtrlPlateformEntity entity = null;
            entity = (BudgetCtrlPlateformEntity) obj;
            return this.BudgetCtrlRuleId.Equals(entity.BudgetCtrlRuleId);
        }

        public override int GetHashCode()
        {
            return this.BudgetCtrlRuleId.GetHashCode();
        }

        public int BudgetCtrlRuleId { get; set; }

        public string BudgetCtrlRuleNum { get; set; }

        public string ControlLevel { get; set; }
    }
}

