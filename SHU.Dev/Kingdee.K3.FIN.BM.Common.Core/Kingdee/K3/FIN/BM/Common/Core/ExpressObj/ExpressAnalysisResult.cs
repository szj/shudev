namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using Kingdee.BOS.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ExpressAnalysisResult
    {
        public ExpressAnalysisResult()
        {
            this.Variables = new List<ExpressVariable>();
        }

        public static ExpressAnalysisResult AnalysisExpress(string strFormId, string strExpress)
        {
            ExpressAnalysisResult result = new ExpressAnalysisResult();
            string[] strArray = strExpress.Split(new char[] { '[' });
            if (strArray.Length != 1)
            {
                foreach (string str in strArray)
                {
                    if (!str.IsEmpty())
                    {
                        string[] strArray2 = str.Split(new char[] { ']' });
                        if (strArray2.Length != 1)
                        {
                            string expressMember = strArray2[0];
                            result.Variables.Add(new ExpressVariable(strFormId, expressMember));
                        }
                    }
                }
            }
            return result;
        }

        public List<ExpressVariable> Variables { get; set; }
    }
}

