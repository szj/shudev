namespace Kingdee.K3.FIN.BM.App.KDSFunc
{
    using Kingdee.BOS;
    using Kingdee.BOS.KDSReportCommon;
    using Kingdee.BOS.KDSReportEntity.BusinessClass;
    using Kingdee.BOS.KDSReportEntity.Entity;
    using Kingdee.BOS.KDSReportEntity.Expressions;
    using Kingdee.BOS.KDSReportEntity.OuterFunc;
    using Kingdee.K3.FIN.ReportEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.InteropServices;

    [Serializable, KDFunctionAttribDes("BGData")]
    public class BugdetDataFunction : KDBaseFunctionInfo
    {
        private Kingdee.BOS.Context _ctx;
        [NonSerialized]
        private IDictionary<FunctionExpression, IList<object>> _dctFuncArgs;
        private Kingdee.BOS.KDSReportEntity.BusinessClass.FormulaDefaultParams _defaultParams;
        private Kingdee.BOS.KDSContext _kdsctx;
        private ReportProperty _reportProperty;

        public override Dictionary<FunctionExpression, object> BatchEvaluate(List<FunctionExpression> listFunctions, ExpressionVisitor expressionVisitor)
        {
            Dictionary<FunctionExpression, object> dctFuncValue = new Dictionary<FunctionExpression, object>();
            IList<FunctionExpression> lstFuncsToBatch = this.IntelligentValidate(listFunctions, expressionVisitor, ref dctFuncValue);
            if ((lstFuncsToBatch != null) && (lstFuncsToBatch.Count != 0))
            {
                Dictionary<string, BudgetDataEntities> dictionary2;
                Dictionary<string, List<FunctionExpression>> dictionary3;
                this.IntelligentAnalyse(lstFuncsToBatch, expressionVisitor, out dictionary2, out dictionary3);
                this.IntelligentCaculate(dictionary3, expressionVisitor, dictionary2, ref dctFuncValue);
            }
            return dctFuncValue;
        }

        private decimal CaculFuncValue(DataTable dtSource, BudgetDataEntities funcArgs)
        {
            if (funcArgs == null)
            {
                return 0M;
            }
            return 0M;
        }

        protected override object EvaluateFunction(object[] args)
        {
            if (((args == null) || (args.Length < this.MinArgs)) || (args.Length > this.MaxArgs))
            {
                return "#VALUE?";
            }
            List<string> list = new List<string>();
            foreach (object obj2 in args)
            {
                list.Add((string) obj2);
            }
            BugdetDataFunctionArgs args2 = new BugdetDataFunctionArgs();
            Kingdee.BOS.Context namedSlotData = KdsCalcCacheMananger.GetNamedSlotData("Context") as Kingdee.BOS.Context;
            List<string> values = BugdetDataFunctionArgs.Validate(args2.ConvertToFunctionArgs(namedSlotData, list.ToArray()), namedSlotData);
            if (values.Count > 0)
            {
                return string.Format("#{0}", string.Join("", values));
            }
            KdsCalcCacheMananger.GetNamedSlotData("KDSContext");
            KdsCalcCacheMananger.GetNamedSlotData("ReportProperty");
            KdsCalcCacheMananger.GetNamedSlotData("KDSCalContext");
            object obj3 = null;
            return decimal.Parse(obj3.ToString());
        }

        private IList<object> GetFuncArgs(FunctionExpression func, ExpressionVisitor expressionVisitor)
        {
            if (this._dctFuncArgs == null)
            {
                this._dctFuncArgs = new Dictionary<FunctionExpression, IList<object>>();
            }
            if (!this._dctFuncArgs.ContainsKey(func))
            {
                this._dctFuncArgs.Add(func, func.Expressions.Select<BaseExpression, object>(new Func<BaseExpression, object>(expressionVisitor.Evaluate)).ToList<object>());
            }
            return this._dctFuncArgs[func];
        }

        private void IntelligentAnalyse(IList<FunctionExpression> lstFuncsToBatch, ExpressionVisitor expressionVisitor, out Dictionary<string, BudgetDataEntities> funcArgsGrp, out Dictionary<string, List<FunctionExpression>> funcGrp)
        {
            Kingdee.BOS.KDSReportEntity.BusinessClass.FormulaDefaultParams formulaDefaultParams = this.FormulaDefaultParams;
            funcArgsGrp = new Dictionary<string, BudgetDataEntities>();
            funcGrp = new Dictionary<string, List<FunctionExpression>>();
            foreach (FunctionExpression expression in lstFuncsToBatch)
            {
                this.GetFuncArgs(expression, expressionVisitor);
            }
        }

        private void IntelligentCaculate(Dictionary<string, List<FunctionExpression>> funcGrp, ExpressionVisitor expressionVisitor, IDictionary<string, BudgetDataEntities> dctArgs, ref Dictionary<FunctionExpression, object> dctFuncValue)
        {
            Kingdee.BOS.Context ctx = this.Context;
            Kingdee.BOS.KDSContext kDSContext = this.KDSContext;
            DataSet set = new BugdetDataFunctionService().GetBatchFunctionValue(ctx, kDSContext, dctArgs);
            IDictionary<FunctionExpression, BudgetDataEntities> dictionary = new Dictionary<FunctionExpression, BudgetDataEntities>();
            foreach (List<FunctionExpression> list in funcGrp.Values)
            {
                foreach (FunctionExpression expression in list)
                {
                    if (!dctFuncValue.ContainsKey(expression))
                    {
                        string[] args = new string[0];
                        dictionary.Add(expression, new BugdetDataFunctionArgs().ConvertToFunctionArgs(this.Context, args));
                    }
                }
            }
            foreach (KeyValuePair<string, List<FunctionExpression>> pair in funcGrp)
            {
                foreach (FunctionExpression expression2 in pair.Value)
                {
                    if (!dctFuncValue.ContainsKey(expression2))
                    {
                        DataTable dtSource = set.Tables[pair.Key];
                        decimal num = this.CaculFuncValue(dtSource, dictionary[expression2]);
                        dctFuncValue.Add(expression2, num);
                    }
                }
            }
        }

        private bool IntelligentValidate(FunctionExpression function, ExpressionVisitor expressionVisitor, ref Dictionary<FunctionExpression, object> dctFuncValue)
        {
            IList<object> funcArgs = this.GetFuncArgs(function, expressionVisitor);
            List<string> list2 = new List<string>();
            foreach (object obj2 in funcArgs)
            {
                list2.Add((string) obj2);
            }
            BugdetDataFunctionArgs args = new BugdetDataFunctionArgs();
            List<string> list3 = BugdetDataFunctionArgs.Validate(args.ConvertToFunctionArgs(this.Context, list2.ToArray()), this.Context);
            if (list3.Count > 0)
            {
                dctFuncValue.Add(function, string.Format("#{0}", list3[0]));
                return false;
            }
            return true;
        }

        private IList<FunctionExpression> IntelligentValidate(IList<FunctionExpression> lstFuncs, ExpressionVisitor expressionVisitor, ref Dictionary<FunctionExpression, object> dctFuncValue)
        {
            IList<FunctionExpression> list = new List<FunctionExpression>();
            foreach (FunctionExpression expression in lstFuncs)
            {
                if (this.IntelligentValidate(expression, expressionVisitor, ref dctFuncValue))
                {
                    list.Add(expression);
                }
            }
            return list;
        }

        private Kingdee.BOS.Context Context
        {
            get
            {
                return (this._ctx ?? (this._ctx = KdsCalcCacheMananger.GetNamedSlotData("Context") as Kingdee.BOS.Context));
            }
        }

        private Kingdee.BOS.KDSReportEntity.BusinessClass.FormulaDefaultParams FormulaDefaultParams
        {
            get
            {
                return (this._defaultParams ?? (this._defaultParams = this.reportProperty.AcctParameter));
            }
        }

        private Kingdee.BOS.KDSContext KDSContext
        {
            get
            {
                return (this._kdsctx ?? (this._kdsctx = KdsCalcCacheMananger.GetNamedSlotData("KDSContext") as Kingdee.BOS.KDSContext));
            }
        }

        public override int MaxArgs
        {
            get
            {
                return 13;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 12;
            }
        }

        public override string Name
        {
            get
            {
                return "BGData";
            }
        }

        public override bool NeedDataCache
        {
            get
            {
                return true;
            }
            set
            {
                base.NeedDataCache = value;
            }
        }

        private ReportProperty reportProperty
        {
            get
            {
                return (this._reportProperty ?? (this._reportProperty = KdsCalcCacheMananger.GetNamedSlotData("ReportProperty") as ReportProperty));
            }
        }
    }
}

