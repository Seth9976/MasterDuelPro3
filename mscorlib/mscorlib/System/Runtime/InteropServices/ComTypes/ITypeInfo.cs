using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the Component Automation ITypeInfo interface.</summary>
	// Token: 0x0200056D RID: 1389
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x06002AA0 RID: 10912
		void $__Stripped0_GetTypeAttr();

		// Token: 0x06002AA1 RID: 10913
		void $__Stripped1_GetTypeComp();

		// Token: 0x06002AA2 RID: 10914
		void $__Stripped2_GetFuncDesc();

		// Token: 0x06002AA3 RID: 10915
		void $__Stripped3_GetVarDesc();

		// Token: 0x06002AA4 RID: 10916
		void $__Stripped4_GetNames();

		// Token: 0x06002AA5 RID: 10917
		void $__Stripped5_GetRefTypeOfImplType();

		// Token: 0x06002AA6 RID: 10918
		void $__Stripped6_GetImplTypeFlags();

		// Token: 0x06002AA7 RID: 10919
		void $__Stripped7_GetIDsOfNames();

		// Token: 0x06002AA8 RID: 10920
		void $__Stripped8_Invoke();

		// Token: 0x06002AA9 RID: 10921
		void $__Stripped9_GetDocumentation();

		// Token: 0x06002AAA RID: 10922
		void $__Stripped10_GetDllEntry();

		// Token: 0x06002AAB RID: 10923
		void $__Stripped11_GetRefTypeInfo();

		// Token: 0x06002AAC RID: 10924
		void $__Stripped12_AddressOfMember();

		// Token: 0x06002AAD RID: 10925
		void $__Stripped13_CreateInstance();

		// Token: 0x06002AAE RID: 10926
		void $__Stripped14_GetMops();

		// Token: 0x06002AAF RID: 10927
		void $__Stripped15_GetContainingTypeLib();

		// Token: 0x06002AB0 RID: 10928
		void $__Stripped16_ReleaseTypeAttr();

		// Token: 0x06002AB1 RID: 10929
		void $__Stripped17_ReleaseFuncDesc();

		// Token: 0x06002AB2 RID: 10930
		void $__Stripped18_ReleaseVarDesc();
	}
}
