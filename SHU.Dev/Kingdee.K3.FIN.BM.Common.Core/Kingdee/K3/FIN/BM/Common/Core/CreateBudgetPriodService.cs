namespace Kingdee.K3.FIN.BM.Common.Core
{
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    [Description("生成预算日历")]
    public class CreateBudgetPriodService
    {
        private IList<BudgetCalendarModel> CreateBudgetCalendarEntry(int priodTypeValue, DynamicObject acctPeriod, IDictionary<int, BudgetCalendarModel> currPriodRowID, int intWeekStartPoint, DynamicObject acctCarlenderObj, bool isHaveDay, string entryID, int acctYear)
        {
            TimeSpan span;
            IList<BudgetCalendarModel> list = new List<BudgetCalendarModel>();
            DateTime time = Convert.ToDateTime(acctPeriod["PERIODSTARTDATE"]);
            DateTime time2 = Convert.ToDateTime(acctPeriod["PERIODENDDATE"]);
            int num = 0;
            int num2 = 1;
            int num3 = 1;
            int num4 = 1;
            string str = string.Empty;
            int num5 = 0;
            int num6 = 0;
        Label_0415:
            span = (TimeSpan) (time2 - time);
            if (num6 <= span.Days)
            {
                currPriodRowID[priodTypeValue].PeriodStartDate = time.AddDays((double) num6);
                currPriodRowID[priodTypeValue].PeriodEndDate = time.AddDays((double) num6);
                BudgetCalendarModel item = null;
                str = (Convert.ToInt64(entryID) + num).ToString();
                if ((priodTypeValue == 4) && ((num6 == 0) || (((((time.Day + num6) / 10) >= 1) && (((time.Day + num6) % 10) == 1)) && ((time.Day + num6) < 30))))
                {
                    if ((time.Day + num6) < 11)
                    {
                        num3 = 1;
                        currPriodRowID[4].PeriodEndDate = currPriodRowID[4].PeriodStartDate.AddDays((double) (10 - currPriodRowID[4].PeriodStartDate.Day));
                    }
                    else if ((time.Day + num6) > 20)
                    {
                        num3 = 3;
                        currPriodRowID[4].PeriodEndDate = currPriodRowID[4].PeriodStartDate.AddMonths(1).AddDays((double) -currPriodRowID[4].PeriodStartDate.Day);
                    }
                    else
                    {
                        num3 = 2;
                        currPriodRowID[4].PeriodEndDate = currPriodRowID[4].PeriodStartDate.AddDays((double) (20 - currPriodRowID[4].PeriodStartDate.Day));
                    }
                    currPriodRowID[4].Period = num3;
                    currPriodRowID[4].PeriodYear = acctYear;
                    item = this.NewBudgetCalendarModel(4, currPriodRowID, intWeekStartPoint);
                    item.ID = str;
                    item.PeriodYear = acctYear;
                    currPriodRowID[4].ID = str;
                    num++;
                    num4 = 1;
                }
                else if ((priodTypeValue == 5) && ((num6 == 0) || (Convert.ToInt32(time.AddDays((double) num6).DayOfWeek) == intWeekStartPoint)))
                {
                    currPriodRowID[5].Period = num2;
                    item = this.NewBudgetCalendarModel(5, currPriodRowID, intWeekStartPoint);
                    int num7 = Convert.ToInt32(time.AddDays((double) num6).DayOfWeek);
                    if (num7 == intWeekStartPoint)
                    {
                        item.PeriodEndDate = time.AddDays((double) num6).AddDays(6.0);
                    }
                    else if (num7 > intWeekStartPoint)
                    {
                        item.PeriodEndDate = time.AddDays((double) num6).AddDays((double) ((6 - num7) + intWeekStartPoint));
                    }
                    else
                    {
                        item.PeriodEndDate = time.AddDays((double) num6).AddDays((double) ((intWeekStartPoint - num7) - 1));
                    }
                    if (item.PeriodEndDate > time2)
                    {
                        item.PeriodEndDate = time2;
                    }
                    currPriodRowID[5].PeriodYear = acctYear;
                    item.ID = str;
                    currPriodRowID[5].ID = str;
                    item.PeriodYear = acctYear;
                    num++;
                    num2++;
                    num4 = 1;
                }
                if (item != null)
                {
                    list.Add(item);
                }
                if (isHaveDay)
                {
                    num5++;
                    currPriodRowID[6].Period = num4;
                    currPriodRowID[6].PeriodStartDate = time.AddDays((double) num6);
                    currPriodRowID[6].PeriodEndDate = time.AddDays((double) num6);
                    currPriodRowID[6].PeriodYear = acctYear;
                    BudgetCalendarModel model2 = this.NewBudgetCalendarModel(6, currPriodRowID, intWeekStartPoint);
                    if (num4 == 1)
                    {
                        model2.ID = (Convert.ToInt32(str) + 1).ToString();
                        currPriodRowID[6].ID = (Convert.ToInt32(str) + 1).ToString();
                    }
                    else
                    {
                        model2.ID = str;
                        currPriodRowID[6].ID = str;
                    }
                    model2.PeriodYear = acctYear;
                    list.Add(model2);
                    num++;
                    num4++;
                }
                num6++;
                goto Label_0415;
            }
            return list;
        }

        public List<BudgetCalendarModel> CreateBudgetPriod(IList<int> selectPriodList, DynamicObject acctCarlenderObj, int intWeekStartPoint, int adjYear, string strCycleSelectType)
        {
            string str = Convert.ToString(acctCarlenderObj["PERIODTYPE"]);
            DateTime periodStartDate = Convert.ToDateTime(acctCarlenderObj["STARTDATE"]);
            DateTime periodEndDate = Convert.ToDateTime(acctCarlenderObj["ENDDATE"]);
            int num = Convert.ToInt32(acctCarlenderObj["PeriodCount"]);
            DynamicObjectCollection objects = acctCarlenderObj["BD_ACCOUNTPERIOD"] as DynamicObjectCollection;
            List<BudgetCalendarModel> currBudgetCalendarList = new List<BudgetCalendarModel>();
            bool isHaveDay = false;
            bool flag2 = true;
            IEnumerable<DynamicObject> enumerable = from p in objects
                orderby p["YEAR"], p["Seq"]
                select p;
            string entryID = string.Empty;
            int acctYear = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            IDictionary<int, BudgetCalendarModel> currPriodRowID = new Dictionary<int, BudgetCalendarModel>();
            foreach (int num6 in selectPriodList)
            {
                currPriodRowID.Add(num6, new BudgetCalendarModel(periodStartDate, periodEndDate));
            }
            foreach (DynamicObject obj2 in enumerable)
            {
                acctYear = Convert.ToInt32(obj2["YEAR"]);
                num3 = Convert.ToInt32(obj2["MONTH"]);
                num4 = Convert.ToInt32(obj2["QUARTER"]);
                num5 = Convert.ToInt32(obj2["WEEK"]);
                if (acctYear >= adjYear)
                {
                    if (num3 > 12)
                    {
                        num3 = ((num3 % 12) == 0) ? 12 : (num3 % 12);
                    }
                    if (selectPriodList.Contains(6))
                    {
                        isHaveDay = true;
                    }
                    foreach (int num7 in selectPriodList)
                    {
                        BudgetCalendarModel model;
                        IList<BudgetCalendarModel> list2;
                        int num9;
                        DateTime time3 = Convert.ToDateTime(obj2["PERIODSTARTDATE"]);
                        DateTime time4 = Convert.ToDateTime(obj2["PERIODENDDATE"]);
                        currPriodRowID[num7].PeriodType = num7;
                        currPriodRowID[num7].PeriodStartDate = time3;
                        currPriodRowID[num7].PeriodEndDate = time4;
                        flag2 = true;
                        switch (num7)
                        {
                            case 0:
                                if (currPriodRowID[num7].PeriodYear == acctYear)
                                {
                                    flag2 = false;
                                }
                                if (!flag2)
                                {
                                    goto Label_09E8;
                                }
                                if (!str.StartsWith("5"))
                                {
                                    break;
                                }
                                if (num == 0x35)
                                {
                                    time4 = time3.AddDays(371.0).AddDays(-1.0);
                                }
                                else
                                {
                                    time4 = time3.AddDays(364.0).AddDays(-1.0);
                                }
                                goto Label_0311;

                            case 1:
                                if ((str.StartsWith("5") || (num3 == 1)) || ((num3 % 7) == 0))
                                {
                                    goto Label_0352;
                                }
                                flag2 = false;
                                goto Label_0388;

                            case 2:
                                if (!str.StartsWith("5"))
                                {
                                    goto Label_0543;
                                }
                                if (num != 0x34)
                                {
                                    goto Label_0524;
                                }
                                if (((num5 % 13) != 1) && ((num5 % 0x34) != 1))
                                {
                                    goto Label_051F;
                                }
                                flag2 = true;
                                goto Label_0552;

                            case 3:
                                if (!str.Equals("5"))
                                {
                                    goto Label_06AA;
                                }
                                if (((num3 % 3) != 0) || (num5 != (((num3 / 3) * 13) - 4)))
                                {
                                    goto Label_0681;
                                }
                                flag2 = true;
                                goto Label_06C8;

                            case 4:
                            case 5:
                                entryID = this.RetrunNewCalendarEntryID(currBudgetCalendarList);
                                if (!str.Equals("5") || (num7 != 5))
                                {
                                    goto Label_0905;
                                }
                                if (num != 0x34)
                                {
                                    goto Label_07FF;
                                }
                                currPriodRowID[num7].Period = (num5 % 0x34) % 13;
                                goto Label_081A;

                            case 6:
                                if (!isHaveDay)
                                {
                                    goto Label_09E5;
                                }
                                num9 = 1;
                                goto Label_09D7;

                            default:
                                goto Label_09E8;
                        }
                        if (str.StartsWith("1"))
                        {
                            time4 = time3.AddDays((double) (-time3.Day + 1)).AddMonths(12).AddDays(-1.0);
                        }
                        else if (str.StartsWith("3"))
                        {
                            time4 = time3.AddMonths(12).AddDays(-1.0);
                        }
                    Label_0311:
                        currPriodRowID[num7].PeriodEndDate = time4;
                        currPriodRowID[num7].Period = 1;
                        goto Label_09E8;
                    Label_0352:
                        if ((str.StartsWith("5") && (num5 != 1)) && ((num5 % 0x1a) != 1))
                        {
                            flag2 = false;
                        }
                        else if (str.StartsWith("5") && ((num5 % 0x35) == 0))
                        {
                            flag2 = false;
                        }
                    Label_0388:
                        if (flag2)
                        {
                            if (str.StartsWith("5"))
                            {
                                if ((num == 0x35) && (((num7 == 5) && (num5 >= 0x1b)) || ((num7 != 5) && (num3 >= 7))))
                                {
                                    time4 = time3.AddDays(189.0).AddDays(-1.0);
                                }
                                else
                                {
                                    time4 = time3.AddDays(182.0).AddDays(-1.0);
                                }
                                if (num5 < 0x1a)
                                {
                                    currPriodRowID[num7].Period = 1;
                                }
                                else
                                {
                                    currPriodRowID[num7].Period = 2;
                                }
                            }
                            else if (str.StartsWith("1"))
                            {
                                time4 = time3.AddDays((double) (-time3.Day + 1)).AddMonths(6).AddDays(-1.0);
                                if (num3 < 6)
                                {
                                    currPriodRowID[num7].Period = 1;
                                }
                                else
                                {
                                    currPriodRowID[num7].Period = 2;
                                }
                            }
                            else if (str.StartsWith("3"))
                            {
                                time4 = time3.AddMonths(6).AddDays(-1.0);
                                if (num3 < 6)
                                {
                                    currPriodRowID[num7].Period = 1;
                                }
                                else
                                {
                                    currPriodRowID[num7].Period = 2;
                                }
                            }
                            currPriodRowID[num7].PeriodEndDate = time4;
                        }
                        goto Label_09E8;
                    Label_051F:
                        flag2 = false;
                        goto Label_0552;
                    Label_0524:
                        if (num == 0x35)
                        {
                            if (((num5 % 13) == 1) || ((num5 % 0x35) == 1))
                            {
                                flag2 = true;
                            }
                            else
                            {
                                flag2 = false;
                            }
                        }
                        goto Label_0552;
                    Label_0543:
                        if ((num3 % 3) == 1)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = false;
                        }
                    Label_0552:
                        if (flag2)
                        {
                            currPriodRowID[num7].Period = num4;
                            if (str.StartsWith("5"))
                            {
                                if ((num == 0x35) && (((num7 == 5) && (num5 > 0x34)) || ((num7 != 5) && (num4 > 3))))
                                {
                                    time4 = time3.AddDays(98.0).AddDays(-1.0);
                                }
                                else
                                {
                                    time4 = time3.AddDays(91.0).AddDays(-1.0);
                                }
                            }
                            else if (str.StartsWith("1"))
                            {
                                time4 = time3.AddDays((double) (-time3.Day + 1)).AddMonths(3).AddDays(-1.0);
                            }
                            else if (str.StartsWith("3"))
                            {
                                time4 = time3.AddMonths(3).AddDays(-1.0);
                            }
                            currPriodRowID[num7].PeriodEndDate = time4;
                        }
                        goto Label_09E8;
                    Label_0681:
                        if (((num3 % 3) != 0) && (num5 == ((((num3 % 3) * 4) + (Convert.ToInt32((int) (num3 / 3)) * 13)) - 3)))
                        {
                            flag2 = true;
                        }
                        else
                        {
                            flag2 = false;
                        }
                        goto Label_06C8;
                    Label_06AA:
                        if (((currPriodRowID[num7].Period == num3) && (num3 != 1)) && (num5 == 0))
                        {
                            flag2 = false;
                        }
                    Label_06C8:
                        if (flag2)
                        {
                            currPriodRowID[num7].Period = num3;
                            if (str.Equals("5"))
                            {
                                if ((num5 % 13) > 5)
                                {
                                    time4 = time3.AddDays(35.0).AddDays(-1.0);
                                }
                                else
                                {
                                    time4 = time3.AddDays(28.0).AddDays(-1.0);
                                }
                            }
                            else if (str.StartsWith("1"))
                            {
                                time4 = time3.AddDays((double) (-time3.Day + 1)).AddMonths(1).AddDays(-1.0);
                            }
                            else if (str.StartsWith("3"))
                            {
                                time4 = time3.AddMonths(1).AddDays(-1.0);
                            }
                            currPriodRowID[num7].PeriodEndDate = time4;
                        }
                        goto Label_09E8;
                    Label_07FF:
                        if (num == 0x34)
                        {
                            currPriodRowID[num7].Period = (num5 % 0x35) % 13;
                        }
                    Label_081A:
                        model = this.NewBudgetCalendarModel(num7, currPriodRowID, intWeekStartPoint);
                        model.ID = entryID;
                        model.PeriodYear = acctYear;
                        currPriodRowID[num7].ID = entryID;
                        currPriodRowID[num7].PeriodYear = acctYear;
                        DateTime time5 = time4.AddDays((double) (1 - time4.Day)).AddMonths(1).AddDays(-1.0);
                        int num8 = Convert.ToInt32(time4.DayOfWeek);
                        if (num8 == intWeekStartPoint)
                        {
                            time4 = time4.AddDays(6.0);
                            if (time4 > time5)
                            {
                                time4 = time5;
                            }
                        }
                        else if (num8 > intWeekStartPoint)
                        {
                            time4 = time4.AddDays((double) ((6 - num8) + intWeekStartPoint));
                        }
                        else
                        {
                            time4 = time4.AddDays((double) ((intWeekStartPoint - num8) - 1));
                        }
                        currPriodRowID[num7].PeriodEndDate = time4;
                        currBudgetCalendarList.Add(model);
                        goto Label_0927;
                    Label_0905:
                        list2 = this.CreateBudgetCalendarEntry(num7, obj2, currPriodRowID, intWeekStartPoint, acctCarlenderObj, isHaveDay, entryID, acctYear);
                        currBudgetCalendarList.AddRange(list2);
                        isHaveDay = false;
                    Label_0927:
                        flag2 = false;
                        goto Label_09E8;
                    Label_093E:
                        entryID = this.RetrunNewCalendarEntryID(currBudgetCalendarList);
                        currPriodRowID[num7].PeriodStartDate = time3;
                        currPriodRowID[num7].PeriodEndDate = time3;
                        currPriodRowID[num7].Period = num9++;
                        BudgetCalendarModel item = this.NewBudgetCalendarModel(num7, currPriodRowID, intWeekStartPoint);
                        item.ID = entryID;
                        currPriodRowID[num7].ID = entryID;
                        currPriodRowID[num7].PeriodYear = acctYear;
                        item.PeriodYear = acctYear;
                        currBudgetCalendarList.Add(item);
                        time3 = time3.AddDays(1.0);
                    Label_09D7:
                        if (time3 <= time4)
                        {
                            goto Label_093E;
                        }
                    Label_09E5:
                        flag2 = false;
                    Label_09E8:
                        if (flag2)
                        {
                            entryID = this.RetrunNewCalendarEntryID(currBudgetCalendarList);
                            currPriodRowID[num7].PeriodYear = acctYear;
                            BudgetCalendarModel model3 = this.NewBudgetCalendarModel(num7, currPriodRowID, intWeekStartPoint);
                            model3.ID = entryID;
                            currPriodRowID[num7].ID = entryID;
                            model3.PeriodYear = acctYear;
                            currBudgetCalendarList.Add(model3);
                        }
                    }
                }
            }
            return currBudgetCalendarList;
        }

        private BudgetCalendarModel NewBudgetCalendarModel(int priodTypeValue, IDictionary<int, BudgetCalendarModel> currPriodRowID, int intWeekStartPoint)
        {
            BudgetCalendarModel model = currPriodRowID[priodTypeValue];
            BudgetCalendarModel model2 = new BudgetCalendarModel();
            if (currPriodRowID.Keys.First<int>() != priodTypeValue)
            {
                int num = currPriodRowID.Keys.ElementAt<int>(currPriodRowID.Keys.ToList<int>().IndexOf(priodTypeValue) - 1);
                BudgetCalendarModel model3 = currPriodRowID[num];
                model2.ParentID = model3.ID;
                model2.ParentBudgetCalendarModel = model3;
                model.ParentID = model3.ID;
                model.ParentBudgetCalendarModel = model3;
            }
            model2.Period = currPriodRowID[priodTypeValue].Period;
            model2.PeriodType = priodTypeValue;
            model2.PeriodNumber = this.ReturnPriodFullName(priodTypeValue, currPriodRowID, 0x409);
            model2.PeriodName = this.ReturnPriodFullName(priodTypeValue, currPriodRowID, 0);
            model2.PeriodShortName = this.ReturnPriodShortName(priodTypeValue, currPriodRowID, 0);
            model2.PeriodStartDate = model.PeriodStartDate;
            model2.PeriodEndDate = model.PeriodEndDate;
            if (priodTypeValue == 4)
            {
                currPriodRowID[priodTypeValue].PeriodEndDate = model2.PeriodEndDate.AddDays((double) (((model2.Period - 1) * 10) - model2.PeriodEndDate.Day));
                return model2;
            }
            currPriodRowID[priodTypeValue].PeriodEndDate = model2.PeriodEndDate;
            return model2;
        }

        private string RetrunNewCalendarEntryID(List<BudgetCalendarModel> currBudgetCalendarList)
        {
            if (currBudgetCalendarList.Count > 0)
            {
                return Convert.ToString((int) (currBudgetCalendarList.Count + 1));
            }
            return "1";
        }

        private string ReturnPriodFullName(int priodTypeValue, IDictionary<int, BudgetCalendarModel> currPriodRowID, int LCID)
        {
            BudgetCalendarModel model = currPriodRowID[priodTypeValue];
            string str = this.ReturnPriodName(model.PeriodYear, model.Period, priodTypeValue, LCID);
            if (!model.ParentID.IsNullOrEmptyOrWhiteSpace())
            {
                BudgetCalendarModel parentBudgetCalendarModel = model.ParentBudgetCalendarModel;
                str = this.ReturnPriodFullName(parentBudgetCalendarModel.PeriodType, currPriodRowID, LCID) + str;
            }
            return str;
        }

        private string ReturnPriodName(int periodYear, int Period, int PeriodType, int LCID)
        {
            string str = string.Empty;
            switch (PeriodType)
            {
                case 0:
                    if (LCID != 0)
                    {
                        return string.Format("{0}Y", periodYear);
                    }
                    return string.Format(ResManager.LoadKDString("{0}年", "0032060000019092", SubSystemType.FIN, new object[0]), periodYear);

                case 1:
                    if (LCID != 0)
                    {
                        return string.Format("0{0}HY", Period);
                    }
                    if (Period != 1)
                    {
                        return ResManager.LoadKDString("下半年", "0032060000019091", SubSystemType.FIN, new object[0]);
                    }
                    return ResManager.LoadKDString("上半年", "0032060000019086", SubSystemType.FIN, new object[0]);

                case 2:
                    if (LCID != 0)
                    {
                        return string.Format("0{0}Q", Period);
                    }
                    return string.Format(ResManager.LoadKDString("0{0}季", "0032060000019087", SubSystemType.FIN, new object[0]), Period);

                case 3:
                    if (LCID != 0)
                    {
                        return string.Format("{0}M", (Period == 0) ? 12 : Period);
                    }
                    return string.Format(ResManager.LoadKDString("{0}月", "0032060000019088", SubSystemType.FIN, new object[0]), (Period == 0) ? 12 : Period);

                case 4:
                    if (LCID != 0)
                    {
                        return string.Format("{0}X", Period);
                    }
                    return Enum.GetName(typeof(TenDayType), Period);

                case 5:
                    if (LCID != 0)
                    {
                        return string.Format("{0}W", Period);
                    }
                    return string.Format(ResManager.LoadKDString("{0}周", "0032060000019089", SubSystemType.FIN, new object[0]), Period);

                case 6:
                    if (LCID != 0)
                    {
                        return string.Format("{0}D", Period);
                    }
                    return string.Format(ResManager.LoadKDString("{0}日", "0032060000019090", SubSystemType.FIN, new object[0]), Period);
            }
            return str;
        }

        private string ReturnPriodShortName(int priodTypeValue, IDictionary<int, BudgetCalendarModel> currPriodRowID, int LCID)
        {
            BudgetCalendarModel model = currPriodRowID[priodTypeValue];
            return this.ReturnPriodName(model.PeriodYear, model.Period, priodTypeValue, LCID);
        }

        public enum TenDayType
        {
            上旬 = 1,
            下旬 = 3,
            中旬 = 2
        }
    }
}

