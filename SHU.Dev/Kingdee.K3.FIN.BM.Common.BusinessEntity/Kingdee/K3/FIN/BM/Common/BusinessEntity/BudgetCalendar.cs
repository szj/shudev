namespace Kingdee.K3.FIN.BM.Common.BusinessEntity
{
    using Kingdee.BOS;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.Util;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Orm.Metadata.DataEntity;
    using System;
    using System.Data;

    public class BudgetCalendar : NameNumberView
    {
        private DynamicObjectViewCollection<BudgetPeriodEntity> _BudgetPeriodEntityList;
        public static readonly DynamicProperty ACId_IdProperty;
        public static readonly DynamicProperty ACIdProperty;
        public static readonly DynamicProperty AouditorId_IdProperty;
        public static readonly DynamicProperty AouditorIdProperty;
        public static readonly DynamicProperty AuditDateProperty;
        protected static readonly DynamicObjectType BD_ACCOUNTPERIODType;
        protected static readonly DynamicObjectType BM_BUDGETCALENDARType;
        public static readonly DynamicProperty BudgetCalendarTypeProperty;
        public static readonly DynamicProperty BudgetPeriodEntityListProperty;
        protected static readonly DynamicObjectType BudgetPeriodEntityType;
        public static readonly DynamicProperty CreateDateProperty;
        public static readonly DynamicProperty CreatorId_IdProperty;
        public static readonly DynamicProperty CreatorIdProperty;
        public static readonly DynamicProperty CycleSelectTypeProperty;
        public static readonly DynamicProperty DAYSProperty;
        public static readonly DynamicProperty DescriptionProperty;
        public static readonly DynamicProperty DocumentStatusProperty;
        public static readonly DynamicProperty EndDateProperty;
        public static readonly DynamicProperty FModifyDateProperty;
        public static readonly DynamicProperty ForbidDateProperty;
        public static readonly DynamicProperty ForbidderId_IdProperty;
        public static readonly DynamicProperty ForbidderIdProperty;
        public static readonly DynamicProperty ForbidStatusProperty;
        public static readonly DynamicProperty HALFOFYEARProperty;
        public static readonly DynamicProperty IdProperty;
        public static readonly DynamicProperty ModifierId_IdProperty;
        public static readonly DynamicProperty ModifierIdProperty;
        public static readonly DynamicProperty MONTHProperty;
        protected static readonly DynamicObjectType ORMBD_ACCOUNType;
        public static readonly DynamicProperty SEASONProperty;
        public static readonly DynamicProperty StartDateProperty;
        public static readonly DynamicProperty TENDAYSProperty;
        protected static readonly DynamicObjectType UserType;
        public static readonly DynamicProperty WEEKSProperty;
        public static readonly DynamicProperty YearProperty;

        static BudgetCalendar()
        {
            object[] attributes = new object[1];
            DataEntityTypeAttribute attribute = new DataEntityTypeAttribute {
                Alias = "T_BM_BUDGETCALENDAR"
            };
            attributes[0] = attribute;
            BM_BUDGETCALENDARType = new DynamicObjectType("BM_BUDGETCALENDAR", NameNumberView.NameNumberViewType, null, DataEntityTypeFlag.Class, attributes);
            object[] objArray2 = new object[1];
            DataEntityTypeAttribute attribute2 = new DataEntityTypeAttribute {
                Alias = "T_BD_ACCOUNTPERIOD"
            };
            objArray2[0] = attribute2;
            BD_ACCOUNTPERIODType = new DynamicObjectType("BD_ACCOUNTPERIOD", IdView.IdViewType, null, DataEntityTypeFlag.Class, objArray2);
            object[] objArray3 = new object[1];
            DataEntityTypeAttribute attribute3 = new DataEntityTypeAttribute {
                Alias = "T_BM_BUDGETPERIOD"
            };
            objArray3[0] = attribute3;
            BudgetPeriodEntityType = new DynamicObjectType("BudgetPeriodEntity", IdView.IdViewType, null, DataEntityTypeFlag.Class, objArray3);
            object[] objArray4 = new object[1];
            DataEntityTypeAttribute attribute4 = new DataEntityTypeAttribute {
                Alias = "T_BD_ACCOUNTCALENDAR"
            };
            objArray4[0] = attribute4;
            ORMBD_ACCOUNType = new DynamicObjectType("ORMBD_ACCOUN", NameNumberMasterIdView.NameNumberMasterIdViewType, null, DataEntityTypeFlag.Class, objArray4);
            object[] objArray5 = new object[1];
            DataEntityTypeAttribute attribute5 = new DataEntityTypeAttribute {
                Alias = "T_SEC_user"
            };
            objArray5[0] = attribute5;
            UserType = new DynamicObjectType("User", IdView.IdViewType, null, DataEntityTypeFlag.Class, objArray5);
            ACIdProperty = BM_BUDGETCALENDARType.RegisterComplexProperty("ACId", ORMBD_ACCOUNType, false, new object[] { new DbIgnoreAttribute() });
            object[] objArray7 = new object[1];
            SimplePropertyAttribute attribute6 = new SimplePropertyAttribute {
                Alias = "FACID"
            };
            objArray7[0] = attribute6;
            ACId_IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("ACId_Id", typeof(long), null, false, objArray7);
            AouditorIdProperty = BM_BUDGETCALENDARType.RegisterComplexProperty("AouditorId", UserType, false, new object[] { new DbIgnoreAttribute() });
            object[] objArray9 = new object[1];
            SimplePropertyAttribute attribute7 = new SimplePropertyAttribute {
                Alias = "FAUDITORID"
            };
            objArray9[0] = attribute7;
            AouditorId_IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("AouditorId_Id", typeof(long), null, false, objArray9);
            object[] objArray10 = new object[1];
            SimplePropertyAttribute attribute8 = new SimplePropertyAttribute {
                Alias = "FAUDITDATE"
            };
            objArray10[0] = attribute8;
            AuditDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("AuditDate", typeof(DateTime?), null, false, objArray10);
            object[] objArray11 = new object[1];
            SimplePropertyAttribute attribute9 = new SimplePropertyAttribute {
                Alias = "FBUDGETCALENDARTYPE"
            };
            objArray11[0] = attribute9;
            BudgetCalendarTypeProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("BudgetCalendarType", typeof(string), null, false, objArray11);
            BudgetPeriodEntityListProperty = BM_BUDGETCALENDARType.RegisterCollectionProperty("BudgetPeriodEntity", BudgetPeriodEntityType, null, new object[0]);
            object[] objArray12 = new object[1];
            SimplePropertyAttribute attribute10 = new SimplePropertyAttribute {
                Alias = "FCREATEDATE"
            };
            objArray12[0] = attribute10;
            CreateDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("CreateDate", typeof(DateTime?), null, false, objArray12);
            CreatorIdProperty = BM_BUDGETCALENDARType.RegisterComplexProperty("CreatorId", UserType, false, new object[] { new DbIgnoreAttribute() });
            object[] objArray14 = new object[1];
            SimplePropertyAttribute attribute11 = new SimplePropertyAttribute {
                Alias = "FCREATORID"
            };
            objArray14[0] = attribute11;
            CreatorId_IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("CreatorId_Id", typeof(long), null, false, objArray14);
            object[] objArray15 = new object[1];
            SimplePropertyAttribute attribute12 = new SimplePropertyAttribute {
                Alias = "FCYCLESELECTTYPE"
            };
            objArray15[0] = attribute12;
            CycleSelectTypeProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("CycleSelectType", typeof(string), null, false, objArray15);
            object[] objArray16 = new object[1];
            SimplePropertyAttribute attribute13 = new SimplePropertyAttribute {
                Alias = "FDAYS",
                DbType = DbType.StringFixedLength
            };
            objArray16[0] = attribute13;
            DAYSProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("DAYS", typeof(bool), null, false, objArray16);
            object[] objArray17 = new object[2];
            SimplePropertyAttribute attribute14 = new SimplePropertyAttribute {
                Alias = "FDESCRIPTION"
            };
            objArray17[0] = attribute14;
            objArray17[1] = new DbIgnoreAttribute();
            DescriptionProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("Description", typeof(LocaleValue), null, false, objArray17);
            object[] objArray18 = new object[1];
            SimplePropertyAttribute attribute15 = new SimplePropertyAttribute {
                Alias = "FDOCUMENTSTATUS"
            };
            objArray18[0] = attribute15;
            DocumentStatusProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("DocumentStatus", typeof(string), null, false, objArray18);
            object[] objArray19 = new object[1];
            SimplePropertyAttribute attribute16 = new SimplePropertyAttribute {
                Alias = "FENDDATE"
            };
            objArray19[0] = attribute16;
            EndDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("EndDate", typeof(DateTime?), null, false, objArray19);
            object[] objArray20 = new object[1];
            SimplePropertyAttribute attribute17 = new SimplePropertyAttribute {
                Alias = "FMODIFYDATE"
            };
            objArray20[0] = attribute17;
            FModifyDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("FModifyDate", typeof(DateTime?), null, false, objArray20);
            object[] objArray21 = new object[1];
            SimplePropertyAttribute attribute18 = new SimplePropertyAttribute {
                Alias = "FFORBIDDATE"
            };
            objArray21[0] = attribute18;
            ForbidDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("ForbidDate", typeof(DateTime?), null, false, objArray21);
            ForbidderIdProperty = BM_BUDGETCALENDARType.RegisterComplexProperty("ForbidderId", UserType, false, new object[] { new DbIgnoreAttribute() });
            object[] objArray23 = new object[1];
            SimplePropertyAttribute attribute19 = new SimplePropertyAttribute {
                Alias = "FFORBIDDERID"
            };
            objArray23[0] = attribute19;
            ForbidderId_IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("ForbidderId_Id", typeof(long), null, false, objArray23);
            object[] objArray24 = new object[1];
            SimplePropertyAttribute attribute20 = new SimplePropertyAttribute {
                Alias = "FFORBIDSTATUS"
            };
            objArray24[0] = attribute20;
            ForbidStatusProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("ForbidStatus", typeof(string), null, false, objArray24);
            object[] objArray25 = new object[1];
            SimplePropertyAttribute attribute21 = new SimplePropertyAttribute {
                Alias = "FHALFOFYEAR",
                DbType = DbType.StringFixedLength
            };
            objArray25[0] = attribute21;
            HALFOFYEARProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("HALFOFYEAR", typeof(bool), null, false, objArray25);
            object[] objArray26 = new object[1];
            SimplePropertyAttribute attribute22 = new SimplePropertyAttribute(true) {
                Alias = "FID"
            };
            objArray26[0] = attribute22;
            IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("Id", typeof(long), null, false, objArray26);
            ModifierIdProperty = BM_BUDGETCALENDARType.RegisterComplexProperty("ModifierId", UserType, false, new object[] { new DbIgnoreAttribute() });
            object[] objArray28 = new object[1];
            SimplePropertyAttribute attribute23 = new SimplePropertyAttribute {
                Alias = "FMODIFIERID"
            };
            objArray28[0] = attribute23;
            ModifierId_IdProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("ModifierId_Id", typeof(long), null, false, objArray28);
            object[] objArray29 = new object[1];
            SimplePropertyAttribute attribute24 = new SimplePropertyAttribute {
                Alias = "FMONTH",
                DbType = DbType.StringFixedLength
            };
            objArray29[0] = attribute24;
            MONTHProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("MONTH", typeof(bool), null, false, objArray29);
            object[] objArray30 = new object[1];
            SimplePropertyAttribute attribute25 = new SimplePropertyAttribute {
                Alias = "FSEASON",
                DbType = DbType.StringFixedLength
            };
            objArray30[0] = attribute25;
            SEASONProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("SEASON", typeof(bool), null, false, objArray30);
            object[] objArray31 = new object[1];
            SimplePropertyAttribute attribute26 = new SimplePropertyAttribute {
                Alias = "FSTARTDATE"
            };
            objArray31[0] = attribute26;
            StartDateProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("StartDate", typeof(DateTime?), null, false, objArray31);
            object[] objArray32 = new object[1];
            SimplePropertyAttribute attribute27 = new SimplePropertyAttribute {
                Alias = "FTENDAYS",
                DbType = DbType.StringFixedLength
            };
            objArray32[0] = attribute27;
            TENDAYSProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("TENDAYS", typeof(bool), null, false, objArray32);
            attributes = new object[1];
            SimplePropertyAttribute attribute28 = new SimplePropertyAttribute {
                Alias = "FWEEKS",
                DbType = DbType.StringFixedLength
            };
            attributes[0] = attribute28;
            WEEKSProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("WEEKS", typeof(bool), null, false, attributes);
            attributes = new object[1];
            SimplePropertyAttribute attribute29 = new SimplePropertyAttribute {
                Alias = "FYEAR",
                DbType = DbType.StringFixedLength
            };
            attributes[0] = attribute29;
            YearProperty = BM_BUDGETCALENDARType.RegisterSimpleProperty("Year", typeof(bool), null, false, attributes);
        }

        public BudgetCalendar(DynamicObject obj) : base(obj)
        {
        }

        public BudgetCalendar(IDynamicFormModel model, DynamicObject dataEntity) : base(model, dataEntity)
        {
        }

        public static implicit operator BudgetCalendar(DynamicObject obj)
        {
            if (obj != null)
            {
                return new BudgetCalendar(obj);
            }
            return null;
        }

        public ORMBD_ACCOUN ACId
        {
            get
            {
                return (DynamicObject) ACIdProperty.GetValue(base.DataEntity);
            }
        }

        public long ACId_Id
        {
            get
            {
                return (long) ACId_IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(ACId_IdProperty, "FACId", base.DataEntity, value);
            }
        }

        public User AouditorId
        {
            get
            {
                return (DynamicObject) AouditorIdProperty.GetValue(base.DataEntity);
            }
        }

        public long AouditorId_Id
        {
            get
            {
                return (long) AouditorId_IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(AouditorId_IdProperty, "FAuditorId", base.DataEntity, value);
            }
        }

        public DateTime? AuditDate
        {
            get
            {
                return (DateTime?) AuditDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(AuditDateProperty, "FAuditDate", base.DataEntity, value);
            }
        }

        public string BudgetCalendarType
        {
            get
            {
                return (string) BudgetCalendarTypeProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(BudgetCalendarTypeProperty, "FBudgetCalendarType", base.DataEntity, value);
            }
        }

        public DynamicObjectViewCollection<BudgetPeriodEntity> BudgetPeriodEntityList
        {
            get
            {
                this._BudgetPeriodEntityList = this._BudgetPeriodEntityList ?? new DynamicObjectViewCollection4Model<BudgetPeriodEntity>(base.Model, "FBudgetPeriodEntity", (DynamicObjectCollection) BudgetPeriodEntityListProperty.GetValue(base.DataEntity));
                return this._BudgetPeriodEntityList;
            }
        }

        public DateTime? CreateDate
        {
            get
            {
                return (DateTime?) CreateDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(CreateDateProperty, "FCreateDate", base.DataEntity, value);
            }
        }

        public User CreatorId
        {
            get
            {
                return (DynamicObject) CreatorIdProperty.GetValue(base.DataEntity);
            }
        }

        public long CreatorId_Id
        {
            get
            {
                return (long) CreatorId_IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(CreatorId_IdProperty, "FCreatorId", base.DataEntity, value);
            }
        }

        public string CycleSelectType
        {
            get
            {
                return (string) CycleSelectTypeProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(CycleSelectTypeProperty, "FCycleSelectType", base.DataEntity, value);
            }
        }

        public bool DAYS
        {
            get
            {
                return (bool) DAYSProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(DAYSProperty, "FDAYS", base.DataEntity, value);
            }
        }

        public LocaleValue Description
        {
            get
            {
                return (LocaleValue) DescriptionProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(DescriptionProperty, "FDescription", base.DataEntity, value);
            }
        }

        public string DocumentStatus
        {
            get
            {
                return (string) DocumentStatusProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(DocumentStatusProperty, "FDocumentStatus", base.DataEntity, value);
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return (DateTime?) EndDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(EndDateProperty, "FEndDate", base.DataEntity, value);
            }
        }

        public DateTime? FModifyDate
        {
            get
            {
                return (DateTime?) FModifyDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(FModifyDateProperty, "FModifyDate", base.DataEntity, value);
            }
        }

        public DateTime? ForbidDate
        {
            get
            {
                return (DateTime?) ForbidDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(ForbidDateProperty, "FForbidDate", base.DataEntity, value);
            }
        }

        public User ForbidderId
        {
            get
            {
                return (DynamicObject) ForbidderIdProperty.GetValue(base.DataEntity);
            }
        }

        public long ForbidderId_Id
        {
            get
            {
                return (long) ForbidderId_IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(ForbidderId_IdProperty, "FForbidderId", base.DataEntity, value);
            }
        }

        public string ForbidStatus
        {
            get
            {
                return (string) ForbidStatusProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(ForbidStatusProperty, "FForbidStatus", base.DataEntity, value);
            }
        }

        public bool HALFOFYEAR
        {
            get
            {
                return (bool) HALFOFYEARProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(HALFOFYEARProperty, "FHALFOFYEAR", base.DataEntity, value);
            }
        }

        public long Id
        {
            get
            {
                return (long) IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                IdProperty.SetValue(base.DataEntity, value);
            }
        }

        public User ModifierId
        {
            get
            {
                return (DynamicObject) ModifierIdProperty.GetValue(base.DataEntity);
            }
        }

        public long ModifierId_Id
        {
            get
            {
                return (long) ModifierId_IdProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(ModifierId_IdProperty, "FModifierId", base.DataEntity, value);
            }
        }

        public bool MONTH
        {
            get
            {
                return (bool) MONTHProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(MONTHProperty, "FMONTH", base.DataEntity, value);
            }
        }

        public bool SEASON
        {
            get
            {
                return (bool) SEASONProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(SEASONProperty, "FSEASON", base.DataEntity, value);
            }
        }

        public DateTime? StartDate
        {
            get
            {
                return (DateTime?) StartDateProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(StartDateProperty, "FStartDate", base.DataEntity, value);
            }
        }

        public bool TENDAYS
        {
            get
            {
                return (bool) TENDAYSProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(TENDAYSProperty, "FTENDAYS", base.DataEntity, value);
            }
        }

        public bool WEEKS
        {
            get
            {
                return (bool) WEEKSProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(WEEKSProperty, "FWEEKS", base.DataEntity, value);
            }
        }

        public bool Year
        {
            get
            {
                return (bool) YearProperty.GetValue(base.DataEntity);
            }
            set
            {
                base.SetValue(YearProperty, "FYear", base.DataEntity, value);
            }
        }

        public class BD_ACCOUNTPERIOD : IdView
        {
            public static readonly DynamicProperty MONTHProperty;
            public static readonly DynamicProperty PERIODENDDATEProperty;
            public static readonly DynamicProperty PERIODProperty;
            public static readonly DynamicProperty PERIODSTARTDATEProperty;
            public static readonly DynamicProperty QUARTERProperty;
            public static readonly DynamicProperty SeqProperty;
            public static readonly DynamicProperty WEEKProperty;
            public static readonly DynamicProperty YEARProperty;

            static BD_ACCOUNTPERIOD()
            {
                object[] attributes = new object[1];
                SimplePropertyAttribute attribute = new SimplePropertyAttribute {
                    Alias = "FMONTH"
                };
                attributes[0] = attribute;
                MONTHProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("MONTH", typeof(long), null, false, attributes);
                object[] objArray2 = new object[1];
                SimplePropertyAttribute attribute2 = new SimplePropertyAttribute {
                    Alias = "FPERIOD"
                };
                objArray2[0] = attribute2;
                PERIODProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("PERIOD", typeof(long), null, false, objArray2);
                object[] objArray3 = new object[1];
                SimplePropertyAttribute attribute3 = new SimplePropertyAttribute {
                    Alias = "FPERIODENDDATE"
                };
                objArray3[0] = attribute3;
                PERIODENDDATEProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("PERIODENDDATE", typeof(DateTime?), null, false, objArray3);
                object[] objArray4 = new object[1];
                SimplePropertyAttribute attribute4 = new SimplePropertyAttribute {
                    Alias = "FPERIODSTARTDATE"
                };
                objArray4[0] = attribute4;
                PERIODSTARTDATEProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("PERIODSTARTDATE", typeof(DateTime?), null, false, objArray4);
                object[] objArray5 = new object[1];
                SimplePropertyAttribute attribute5 = new SimplePropertyAttribute {
                    Alias = "FQUARTER"
                };
                objArray5[0] = attribute5;
                QUARTERProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("QUARTER", typeof(long), null, false, objArray5);
                object[] objArray6 = new object[1];
                SimplePropertyAttribute attribute6 = new SimplePropertyAttribute {
                    Alias = "FENTRYSEQ"
                };
                objArray6[0] = attribute6;
                SeqProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("Seq", typeof(int), null, false, objArray6);
                object[] objArray7 = new object[1];
                SimplePropertyAttribute attribute7 = new SimplePropertyAttribute {
                    Alias = "FWEEK"
                };
                objArray7[0] = attribute7;
                WEEKProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("WEEK", typeof(long), null, false, objArray7);
                object[] objArray8 = new object[1];
                SimplePropertyAttribute attribute8 = new SimplePropertyAttribute {
                    Alias = "FYEAR"
                };
                objArray8[0] = attribute8;
                YEARProperty = BudgetCalendar.BD_ACCOUNTPERIODType.RegisterSimpleProperty("YEAR", typeof(long), null, false, objArray8);
            }

            public BD_ACCOUNTPERIOD(DynamicObject obj) : base(obj)
            {
            }

            public static implicit operator BudgetCalendar.BD_ACCOUNTPERIOD(DynamicObject obj)
            {
                if (obj != null)
                {
                    return new BudgetCalendar.BD_ACCOUNTPERIOD(obj);
                }
                return null;
            }

            public long MONTH
            {
                get
                {
                    return (long) MONTHProperty.GetValue(base.DataEntity);
                }
            }

            public long PERIOD
            {
                get
                {
                    return (long) PERIODProperty.GetValue(base.DataEntity);
                }
            }

            public DateTime? PERIODENDDATE
            {
                get
                {
                    return (DateTime?) PERIODENDDATEProperty.GetValue(base.DataEntity);
                }
            }

            public DateTime? PERIODSTARTDATE
            {
                get
                {
                    return (DateTime?) PERIODSTARTDATEProperty.GetValue(base.DataEntity);
                }
            }

            public long QUARTER
            {
                get
                {
                    return (long) QUARTERProperty.GetValue(base.DataEntity);
                }
            }

            public int Seq
            {
                get
                {
                    return (int) SeqProperty.GetValue(base.DataEntity);
                }
            }

            public long WEEK
            {
                get
                {
                    return (long) WEEKProperty.GetValue(base.DataEntity);
                }
            }

            public long YEAR
            {
                get
                {
                    return (long) YEARProperty.GetValue(base.DataEntity);
                }
            }
        }

        public class BudgetPeriodEntity : IdView
        {
            public static readonly DynamicProperty PeriodEndDateProperty;
            public static readonly DynamicProperty PeriodNameProperty;
            public static readonly DynamicProperty PeriodNumberProperty;
            public static readonly DynamicProperty PeriodProperty;
            public static readonly DynamicProperty PeriodStartDateProperty;
            public static readonly DynamicProperty PeriodTypeProperty;
            public static readonly DynamicProperty PeriodYearProperty;
            public static readonly DynamicProperty RowIDProperty;
            public static readonly DynamicProperty RowParentIdProperty;
            public static readonly DynamicProperty RowTypeProperty;

            static BudgetPeriodEntity()
            {
                object[] attributes = new object[1];
                SimplePropertyAttribute attribute = new SimplePropertyAttribute {
                    Alias = "FPERIOD"
                };
                attributes[0] = attribute;
                PeriodProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("Period", typeof(long), null, false, attributes);
                object[] objArray2 = new object[1];
                SimplePropertyAttribute attribute2 = new SimplePropertyAttribute {
                    Alias = "FPERIODENDDATE"
                };
                objArray2[0] = attribute2;
                PeriodEndDateProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodEndDate", typeof(DateTime?), null, false, objArray2);
                object[] objArray3 = new object[2];
                SimplePropertyAttribute attribute3 = new SimplePropertyAttribute {
                    Alias = "FPERIODNAME"
                };
                objArray3[0] = attribute3;
                objArray3[1] = new DbIgnoreAttribute();
                PeriodNameProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodName", typeof(LocaleValue), null, false, objArray3);
                object[] objArray4 = new object[1];
                SimplePropertyAttribute attribute4 = new SimplePropertyAttribute {
                    Alias = "FPERIODNUMBER"
                };
                objArray4[0] = attribute4;
                PeriodNumberProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodNumber", typeof(string), null, false, objArray4);
                object[] objArray5 = new object[1];
                SimplePropertyAttribute attribute5 = new SimplePropertyAttribute {
                    Alias = "FPERIODSTARTDATE"
                };
                objArray5[0] = attribute5;
                PeriodStartDateProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodStartDate", typeof(DateTime?), null, false, objArray5);
                object[] objArray6 = new object[1];
                SimplePropertyAttribute attribute6 = new SimplePropertyAttribute {
                    Alias = "FPERIODTYPE"
                };
                objArray6[0] = attribute6;
                PeriodTypeProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodType", typeof(string), null, false, objArray6);
                object[] objArray7 = new object[1];
                SimplePropertyAttribute attribute7 = new SimplePropertyAttribute {
                    Alias = "FPERIODYEAR"
                };
                objArray7[0] = attribute7;
                PeriodYearProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("PeriodYear", typeof(long), null, false, objArray7);
                object[] objArray8 = new object[1];
                SimplePropertyAttribute attribute8 = new SimplePropertyAttribute {
                    Alias = "FROWID"
                };
                objArray8[0] = attribute8;
                RowIDProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("RowID", typeof(string), null, false, objArray8);
                object[] objArray9 = new object[1];
                SimplePropertyAttribute attribute9 = new SimplePropertyAttribute {
                    Alias = "FROWPARENTID"
                };
                objArray9[0] = attribute9;
                RowParentIdProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("RowParentId", typeof(string), null, false, objArray9);
                object[] objArray10 = new object[1];
                SimplePropertyAttribute attribute10 = new SimplePropertyAttribute {
                    Alias = "FROWTYPE"
                };
                objArray10[0] = attribute10;
                RowTypeProperty = BudgetCalendar.BudgetPeriodEntityType.RegisterSimpleProperty("RowType", typeof(string), null, false, objArray10);
            }

            public BudgetPeriodEntity(DynamicObject obj) : base(obj)
            {
            }

            public BudgetPeriodEntity(IDynamicFormModel model, DynamicObject dataEntity) : base(model, dataEntity)
            {
            }

            public static implicit operator BudgetCalendar.BudgetPeriodEntity(DynamicObject obj)
            {
                if (obj != null)
                {
                    return new BudgetCalendar.BudgetPeriodEntity(obj);
                }
                return null;
            }

            public long Period
            {
                get
                {
                    return (long) PeriodProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodProperty, "FPeriod", base.DataEntity, value);
                }
            }

            public DateTime? PeriodEndDate
            {
                get
                {
                    return (DateTime?) PeriodEndDateProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodEndDateProperty, "FPeriodEndDate", base.DataEntity, value);
                }
            }

            public LocaleValue PeriodName
            {
                get
                {
                    return (LocaleValue) PeriodNameProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodNameProperty, "FPeriodName", base.DataEntity, value);
                }
            }

            public string PeriodNumber
            {
                get
                {
                    return (string) PeriodNumberProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodNumberProperty, "FPeriodNumber", base.DataEntity, value);
                }
            }

            public DateTime? PeriodStartDate
            {
                get
                {
                    return (DateTime?) PeriodStartDateProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodStartDateProperty, "FPeriodStartDate", base.DataEntity, value);
                }
            }

            public string PeriodType
            {
                get
                {
                    return (string) PeriodTypeProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodTypeProperty, "FPeriodType", base.DataEntity, value);
                }
            }

            public long PeriodYear
            {
                get
                {
                    return (long) PeriodYearProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(PeriodYearProperty, "FPeriodYear", base.DataEntity, value);
                }
            }

            public string RowID
            {
                get
                {
                    return (string) RowIDProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(RowIDProperty, "FRowID", base.DataEntity, value);
                }
            }

            public string RowParentId
            {
                get
                {
                    return (string) RowParentIdProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(RowParentIdProperty, "FRowParentId", base.DataEntity, value);
                }
            }

            public string RowType
            {
                get
                {
                    return (string) RowTypeProperty.GetValue(base.DataEntity);
                }
                set
                {
                    base.SetValue(RowTypeProperty, "FRowType", base.DataEntity, value);
                }
            }
        }

        public class ORMBD_ACCOUN : NameNumberMasterIdView
        {
            private DynamicObjectViewCollection<BudgetCalendar.BD_ACCOUNTPERIOD> _BD_ACCOUNTPERIODList;
            public static readonly DynamicProperty BD_ACCOUNTPERIODListProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterCollectionProperty("BD_ACCOUNTPERIOD", BudgetCalendar.BD_ACCOUNTPERIODType, null, new object[0]);
            public static readonly DynamicProperty ENDDATEProperty;
            public static readonly DynamicProperty IdProperty;
            public static readonly DynamicProperty PeriodCountProperty;
            public static readonly DynamicProperty PERIODTYPEProperty;
            public static readonly DynamicProperty STARTDATEProperty;

            static ORMBD_ACCOUN()
            {
                object[] attributes = new object[1];
                SimplePropertyAttribute attribute = new SimplePropertyAttribute {
                    Alias = "FENDDATE"
                };
                attributes[0] = attribute;
                ENDDATEProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterSimpleProperty("ENDDATE", typeof(DateTime?), null, false, attributes);
                object[] objArray2 = new object[1];
                SimplePropertyAttribute attribute2 = new SimplePropertyAttribute(true) {
                    Alias = "FID"
                };
                objArray2[0] = attribute2;
                IdProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterSimpleProperty("Id", typeof(long), null, false, objArray2);
                object[] objArray3 = new object[1];
                SimplePropertyAttribute attribute3 = new SimplePropertyAttribute {
                    Alias = "FPERIODCOUNT"
                };
                objArray3[0] = attribute3;
                PeriodCountProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterSimpleProperty("PeriodCount", typeof(long), null, false, objArray3);
                object[] objArray4 = new object[1];
                SimplePropertyAttribute attribute4 = new SimplePropertyAttribute {
                    Alias = "FPERIODTYPE"
                };
                objArray4[0] = attribute4;
                PERIODTYPEProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterSimpleProperty("PERIODTYPE", typeof(string), null, false, objArray4);
                object[] objArray5 = new object[1];
                SimplePropertyAttribute attribute5 = new SimplePropertyAttribute {
                    Alias = "FSTARTDATE"
                };
                objArray5[0] = attribute5;
                STARTDATEProperty = BudgetCalendar.ORMBD_ACCOUNType.RegisterSimpleProperty("STARTDATE", typeof(DateTime?), null, false, objArray5);
            }

            public ORMBD_ACCOUN(DynamicObject obj) : base(obj)
            {
            }

            public static implicit operator BudgetCalendar.ORMBD_ACCOUN(DynamicObject obj)
            {
                if (obj != null)
                {
                    return new BudgetCalendar.ORMBD_ACCOUN(obj);
                }
                return null;
            }

            public DynamicObjectViewCollection<BudgetCalendar.BD_ACCOUNTPERIOD> BD_ACCOUNTPERIODList
            {
                get
                {
                    this._BD_ACCOUNTPERIODList = this._BD_ACCOUNTPERIODList ?? new DynamicObjectViewCollection<BudgetCalendar.BD_ACCOUNTPERIOD>((DynamicObjectCollection) BD_ACCOUNTPERIODListProperty.GetValue(base.DataEntity));
                    return this._BD_ACCOUNTPERIODList;
                }
            }

            public DateTime? ENDDATE
            {
                get
                {
                    return (DateTime?) ENDDATEProperty.GetValue(base.DataEntity);
                }
            }

            public long Id
            {
                get
                {
                    return (long) IdProperty.GetValue(base.DataEntity);
                }
            }

            public long PeriodCount
            {
                get
                {
                    return (long) PeriodCountProperty.GetValue(base.DataEntity);
                }
            }

            public string PERIODTYPE
            {
                get
                {
                    return (string) PERIODTYPEProperty.GetValue(base.DataEntity);
                }
            }

            public DateTime? STARTDATE
            {
                get
                {
                    return (DateTime?) STARTDATEProperty.GetValue(base.DataEntity);
                }
            }
        }

        public class User : IdView
        {
            public static readonly DynamicProperty NameProperty;
            public static readonly DynamicProperty UserAccountProperty;

            static User()
            {
                object[] attributes = new object[1];
                SimplePropertyAttribute attribute = new SimplePropertyAttribute {
                    Alias = "FNAME"
                };
                attributes[0] = attribute;
                NameProperty = BudgetCalendar.UserType.RegisterSimpleProperty("Name", typeof(string), null, false, attributes);
                object[] objArray2 = new object[1];
                SimplePropertyAttribute attribute2 = new SimplePropertyAttribute {
                    Alias = "FUSERACCOUNT"
                };
                objArray2[0] = attribute2;
                UserAccountProperty = BudgetCalendar.UserType.RegisterSimpleProperty("UserAccount", typeof(string), null, false, objArray2);
            }

            public User(DynamicObject obj) : base(obj)
            {
            }

            public static implicit operator BudgetCalendar.User(DynamicObject obj)
            {
                if (obj != null)
                {
                    return new BudgetCalendar.User(obj);
                }
                return null;
            }

            public string Name
            {
                get
                {
                    return (string) NameProperty.GetValue(base.DataEntity);
                }
            }

            public string UserAccount
            {
                get
                {
                    return (string) UserAccountProperty.GetValue(base.DataEntity);
                }
            }
        }
    }
}

