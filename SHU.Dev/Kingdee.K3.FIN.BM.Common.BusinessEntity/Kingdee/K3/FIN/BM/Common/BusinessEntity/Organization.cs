namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS;
    using Kingdee.BOS.Orm.DataEntity;
    using System;

    public class Organization : DynamicObjectView
    {
        public Organization()
        {
        }

        public Organization(DynamicObject obj) : base(obj)
        {
        }

        public static implicit operator Organization(DynamicObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new Organization(obj);
        }

        public string AcctOrgType
        {
            get
            {
                return base.DataEntity["AcctOrgType"].ToString();
            }
            set
            {
                base.DataEntity["AcctOrgType"] = value;
            }
        }

        public string Contact
        {
            get
            {
                return base.DataEntity["Contact"].ToString();
            }
            set
            {
                base.DataEntity["Contact"] = value;
            }
        }

        public string Description
        {
            get
            {
                return base.DataEntity["Description"].ToString();
            }
            set
            {
                base.DataEntity["Description"] = value;
            }
        }

        public string DocumentStatus
        {
            get
            {
                return base.DataEntity["DocumentStatus"].ToString();
            }
            set
            {
                base.DataEntity["DocumentStatus"] = value;
            }
        }

        public string ForbidStatus
        {
            get
            {
                return base.DataEntity["ForbidStatus"].ToString();
            }
            set
            {
                base.DataEntity["ForbidStatus"] = value;
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

        public bool IsAccountOrg
        {
            get
            {
                return Convert.ToBoolean(base.DataEntity["IsAccountOrg"]);
            }
            set
            {
                base.DataEntity["IsAccountOrg"] = value;
            }
        }

        public bool IsBusinessOrg
        {
            get
            {
                return Convert.ToBoolean(base.DataEntity["IsBusinessOrg"]);
            }
            set
            {
                base.DataEntity["IsBusinessOrg"] = value;
            }
        }

        public bool IsCorp
        {
            get
            {
                return (base.DataEntity["AcctOrgType"].ToString() == "1");
            }
        }

        public bool IsProfileCenter
        {
            get
            {
                return (base.DataEntity["AcctOrgType"].ToString() == "2");
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

        public string Number
        {
            get
            {
                return (string) base.DataEntity["Number"];
            }
            set
            {
                base.DataEntity["Number"] = value;
            }
        }

        public long OrgFormID
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["OrgFormID"]);
            }
            set
            {
                base.DataEntity["OrgFormID"] = value;
            }
        }

        public string OrgFunctions
        {
            get
            {
                return (string) base.DataEntity["OrgFunctions"];
            }
            set
            {
                base.DataEntity["OrgFunctions"] = value;
            }
        }

        public long ParentId
        {
            get
            {
                return Convert.ToInt64(base.DataEntity["ParentOrg_Id"]);
            }
            set
            {
                base.DataEntity["ParentOrg_Id"] = value;
            }
        }

        public string Tel
        {
            get
            {
                return base.DataEntity["Tel"].ToString();
            }
            set
            {
                base.DataEntity["Tel"] = value;
            }
        }
    }
}

