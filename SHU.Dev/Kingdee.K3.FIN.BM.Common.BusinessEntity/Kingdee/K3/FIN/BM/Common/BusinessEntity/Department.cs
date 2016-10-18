namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS;
    using Kingdee.BOS.Orm.DataEntity;
    using System;

    public class Department : DynamicObjectView
    {
        public Department()
        {
        }

        public Department(DynamicObject obj) : base(obj)
        {
        }

        public static implicit operator Department(DynamicObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new Department(obj);
        }

        public long CreateOrgId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["CreateOrgId"]);
            }
            set
            {
                base.DataEntity["CreateOrgId"] = value;
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

        public LocaleValue Name
        {
            get
            {
                return (LocaleValue) base.DataEntity["Name"];
            }
            set
            {
                base.DataEntity["Name"] = value;
            }
        }

        public long UseOrgId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["UseOrgId"]);
            }
            set
            {
                base.DataEntity["UseOrgId"] = value;
            }
        }
    }
}

