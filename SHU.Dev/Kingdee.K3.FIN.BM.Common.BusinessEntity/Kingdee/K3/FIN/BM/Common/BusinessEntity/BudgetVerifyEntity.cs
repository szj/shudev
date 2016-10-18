namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS.Core.Metadata;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BudgetVerifyEntity
    {
        public static bool CheckFeature(BudgetVerifyEntity verifyEntity, BusinessInfo businessInfo)
        {
            string id = businessInfo.GetForm().Id;
            string subsysId = businessInfo.GetForm().SubsysId;
            bool flag = false;
            if (verifyEntity.bExpenseBudget && ((subsysId == "ER") || (id == "AP_OtherPayable")))
            {
                return true;
            }
            if (verifyEntity.bCapitalBudget && ((subsysId == "8") || (subsysId == "SC")))
            {
                return true;
            }
            if (((verifyEntity.bBusinessBudget && (subsysId != "ER")) && ((subsysId != "8") && (subsysId != "SC"))) && (id != "AP_OtherPayable"))
            {
                flag = true;
            }
            return flag;
        }

        public static string GetBudgetBillFilter(BudgetVerifyEntity verifyEntity)
        {
            string str = "";
            if ((!verifyEntity.bExpenseBudget && !verifyEntity.bCapitalBudget) && !verifyEntity.bBusinessBudget)
            {
                return " 1=2 ";
            }
            if ((verifyEntity.bExpenseBudget && verifyEntity.bCapitalBudget) && verifyEntity.bBusinessBudget)
            {
                return str;
            }
            if (!verifyEntity.bBusinessBudget)
            {
                List<string> values = new List<string>();
                bool flag = false;
                if (verifyEntity.bExpenseBudget)
                {
                    flag = true;
                    values.Add("ER");
                }
                if (verifyEntity.bCapitalBudget)
                {
                    values.Add("8");
                    values.Add("SC");
                }
                if (values.Count > 0)
                {
                    str = string.Format(" FSUBSYSTEMID IN ('{0}')", string.Join("','", values));
                    if (flag)
                    {
                        str = str + " OR FBILLFORMID = 'AP_OtherPayable'";
                    }
                }
                return str;
            }
            if (!verifyEntity.bBusinessBudget)
            {
                return str;
            }
            if (!verifyEntity.bExpenseBudget)
            {
                str = str + "FSUBSYSTEMID <> 'ER' AND FBILLFORMID <> 'AP_OtherPayable'";
            }
            if (verifyEntity.bCapitalBudget)
            {
                return str;
            }
            if (str != "")
            {
                str = str + " AND ";
            }
            return (str + " FSUBSYSTEMID <>'8' AND FSUBSYSTEMID<>'SC' ");
        }

        [DefaultValue(false)]
        public bool bBusinessBudget { get; set; }

        [DefaultValue(false)]
        public bool bCapitalBudget { get; set; }

        [DefaultValue(false)]
        public bool bExpenseBudget { get; set; }
    }
}

