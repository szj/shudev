namespace Kingdee.K3.FIN.BM.App.KDSFunc
{
    using Kingdee.BOS;
    using Kingdee.BOS.App.Data;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.K3.FIN.BM.App.Core;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class CommonFunction
    {
        public static Dictionary<string, int> GetAmountUnitInfo(Context ctx)
        {
            DynamicObjectCollection objects = CommonService.GetInfoWithQueryService(ctx, "KDS_AmountUnit", "FID,FNUMBER", "FDOCUMENTSTATUS='C'", "");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (DynamicObject obj2 in objects)
            {
                dictionary.Add(Convert.ToString(obj2["FNUMBER"]), Convert.ToInt32(obj2["FID"]));
            }
            return dictionary;
        }

        public static Dictionary<string, int> GetBudgetSchemeInfo(Context ctx)
        {
            DynamicObjectCollection objects = CommonService.GetInfoWithQueryService(ctx, "BM_SCHEME", "FID,FNUMBER", "FDOCUMENTSTATUS='C'", "");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (DynamicObject obj2 in objects)
            {
                dictionary.Add(Convert.ToString(obj2["FNUMBER"]), Convert.ToInt32(obj2["FID"]));
            }
            return dictionary;
        }

        public static Dictionary<string, long> GetBugdetOrgInfo(Context ctx)
        {
            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" SELECT (CASE WHEN B.FORGTYPE='ORG' THEN C.FNUMBER ELSE D.FNUMBER END) FORGNUMBER ");
            builder.AppendLine(" ,(CASE WHEN B.FORGTYPE='ORG' THEN C.FORGID ELSE D.FDEPTID END) FORGID ");
            builder.AppendLine(" ,B.FORGTYPE ");
            builder.AppendLine(" FROM T_BM_BUDGETORG A ");
            builder.AppendLine(" JOIN T_BM_BUDGETORGENTRY B ON B.FID=A.FID ");
            builder.AppendLine(" LEFT JOIN T_ORG_ORGANIZATIONS C ON C.FORGID=B.FORGID AND B.FORGTYPE='ORG' ");
            builder.AppendLine(" LEFT JOIN T_BD_DEPARTMENT D ON D.FDEPTID=B.FORGID AND B.FORGTYPE='DEPT' ");
            builder.AppendLine(" WHERE A.FISDEFAULT='1' ");
            foreach (DynamicObject obj2 in DBUtils.ExecuteDynamicObject(ctx, builder.ToString(), null, null, CommandType.Text, new SqlParam[0]))
            {
                dictionary.Add(Convert.ToString(obj2["FORGNUMBER"]), Convert.ToInt64(obj2["FORGID"]));
            }
            return dictionary;
        }

        public static Dictionary<string, int> GetBusinessTypeInfo(Context ctx)
        {
            DynamicObjectCollection objects = CommonService.GetInfoWithQueryService(ctx, "BM_BUSINESSTYPE", "FID,FNUMBER", "FDOCUMENTSTATUS='C'", "");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (DynamicObject obj2 in objects)
            {
                dictionary.Add(Convert.ToString(obj2["FNUMBER"]), Convert.ToInt32(obj2["FID"]));
            }
            return dictionary;
        }

        public static Dictionary<string, int> GetCurrencyInfo(Context ctx)
        {
            DynamicObjectCollection objects = CommonService.GetInfoWithQueryService(ctx, "BD_Currency", "FCURRENCYID,FNUMBER", "FDOCUMENTSTATUS='C'", "");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (DynamicObject obj2 in objects)
            {
                dictionary.Add(Convert.ToString(obj2["FNUMBER"]), Convert.ToInt32(obj2["FCURRENCYID"]));
            }
            return dictionary;
        }

        public static Dictionary<string, DynamicObject> GetDataTypeInfo(Context ctx)
        {
            return CommonService.GetInfoWithQueryService(ctx, "KDS_RptItemDataType", "FDATATYPEID,FNUMBER,FDATATYPE", "FDOCUMENTSTATUS='C'", "").ToDictionary<DynamicObject, string>(p => Convert.ToString(p["FNUMBER"]));
        }
    }
}

