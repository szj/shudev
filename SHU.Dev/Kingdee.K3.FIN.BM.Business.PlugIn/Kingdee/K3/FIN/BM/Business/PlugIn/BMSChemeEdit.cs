System.Reflection.AmbiguousMatchException: 发现不明确的匹配。
   在 System.Windows.Forms.Control.MarshaledInvoke(Control caller, Delegate method, Object[] args, Boolean synchronous)
   在 System.Windows.Forms.Control.Invoke(Delegate method, Object[] args)
   在 ns3.Class14.ns0.Class144.Resolve(IAssemblyReference iassemblyReference_0, String string_0)
   在 ns3.Class228.ns0.Class143.Resolve(IAssemblyReference iassemblyReference_0, String string_0)
   在 ns18.Class255.Load(IAssemblyReference iassemblyReference_0, String string_0)
   在 Reflector.CodeModel.Memory.AssemblyReference.Resolve()
   在 ns18.Class74.smethod_4(ITypeReference itypeReference_0)
   在 ns18.Class272.Resolve()
   在 ns18.Class283.Resolve()
   在 ns18.Class283.ns0.Class269.get_Item(Int32 int_1)
   在 Reflector.CodeModel.Memory.GenericArgument.Resolve()
   在 ns17.Class275.smethod_0(IType itype_0, IGenericArgumentProvider igenericArgumentProvider_0, IGenericArgumentProvider igenericArgumentProvider_1)
   在 ns17.Class275.smethod_0(IType itype_0, IGenericArgumentProvider igenericArgumentProvider_0, IGenericArgumentProvider igenericArgumentProvider_1)
   在 Reflector.CodeModel.Memory.MethodInstanceReference.get_ReturnType()
   在 ns25.Class688.method_80(IInstruction iinstruction_0)
   在 ns25.Class688.method_15(Int32 int_2)
   在 ns25.Class688.method_106(Int32 int_2, Int32 int_3)
   在 ns25.Class688.TranslateMethodDeclaration(IMethodDeclaration imethodDeclaration_1, IMethodBody imethodBody_1, Boolean bool_0)
   在 ns25.Class688.TranslateMethodDeclaration(IMethodDeclaration imethodDeclaration_1, IMethodBody imethodBody_1)
   在 ns25.Class593.vmethod_7(IMethodDeclaration imethodDeclaration_0)
   在 ns64.Class592.vmethod_140(IMethodDeclarationCollection imethodDeclarationCollection_0)
   在 ns25.Class593.vmethod_5(ITypeDeclaration itypeDeclaration_0)
   在 ns3.Class594.TranslateTypeDeclaration(ITypeDeclaration itypeDeclaration_0, Boolean bool_0, Boolean bool_1)
   在 ns3.Class228.WriteTypeDeclaration(ITypeDeclaration itypeDeclaration_0, String string_3, ILanguageWriterConfiguration ilanguageWriterConfiguration_0)
namespace Kingdee.K3.FIN.BM.Business.PlugIn
{
}

