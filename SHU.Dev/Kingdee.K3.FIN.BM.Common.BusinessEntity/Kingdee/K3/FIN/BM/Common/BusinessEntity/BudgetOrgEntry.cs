namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS;
    using Kingdee.BOS.Orm.DataEntity;
    using System;

    public class BudgetOrgEntry : DynamicObjectView
    {
        public BudgetOrgEntry(DynamicObject obj) : base(obj)
        {
        }

        public static implicit operator BudgetOrgEntry(DynamicObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new BudgetOrgEntry(obj);
        }

        public long OrgId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["OrgId"]);
            }
            set
            {
                base.DataEntity["OrgId"] = value;
            }
        }

        public LocaleValue OrgName
        {
            get
            {
                return (LocaleValue) base.DataEntity["OrgName"];
            }
            set
            {
                base.DataEntity["OrgName"] = value;
            }
        }

        public string OrgNumber
        {
            get
            {
                return base.DataEntity["OrgNumber"].ToString();
            }
            set
            {
                base.DataEntity["OrgNumber"] = value;
            }
        }

        public string OrgType
        {
            get
            {
                return base.DataEntity["OrgType"].ToString();
            }
            set
            {
                base.DataEntity["OrgType"] = value;
            }
        }

        public long ParentOrgId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["ParentOrgId"]);
            }
            set
            {
                base.DataEntity["ParentOrgId"] = value;
            }
        }
    }
}

