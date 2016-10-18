System.Reflection.AmbiguousMatchException: 发现不明确的匹配。
   在 System.Windows.Forms.Control.MarshaledInvoke(Control caller, Delegate method, Object[] args, Boolean synchronous)
   在 System.Windows.Forms.Control.Invoke(Delegate method, Object[] args)
   在 ns3.Class14.ns0.Class144.Resolve(IAssemblyReference iassemblyReference_0, String string_0)
   在 ns3.Class228.ns0.Class143.Resolve(IAssemblyReference iassemblyReference_0, String string_0)
   在 ns18.Class255.Load(IAssemblyReference iassemblyReference_0, String string_0)
   在 Reflector.CodeModel.Memory.AssemblyReference.Resolve()
   在 ns31.Class800.ns0.Class782.ns0.Class801..ctor(ICollection icollection_0, INamespace inamespace_0)
   在 ns31.Class800.ns0.Class782.WriteNamespace(INamespace inamespace_0)
   在 ns3.Class228.WriteTypeDeclaration(ITypeDeclaration itypeDeclaration_0, String string_3, ILanguageWriterConfiguration ilanguageWriterConfiguration_0)
namespace Kingdee.K3.FIN.Business.PlugIn
{
    using Kingdee.BOS.Core.Bill.PlugIn;
    using Kingdee.BOS.Core.CommonFilter;
    using Kingdee.BOS.Core.DynamicForm;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
    using Kingdee.BOS.Core.DynamicForm.PlugIn.ControlModel;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Metadata.EntityElement;
    using Kingdee.BOS.JSON;
    using Kingdee.BOS.Model.ListFilter;
    using Kingdee.BOS.Orm;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.BOS.Serialization;
    using Kingdee.BOS.ServiceHelper;
    using Kingdee.BOS.Util;
    using Kingdee.K3.FIN.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;


