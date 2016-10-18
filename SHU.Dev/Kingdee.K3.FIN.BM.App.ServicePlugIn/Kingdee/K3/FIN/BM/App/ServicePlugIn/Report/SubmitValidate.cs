namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.Report
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SubmitValidate : AbstractValidator
    {
        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            Dictionary<int, string> source = new Dictionary<int, string>();
            source.Add(0, ResManager.LoadKDString("年", "0032056000020678", SubSystemType.FIN, new object[0]));
            source.Add(1, ResManager.LoadKDString("半年", "0032056000020679", SubSystemType.FIN, new object[0]));
            source.Add(2, ResManager.LoadKDString("季", "0032056000020680", SubSystemType.FIN, new object[0]));
            source.Add(3, ResManager.LoadKDString("月", "0032056000020681", SubSystemType.FIN, new object[0]));
            source.Add(4, ResManager.LoadKDString("旬", "0032056000020682", SubSystemType.FIN, new object[0]));
            source.Add(5, ResManager.LoadKDString("周", "0032056000020683", SubSystemType.FIN, new object[0]));
            source.Add(6, ResManager.LoadKDString("日", "0032056000020684", SubSystemType.FIN, new object[0]));
            ExtendedDataEntity[] entityArray = dataEntities;
            for (int i = 0; i < entityArray.Length; i++)
            {
                Func<KeyValuePair<int, string>, bool> predicate = null;
                ExtendedDataEntity entity = entityArray[i];
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format(ResManager.LoadKDString("组织:{0}  ", "0032056000020566", SubSystemType.FIN, new object[0]), Convert.ToString((entity.DataEntity["OrgID"] as DynamicObject)["Name"])));
                builder.Append(string.Format(ResManager.LoadKDString("预算模板:{0}  ", "0032056000020558", SubSystemType.FIN, new object[0]), entity.DataEntity["NUMBER"].ToString()));
                if (predicate == null)
                {
                    predicate = dic => ((long) dic.Key) == Convert.ToUInt32(entity.DataEntity["CycleID"]);
                }
                builder.Append(string.Format(ResManager.LoadKDString("周期:{0}  ", "0032056000020559", SubSystemType.FIN, new object[0]), source.Where<KeyValuePair<int, string>>(predicate).FirstOrDefault<KeyValuePair<int, string>>().Value));
                builder.Append(string.Format(ResManager.LoadKDString("预算年度:{0}  ", "0032056000020560", SubSystemType.FIN, new object[0]), Convert.ToString(entity.DataEntity["Year"])));
                builder.Append(string.Format(ResManager.LoadKDString("币别:{0}  ", "0032056000020561", SubSystemType.FIN, new object[0]), Convert.ToString((entity.DataEntity["CurrencyID"] as DynamicObject)["Name"])));
                builder.Append(string.Format(ResManager.LoadKDString("预算方案:{0}  ", "0032056000020562", SubSystemType.FIN, new object[0]), Convert.ToString((entity.DataEntity["SchemeID"] as DynamicObject)["NUMBER"])));
                string str = builder.ToString();
                DynamicObjectCollection objects = entity["BM_Sheet"] as DynamicObjectCollection;
                if ((objects == null) || (objects.Count <= 0))
                {
                    string message = ResManager.LoadKDString("报表未编辑，不能提交！", "0032056000020563", SubSystemType.FIN, new object[0]);
                    string title = ResManager.LoadKDString("提交：", "0032056000020564", SubSystemType.FIN, new object[0]) + str;
                    validateContext.AddError(entity, new ValidationErrorInfo("FNumber", "FRptID", entity.DataEntityIndex, 0, Convert.ToString(entity["ID"]), message, title, ErrorLevel.Error));
                }
            }
        }
    }
}

