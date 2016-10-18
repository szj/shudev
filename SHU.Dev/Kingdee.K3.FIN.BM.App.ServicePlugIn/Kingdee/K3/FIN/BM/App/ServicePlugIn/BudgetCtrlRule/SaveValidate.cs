namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.BudgetCtrlRule
{
    using Kingdee.BOS;
    using Kingdee.BOS.App;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Metadata.EntityElement;
    using Kingdee.BOS.Core.Metadata.FieldElement;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SaveValidate : AbstractValidator
    {
        private Dictionary<string, FormMetadata> dicFormMetaData = new Dictionary<string, FormMetadata>();

        public FormMetadata GetFormMetaData(Context ctx, string formID)
        {
            FormMetadata metadata;
            if (!this.dicFormMetaData.TryGetValue(formID, out metadata))
            {
                metadata = ServiceHelper.GetService<IMetaDataService>().Load(base.Context, formID, true) as FormMetadata;
                this.dicFormMetaData.Add(formID, metadata);
            }
            return metadata;
        }

        private bool IsHeadEntity(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            return ((entity is SubHeadEntity) || (entity is HeadEntity));
        }

        private bool IsSubEntity(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            return (entity is SubEntryEntity);
        }

        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            if ((dataEntities != null) && (dataEntities.Length > 0))
            {
                foreach (ExtendedDataEntity entity in dataEntities)
                {
                    DynamicObjectCollection objects = entity["BM_CTRLBILL"] as DynamicObjectCollection;
                    bool flag = Convert.ToString(entity["CurrencyId"]) == "0";
                    int num = 0;
                    foreach (DynamicObject obj2 in objects)
                    {
                        num++;
                        string str = Convert.ToString(obj2["BillFormId_Id"]);
                        if (!string.IsNullOrEmpty(str))
                        {
                            FormMetadata formMetaData = this.GetFormMetaData(base.Context, str);
                            List<Field> fieldList = formMetaData.BusinessInfo.GetFieldList();
                            string key = Convert.ToString(obj2["BillDateKey"]);
                            string str3 = Convert.ToString(obj2["BillCurrencyKey"]);
                            string str4 = Convert.ToString(obj2["BillOrgKey"]);
                            string str5 = Convert.ToString(obj2["BillDeptKey"]);
                            string str6 = Convert.ToString(obj2["CtrlTime"]);
                            DynamicObject obj3 = obj2["BillFormId"] as DynamicObject;
                            string str7 = Convert.ToString(obj3["Name"]);
                            Field field = formMetaData.BusinessInfo.GetField(key);
                            Field field2 = formMetaData.BusinessInfo.GetField(str3);
                            Field field3 = formMetaData.BusinessInfo.GetField(str4);
                            if (!flag && string.IsNullOrEmpty(str3))
                            {
                                string message = string.Format(ResManager.LoadKDString("单据体“控制单据”第{0}行字段“单据币别”是必填项", "0032056000021901", SubSystemType.FIN, new object[0]), num);
                                ValidationErrorInfo errorInfo = new ValidationErrorInfo(" ", Convert.ToString(obj2["ID"]), 0, 0, " ", message, ResManager.LoadKDString("单据币别字段", "0032056000021903", SubSystemType.FIN, new object[0]), ErrorLevel.FatalError);
                                validateContext.AddError(null, errorInfo);
                            }
                            if ((field != null) && !this.IsHeadEntity(field.Entity))
                            {
                                string str9 = string.Format(ResManager.LoadKDString("预算控制单据[{0}],控制日期字段[{1}]必须在单据头！", "0032056000020504", SubSystemType.FIN, new object[0]), str7, field.Name);
                                ValidationErrorInfo info2 = new ValidationErrorInfo(field.Key, Convert.ToString(obj2["ID"]), 0, 0, " ", str9, " ", ErrorLevel.FatalError);
                                validateContext.AddError(null, info2);
                            }
                            if ((field2 != null) && !this.IsHeadEntity(field2.Entity))
                            {
                                string str10 = string.Format(ResManager.LoadKDString("预算控制单据[{0}],控制币别字段[{1}]必须在单据头！", "0032056000020505", SubSystemType.FIN, new object[0]), str7, field2.Name);
                                ValidationErrorInfo info3 = new ValidationErrorInfo(field2.Key, Convert.ToString(obj2["ID"]), 0, 0, " ", str10, " ", ErrorLevel.FatalError);
                                validateContext.AddError(null, info3);
                            }
                            switch (str6)
                            {
                                case "1":
                                case "2":
                                    if ((field3 != null) && !this.IsHeadEntity(field3.Entity))
                                    {
                                        string str11 = string.Format(ResManager.LoadKDString("预算控制单据[{0}],控制组织字段[{1}]必须在单据头！", "0032056000020499", SubSystemType.FIN, new object[0]), str7, field3.Name);
                                        ValidationErrorInfo info4 = new ValidationErrorInfo(field3.Key, Convert.ToString(obj2["ID"]), 0, 0, " ", str11, " ", ErrorLevel.FatalError);
                                        validateContext.AddError(null, info4);
                                    }
                                    if (!string.IsNullOrWhiteSpace(str5))
                                    {
                                        Field field4 = formMetaData.BusinessInfo.GetField(str5);
                                        if ((field4 != null) && !this.IsHeadEntity(field4.Entity))
                                        {
                                            string str12 = string.Format(ResManager.LoadKDString("预算控制单据[{0}],控制部门字段[{1}]必须在单据头！", "0032056000020501", SubSystemType.FIN, new object[0]), str7, field4.Name);
                                            ValidationErrorInfo info5 = new ValidationErrorInfo(field4.Key, Convert.ToString(obj2["ID"]), 0, 0, " ", str12, " ", ErrorLevel.FatalError);
                                            validateContext.AddError(null, info5);
                                        }
                                    }
                                    break;
                            }
                            DynamicObjectCollection objects2 = obj2["BM_CTRLBILLDIMENSION"] as DynamicObjectCollection;
                            DynamicObjectCollection objects3 = obj2["BM_CTRLBILLDATA"] as DynamicObjectCollection;
                            List<string> collection = (from p in objects2 select Convert.ToString(p["BillDimensionFieldKey"])).Distinct<string>().ToList<string>();
                            List<string> list3 = (from p in objects3 select Convert.ToString(p["BillDataFieldKey"])).Distinct<string>().ToList<string>();
                            List<string> source = new List<string>();
                            source.AddRange(collection);
                            source.AddRange(list3);
                            Dictionary<string, string> dictionary = source.Distinct<string>().ToDictionary<string, string>(p => p);
                            Dictionary<string, Field> dictionary2 = new Dictionary<string, Field>();
                            foreach (Field field5 in fieldList)
                            {
                                if (dictionary.ContainsKey(field5.Key) && !this.IsHeadEntity(field5.Entity))
                                {
                                    if (dictionary2.Count == 0)
                                    {
                                        dictionary2.Add(field5.EntityKey, field5);
                                    }
                                    else
                                    {
                                        Field field6 = dictionary2.FirstOrDefault<KeyValuePair<string, Field>>().Value;
                                        if (string.Compare(field6.EntityKey, field5.EntityKey, true) != 0)
                                        {
                                            string str13 = string.Format(ResManager.LoadKDString("预算控制单据[{0}],字段[{1}]所属单据体[{2}]与字段[{3}]所属单据体[{4}]不一致，不允许保存！", "0032056000020503", SubSystemType.FIN, new object[0]), new object[] { str7, field5.Name, field5.Entity.Name, field6.Name, field6.Entity.Name });
                                            ValidationErrorInfo info6 = new ValidationErrorInfo(field5.Key, Convert.ToString(obj2["ID"]), 0, 0, " ", str13, " ", ErrorLevel.FatalError);
                                            validateContext.AddError(null, info6);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ValidateField(FormMetadata metaData)
        {
        }
    }
}

