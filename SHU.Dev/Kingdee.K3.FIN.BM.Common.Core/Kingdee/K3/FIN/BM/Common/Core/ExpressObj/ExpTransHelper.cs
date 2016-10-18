namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Util;
    using Kingdee.BOS.Scripting;
    using Kingdee.BOS.ServiceHelper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExpTransHelper
    {
        public static ExpTransContext CreateContext(Context ctx, string formId)
        {
            if (string.IsNullOrWhiteSpace(formId))
            {
                return null;
            }
            return new ExpTransContext(((FormMetadata) MetaDataServiceHelper.Load(ctx, formId, true)).BusinessInfo);
        }

        private static string TranslateFormula(ExpTransContext expContext, string formula)
        {
            if ((expContext == null) || string.IsNullOrWhiteSpace(formula))
            {
                return "";
            }
            string text = formula;
            string str2 = formula;
            try
            {
                List<string> list = (from item in CalcExprParser.GetExprVariables(formula)
                    orderby item descending
                    select item).ToList<string>();
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                int num = 1;
                foreach (string str3 in list)
                {
                    string key = string.Format("###{0}###", num.ToString());
                    dictionary.Add(key, "{" + str3 + "}");
                    text = text.Replace(str3, key);
                    num++;
                }
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    text = text.Replace(pair.Key, pair.Value);
                }
                str2 = new DynamicText(text).Parse(expContext);
            }
            catch
            {
            }
            return str2;
        }

        public static LocaleValue TranslateFormula(Context ctx, ExpTransContext expContext, string formula, LocaleValue formulaDesc)
        {
            if ((formulaDesc != null) && !string.IsNullOrWhiteSpace(formulaDesc.ToString()))
            {
                return formulaDesc;
            }
            if (expContext == null)
            {
                return new LocaleValue();
            }
            return new LocaleValue(TranslateFormula(expContext, formula), ctx.UserLocale.LCID);
        }
    }
}

