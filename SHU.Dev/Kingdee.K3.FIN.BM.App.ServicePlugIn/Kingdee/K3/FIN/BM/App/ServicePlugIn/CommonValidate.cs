namespace Kingdee.K3.FIN.BM.App.ServicePlugIn
{
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Util;
    using System;
    using System.Runtime.InteropServices;

    public class CommonValidate
    {
        public static void AddMsg(ValidateContext validateContext, ExtendedDataEntity entity, string title, string msg, string displayToFieldKey = "", ErrorLevel errorLevel = 2)
        {
            if (displayToFieldKey.IsNullOrEmptyOrWhiteSpace())
            {
                displayToFieldKey = validateContext.BusinessInfo.GetBillNoField().Key;
            }
            AddMsg(validateContext, entity, displayToFieldKey, entity["Id"].ToString(), title, msg, errorLevel);
        }

        public static void AddMsg(ValidateContext validateContext, ExtendedDataEntity entity, string displayToFieldKey, string id, string title, string msg, ErrorLevel errorLevel = 2)
        {
            ValidationErrorInfo errorInfo = new ValidationErrorInfo(displayToFieldKey, entity.DataEntity["Id"].ToString(), entity.DataEntityIndex, entity.RowIndex, id, msg, title, errorLevel);
            validateContext.AddError(entity.DataEntity, errorInfo);
        }
    }
}

