namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.Orm.DataEntity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudgetCtrlPlateformMain
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
            BudgetCtrlPlateformMain main = null;
            main = (BudgetCtrlPlateformMain) obj;
            return ((this.DeptOrgId.Equals(main.DeptOrgId) && this.OrgType.Equals(main.OrgType)) && this.SchemeId.Equals(main.SchemeId));
        }

        public override int GetHashCode()
        {
            return ((this.DeptOrgId.GetHashCode() + this.OrgType.GetHashCode()) + this.SchemeId.GetHashCode());
        }

        public int DeptOrgId { get; set; }

        public string DeptOrgName { get; set; }

        public IList<BudgetCtrlPlateformEntity> EntityList { get; set; }

        public DynamicObject EntryDeptOrg { get; set; }

        public int EntryOrgId { get; set; }

        public int FID { get; set; }

        public int OrgId { get; set; }

        public string OrgType { get; set; }

        public DynamicObject Scheme { get; set; }

        public int SchemeId { get; set; }

        public string SchemeNumber { get; set; }
    }
}

