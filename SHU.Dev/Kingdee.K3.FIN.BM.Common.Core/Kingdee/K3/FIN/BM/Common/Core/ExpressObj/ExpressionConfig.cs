namespace Kingdee.K3.FIN.BM.Common.Core.ExpressObj
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ExpressionConfig
    {
        public string Expression { get; set; }

        public string ExpressionAliasName { get; set; }

        public string ExpressionDesc { get; set; }

        public string ExpressionName { get; set; }

        public bool Locked { get; set; }
    }
}

