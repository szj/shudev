namespace Kingdee.K3.FIN.BM.App.ServicePlugIn.ReportScheme
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using System;
    using System.Collections.Generic;

    public class SaveValidate : AbstractValidator
    {
        public override void Validate(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
        }

        private void ValidateInput(ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Context ctx)
        {
            foreach (ExtendedDataEntity entity in dataEntities)
            {
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                bool flag6 = false;
                bool flag7 = false;
                bool flag8 = false;
                DynamicObjectCollection objects = entity.DataEntity["CR_WizardItem"] as DynamicObjectCollection;
                foreach (DynamicObject obj2 in objects)
                {
                    DynamicObject obj3 = obj2["ItemDataTypeID"] as DynamicObject;
                    switch (Convert.ToInt32(obj3["DataType"]))
                    {
                        case 0:
                            flag = true;
                            break;

                        case 1:
                            flag2 = true;
                            break;
                    }
                }
                new Dictionary<long, DynamicObject>();
                DynamicObjectCollection objects2 = entity.DataEntity["CR_WizardDimension"] as DynamicObjectCollection;
                foreach (DynamicObject obj4 in objects2)
                {
                    long num4 = Convert.ToInt64(obj4["DimensionID_ID"]);
                    if ((num4 <= 0x6aL) && (num4 >= 100L))
                    {
                        switch (((int) (num4 - 100L)))
                        {
                            case 0:
                                flag3 = true;
                                break;

                            case 2:
                                flag4 = true;
                                break;

                            case 3:
                                flag5 = true;
                                break;

                            case 4:
                                flag6 = true;
                                break;

                            case 5:
                                flag7 = true;
                                break;

                            case 6:
                                flag8 = true;
                                break;
                        }
                    }
                }
                if (!flag3)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少预算组织维度", "0032056000020521", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
                if (!flag6)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少预算周期维度", "0032056000020513", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
                if (!flag7)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少预算业务类型维度", "0032056000020514", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
                if (flag && !flag4)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少币别维度，有金额数据类型则必须要有币别维度", "0032056000020523", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
                if (flag && !flag5)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少币别维度，有金额数据类型则必须要有金额单位维度", "0032056000020530", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
                if (flag2 && !flag8)
                {
                    validateContext.AddError(entity, new ValidationErrorInfo("", "", 0, 0, Convert.ToString(entity["ID"]), ResManager.LoadKDString("缺少单位维度，有数量数据类型则必须要有单位维度", "0032056000020527", SubSystemType.FIN, new object[0]), ResManager.LoadKDString("必录维度", "0032056000020522", SubSystemType.FIN, new object[0]), ErrorLevel.Error));
                }
            }
        }
    }
}

