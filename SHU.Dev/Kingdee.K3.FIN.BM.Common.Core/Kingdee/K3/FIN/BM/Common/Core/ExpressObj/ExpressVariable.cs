namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using Kingdee.BOS.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ExpressVariable
    {
        public ExpressVariable(string currentFormId, string expressMember)
        {
            this.CurrentFormId = currentFormId;
            this.ExpressMember = expressMember;
            this.AnalysisThisExpressVariable();
        }

        private void AnalysisThisExpressVariable()
        {
            string[] itemArray = this.ExpressMember.Split(new char[] { '.' });
            this.Items = itemArray;
            this.FormId = itemArray[0];
            this.EntryKey = itemArray[1];
            this.FieldKey = itemArray[2];
            this.BaseFieldPropertyKey = this.GetBaseFieldPropertyKey(itemArray);
            this.FullExpressMember = string.Format("[{0}]", this.ExpressMember);
        }

        private string GetBaseFieldPropertyKey(string[] itemArray)
        {
            if (itemArray.Length <= 3)
            {
                return "";
            }
            List<string> values = new List<string>();
            for (int i = 3; i < itemArray.Length; i++)
            {
                values.Add(itemArray[i]);
            }
            return string.Join(".", values);
        }

        public string GetVariableRealKey()
        {
            if (!this.IsSetBaseFieldProperty())
            {
                return this.FieldKey;
            }
            return string.Format("{0}.{1}", this.FieldKey, this.BaseFieldPropertyKey);
        }

        public bool IsCurrentFormVariable()
        {
            return this.CurrentFormId.EqualsIgnoreCase(this.FormId);
        }

        public bool IsSetBaseFieldProperty()
        {
            if (this.Items.Length <= 3)
            {
                return false;
            }
            if (this.BaseFieldPropertyKey == "Id")
            {
                return false;
            }
            return true;
        }

        public string BaseFieldPropertyKey { get; private set; }

        public string CurrentFormId { get; private set; }

        public string EntryKey { get; private set; }

        public string ExpressMember { get; private set; }

        public string FieldKey { get; private set; }

        public string FormId { get; private set; }

        public string FullExpressMember { get; private set; }

        public string[] Items { get; private set; }
    }
}

