namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ExpressionConst
    {
        public ExpressionConst(string expressionName, string expression, string expressionDesc)
        {
            this.ExpressionName = expressionName;
            this.Expression = expression;
            this.ExpressionDesc = expressionDesc;
        }

        public string Expression { get; private set; }

        public string ExpressionAliasName { get; set; }

        public string ExpressionDesc { get; private set; }

        public string ExpressionName { get; private set; }
    }
}

