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
namespace Kingdee.K3.FIN.BM.App.Core.BudgetData
{
    using Kingdee.BOS;
    using Kingdee.BOS.AppServer;
    using Kingdee.BOS.Core;
    using Kingdee.BOS.Core.Metadata;
    using Kingdee.BOS.Core.Metadata.EntityElement;
    using Kingdee.BOS.Core.Metadata.FieldElement;
    using Kingdee.BOS.Core.Validation;
    using Kingdee.BOS.Orm.DataEntity;
    using Kingdee.BOS.Resource;
    using Kingdee.K3.FIN.BM.App.Core;
    using Kingdee.K3.FIN.BM.App.Core.BudgetCtrl;
    using Kingdee.K3.FIN.BM.App.Core.Repositories;
    using Kingdee.K3.FIN.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;


