using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the ITypeLib interface.</summary>
	// Token: 0x0200056E RID: 1390
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020402-0000-0000-C000-000000000046")]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x06002AB3 RID: 10931
		void $__Stripped0_GetTypeInfoCount();

		// Token: 0x06002AB4 RID: 10932
		void $__Stripped1_GetTypeInfo();

		// Token: 0x06002AB5 RID: 10933
		void $__Stripped2_GetTypeInfoType();

		// Token: 0x06002AB6 RID: 10934
		void $__Stripped3_GetTypeInfoOfGuid();

		// Token: 0x06002AB7 RID: 10935
		void $__Stripped4_GetLibAttr();

		// Token: 0x06002AB8 RID: 10936
		void $__Stripped5_GetTypeComp();

		// Token: 0x06002AB9 RID: 10937
		void $__Stripped6_GetDocumentation();

		// Token: 0x06002ABA RID: 10938
		void $__Stripped7_IsName();

		// Token: 0x06002ABB RID: 10939
		void $__Stripped8_FindName();

		// Token: 0x06002ABC RID: 10940
		void $__Stripped9_ReleaseTLibAttr();
	}
}
