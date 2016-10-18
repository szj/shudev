namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Scripting;
    using System;
    using System.Runtime.InteropServices;

    public class ExpTransContext : ExpressionContext
    {
        private BusinessInfo _info;

        public ExpTransContext(BusinessInfo info)
        {
            this._info = info;
            base.BindGetField(new TryGetValueHandler(this.TryGetValue));
            base.BindSetField(new TrySetValueHandler(this.TrySetValue));
        }

        public override bool TryGetValue(string key, out object value)
        {
            value = string.Empty;
            Element element = this._info.GetElement(key);
            if (element == null)
            {
                value = key;
            }
            else
            {
                value = element.Name.ToString();
            }
            return true;
        }

        public override bool TrySetValue(string key, object newValue)
        {
            return false;
        }
    }
}

