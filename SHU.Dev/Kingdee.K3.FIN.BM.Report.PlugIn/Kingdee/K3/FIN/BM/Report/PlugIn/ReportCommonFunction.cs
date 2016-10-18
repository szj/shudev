namespace Kingdee.K3.FIN.BM.Report.PlugIn
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.CommonFilter;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.SqlBuilder;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.ServiceHelper;
    using System;
    using System.Collections.Generic;

    public class ReportCommonFunction
    {
        public static void CheckDefaultScheme(IDynamicFormView view, IDynamicFormModel model, ButtonClickEventArgs e, string curFilterSchemeId, string defaultSchemeId)
        {
            if (curFilterSchemeId == defaultSchemeId)
            {
                bool flag = false;
                if (model.GetValue("FChkScheme") != null)
                {
                    flag = Convert.ToBoolean(model.GetValue("FChkScheme"));
                }
                if (flag)
                {
                    view.ShowMessage(ResManager.LoadKDString("默认方案不能勾选“下次以此方案自动进入”，请去掉此选择再进行操作！", "003192000011320", SubSystemType.FIN, new object[0]), MessageBoxType.Notice);
                    e.Cancel = true;
                }
            }
        }

        public static string GetDefaultSchemeId(Context ctx, IDynamicFormModel model, string filterSchemeFormId, string defaultSchemeId)
        {
            filterSchemeFormId = (model as ICommonFilterModelService).FormId;
            if (!string.IsNullOrWhiteSpace(filterSchemeFormId))
            {
                defaultSchemeId = GetSchemeId(ctx, filterSchemeFormId);
            }
            return defaultSchemeId;
        }

        public static string GetSchemeId(Context ctx, string filterSchemeFormId)
        {
            QueryBuilderParemeter paremeter2 = new QueryBuilderParemeter {
                FormId = "BOS_FilterScheme",
                SelectItems = SelectorItemInfo.CreateItems("FSCHEMEID")
            };
            QueryBuilderParemeter para = paremeter2;
            para.FilterClauseWihtKey = string.Format("FFormID=@FFormID and FIsDefault='1' and FUserId = -1", new object[0]);
            List<SqlParam> paramList = new List<SqlParam> {
                new SqlParam("@FFormID", 0x10, filterSchemeFormId)
            };
            DynamicObjectCollection objects = QueryServiceHelper.GetDynamicObjectCollection(ctx, para, paramList);
            string str = string.Empty;
            if ((objects != null) && (objects.Count != 0))
            {
                return objects[0]["FSCHEMEID"].ToString();
            }
            return str;
        }
    }
}

