namespace Kingdee.K3.FIN.BM.Common.Core
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.SqlBuilder;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.ServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class CommonHelper
    {
        public static Dictionary<int, DynamicObject> EntiryId2BudgetOrgId(Context ctx, List<int> entryIds, DynamicObject[] budgetOrgs = null)
        {
            Dictionary<int, DynamicObject> result = new Dictionary<int, DynamicObject>();
            if (budgetOrgs == null)
            {
                budgetOrgs = GetBudgetOrgsById(ctx, 0);
            }
            if (entryIds == null)
            {
                entryIds = (from dy in budgetOrgs select Convert.ToInt32(dy["Id"])).ToList<int>();
            }
            entryIds.ForEach(delegate (int id) {
                DynamicObject obj2 = budgetOrgs.FirstOrDefault<DynamicObject>(dy => Convert.ToInt32(dy["Id"]) == id);
                if (obj2 != null)
                {
                    result.Add(id, obj2);
                }
                else
                {
                    result.Add(id, null);
                }
            });
            return result;
        }

        public static DynamicObject[] GetBudgetOrgsById(Context ctx, int fid = 0)
        {
            if (fid == 0)
            {
                BusinessInfo info = FormMetaDataCache.GetCachedFormMetaData(ctx, "BM_BudgetOrg").BusinessInfo;
                QueryBuilderParemeter paremeter = new QueryBuilderParemeter {
                    FormId = info.GetForm().Id,
                    FilterClauseWihtKey = " FForbidStatus='A' and FDocumentStatus = 'C' AND FISDEFAULT = 1 "
                };
                DynamicObject[] objArray = BusinessDataServiceHelper.Load(ctx, info.GetDynamicObjectType(), paremeter);
                if ((objArray != null) && (objArray.Length > 0))
                {
                    fid = Convert.ToInt32(objArray[0]["ID"]);
                }
            }
            BusinessInfo businessInfo = FormMetaDataCache.GetCachedFormMetaData(ctx, "BM_DEPTORG").BusinessInfo;
            QueryBuilderParemeter queryParemeter = new QueryBuilderParemeter {
                FormId = businessInfo.GetForm().Id,
                FilterClauseWihtKey = string.Format(" FID={0} ", fid)
            };
            return BusinessDataServiceHelper.Load(ctx, businessInfo.GetDynamicObjectType(), queryParemeter);
        }

        public static string GetDefaultOrgSchemeId(Context context)
        {
            string str = "";
            BusinessInfo businessInfo = FormMetaDataCache.GetCachedFormMetaData(context, "BM_BudgetOrg").BusinessInfo;
            QueryBuilderParemeter queryParemeter = new QueryBuilderParemeter {
                FormId = businessInfo.GetForm().Id,
                FilterClauseWihtKey = " FForbidStatus='A' and FDocumentStatus = 'C' AND FISDEFAULT = 1 "
            };
            DynamicObject[] objArray = BusinessDataServiceHelper.Load(context, businessInfo.GetDynamicObjectType(), queryParemeter);
            if ((objArray != null) && (objArray.Length > 0))
            {
                str = objArray[0]["ID"].ToString();
            }
            return str;
        }

        public static DynamicObject GetEntryByOrgId(Context context, int deptOrgId, int orgSchemeId = 0)
        {
            string s = string.Empty;
            if (orgSchemeId == 0)
            {
                s = GetDefaultOrgSchemeId(context);
            }
            int num = int.Parse(s);
            BusinessInfo businessInfo = FormMetaDataCache.GetCachedFormMetaData(context, "BM_DEPTORG").BusinessInfo;
            QueryBuilderParemeter queryParemeter = new QueryBuilderParemeter {
                FormId = businessInfo.GetForm().Id
            };
            string format = " FOrgId={0} AND Fid= {1}";
            string str3 = string.Format(format, deptOrgId, num);
            queryParemeter.FilterClauseWihtKey = str3;
            DynamicObject[] objArray = BusinessDataServiceHelper.Load(context, businessInfo.GetDynamicObjectType(), queryParemeter);
            if ((objArray != null) && (objArray.Length > 0))
            {
                return objArray[0];
            }
            return null;
        }

        public static int GetEntryIdByOrgId(Context context, int deptOrgId, int orgSchemeId = 0)
        {
            int num = 0;
            DynamicObject obj2 = GetEntryByOrgId(context, deptOrgId, orgSchemeId);
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2["ID"]);
            }
            return num;
        }

        public static string GetLanByCode(string code)
        {
            string name = "L_" + code;
            FieldInfo field = typeof(BMLanConst).GetField(name);
            string description = (field == null) ? code : field.GetValue(null).ToString();
            return ResManager.LoadKDString(description, code, SubSystemType.FIN, new object[0]);
        }

        public static IDictionary<int, string> GetPriodTypeCycleIdValueDic()
        {
            string str = ResManager.LoadKDString("年", "0032057000019093", SubSystemType.FIN, new object[0]);
            string str2 = ResManager.LoadKDString("半年", "0032057000019094", SubSystemType.FIN, new object[0]);
            string str3 = ResManager.LoadKDString("季", "0032057000019095", SubSystemType.FIN, new object[0]);
            string str4 = ResManager.LoadKDString("月", "0032057000019096", SubSystemType.FIN, new object[0]);
            string str5 = ResManager.LoadKDString("询", "0032057000019097", SubSystemType.FIN, new object[0]);
            string str6 = ResManager.LoadKDString("周", "0032057000019098", SubSystemType.FIN, new object[0]);
            string str7 = ResManager.LoadKDString("日", "0032057000019099", SubSystemType.FIN, new object[0]);
            string str8 = ResManager.LoadKDString("自定义", "0032057000019100", SubSystemType.FIN, new object[0]);
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(0, str);
            dictionary.Add(1, str2);
            dictionary.Add(2, str3);
            dictionary.Add(3, str4);
            dictionary.Add(4, str5);
            dictionary.Add(5, str6);
            dictionary.Add(6, str7);
            dictionary.Add(7, str8);
            return dictionary;
        }

        public static DynamicObject GetUserOption(Context ctx, string pageId, string pageOptionId)
        {
            BusinessInfo businessInfo = FormMetaDataCache.GetCachedFormMetaData(ctx, pageOptionId).BusinessInfo;
            return UserParamterServiceHelper.Load(ctx, businessInfo, ctx.UserId, pageId, "UserParameter");
        }

        public static bool IsDefaultDimension(long dimensionId)
        {
            long num = dimensionId;
            if ((num <= 0x6aL) && (num >= 100L))
            {
                switch (((int) (num - 100L)))
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return true;
                }
            }
            return false;
        }
    }
}

