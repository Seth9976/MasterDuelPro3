using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" /> instead.</summary>
	// Token: 0x0200054E RID: 1358
	[Obsolete]
	[Guid("00020402-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeLib
	{
		// Token: 0x06002A6C RID: 10860
		void $__Stripped0_GetTypeInfoCount();

		// Token: 0x06002A6D RID: 10861
		void $__Stripped1_GetTypeInfo();

		// Token: 0x06002A6E RID: 10862
		void $__Stripped2_GetTypeInfoType();

		// Token: 0x06002A6F RID: 10863
		void $__Stripped3_GetTypeInfoOfGuid();

		// Token: 0x06002A70 RID: 10864
		void $__Stripped4_GetLibAttr();

		// Token: 0x06002A71 RID: 10865
		void $__Stripped5_GetTypeComp();

		// Token: 0x06002A72 RID: 10866
		void $__Stripped6_GetDocumentation();

		// Token: 0x06002A73 RID: 10867
		void $__Stripped7_IsName();

		// Token: 0x06002A74 RID: 10868
		void $__Stripped8_FindName();

		// Token: 0x06002A75 RID: 10869
		void $__Stripped9_ReleaseTLibAttr();
	}
}
