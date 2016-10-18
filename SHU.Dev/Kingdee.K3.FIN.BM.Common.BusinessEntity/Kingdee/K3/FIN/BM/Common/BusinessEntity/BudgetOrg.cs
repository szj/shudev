namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.Orm.DataEntity;
    using System;

    public class BudgetOrg : DynamicObjectView
    {
        private DynamicObjectViewCollection<BudgetOrgEntry> _items;

        public BudgetOrg(DynamicObject obj) : base(obj)
        {
        }

        public static implicit operator BudgetOrg(DynamicObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new BudgetOrg(obj);
        }

        public DynamicObjectViewCollection<BudgetOrgEntry> BudgetOrgEntrys
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new DynamicObjectViewCollection<BudgetOrgEntry>((DynamicObjectCollection) base.DataEntity["BudgetOrgEntry"]);
                }
                return this._items;
            }
        }

        public long Id
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["Id"]);
            }
            set
            {
                base.DataEntity["Id"] = value;
            }
        }

        public Organization RootOrg
        {
            get
            {
                return (DynamicObject) base.DataEntity["RootOrg"];
            }
        }

        public long RootOrgId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["RootOrg_Id"]);
            }
        }
    }
}

