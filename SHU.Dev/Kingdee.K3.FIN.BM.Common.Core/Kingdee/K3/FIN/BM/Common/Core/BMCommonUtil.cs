namespace Kingdee.K3.FIN.BM.Common.Core
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.CommonFilter;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BMCommonUtil
    {
        private static void AddNodes(List<string> lstOrgIds, TreeNode currentNode)
        {
            foreach (TreeNode node in currentNode.children)
            {
                lstOrgIds.Add(node.id);
                AddNodes(lstOrgIds, node);
            }
        }

        public static T DeepClone<T>(T obj)
        {
            T local = default(T);
            if (obj == null)
            {
                return local;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0L;
                return (T) formatter.Deserialize(stream);
            }
        }

        public static Dictionary<string, int> GetDemoVersionBillCount()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("BM_Report", 5);
            dictionary.Add("BM_BudgetAdjust", 5);
            dictionary.Add("BM_BudgetCtrlRule", 5);
            dictionary.Add("BM_SCHEME", 3);
            return dictionary;
        }

        public static FilterParameter GetDeptOrgTreeFilter(List<string> lstOrgIds, bool isDeptOrg)
        {
            FilterParameter parameter = new FilterParameter();
            string str = isDeptOrg ? "FDEPTORGID" : "FORGID";
            parameter.FilterString = string.Format("{0} IN({1})", str, string.Join(",", lstOrgIds));
            foreach (string str2 in lstOrgIds)
            {
                parameter.SelectedGroupIds.Add(str2, str);
            }
            return parameter;
        }

        public static EnumItem GetEnumItem(Context ctx, string cycle)
        {
            return new EnumItem { Seq = Convert.ToInt32(cycle), EnumId = cycle, Value = cycle, Caption = GetPeriodLocaleValue(ctx, cycle) };
        }

        public static List<EnumItem> GetEnumItemsByCalendar(Context ctx, DynamicObject Canlenda)
        {
            List<EnumItem> list = new List<EnumItem>();
            if (Canlenda != null)
            {
                if (bool.Parse(Canlenda["Year"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "0"));
                }
                if (bool.Parse(Canlenda["HALFOFYEAR"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "1"));
                }
                if (bool.Parse(Canlenda["SEASON"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "2"));
                }
                if (bool.Parse(Canlenda["MONTH"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "3"));
                }
                if (bool.Parse(Canlenda["TENDAYS"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "4"));
                }
                if (bool.Parse(Canlenda["WEEKS"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "5"));
                }
                if (bool.Parse(Canlenda["DAYS"].ToString()))
                {
                    list.Add(GetEnumItem(ctx, "6"));
                }
            }
            return list;
        }

        public static List<string> GetLowerOrgs(List<string> lstOrgIds, TreeNode currentNode, string selectOrgId)
        {
            if (currentNode != null)
            {
                if (currentNode.id == selectOrgId)
                {
                    lstOrgIds.Add(currentNode.id);
                    AddNodes(lstOrgIds, currentNode);
                    return lstOrgIds;
                }
                foreach (TreeNode node in currentNode.children)
                {
                    GetLowerOrgs(lstOrgIds, node, selectOrgId);
                }
            }
            return lstOrgIds;
        }

        public static LocaleValue GetPeriodLocaleValue(Context ctx, string cycle)
        {
            switch (cycle)
            {
                case "0":
                    return new LocaleValue(ResManager.LoadKDString("年", "0032057000019093", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "1":
                    return new LocaleValue(ResManager.LoadKDString("半年", "0032057000019094", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "2":
                    return new LocaleValue(ResManager.LoadKDString("季", "0032057000019095", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "3":
                    return new LocaleValue(ResManager.LoadKDString("月", "0032057000019096", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "4":
                    return new LocaleValue(ResManager.LoadKDString("旬", "0032057000020338", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "5":
                    return new LocaleValue(ResManager.LoadKDString("周", "0032057000019098", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);

                case "6":
                    return new LocaleValue(ResManager.LoadKDString("天", "0032057000020339", SubSystemType.FIN, new object[0]), ctx.UserLocale.LCID);
            }
            return new LocaleValue("");
        }

        public static T GetValue<T>(IDynamicFormView view, string fieldName, T defaultValue = null)
        {
            try
            {
                object obj2 = view.Model.GetValue(fieldName);
                if ((obj2 != null) && !string.IsNullOrWhiteSpace(obj2.ToString()))
                {
                    return (T) Convert.ChangeType(obj2, typeof(T));
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}

