using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> instead.</summary>
	// Token: 0x02000543 RID: 1347
	[Guid("00020401-0000-0000-C000-000000000046")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeInfo
	{
		// Token: 0x06002947 RID: 10567
		void $__Stripped0_GetTypeAttr();

		// Token: 0x06002948 RID: 10568
		void $__Stripped1_GetTypeComp();

		// Token: 0x06002949 RID: 10569
		void $__Stripped2_GetFuncDesc();

		// Token: 0x0600294A RID: 10570
		void $__Stripped3_GetVarDesc();

		// Token: 0x0600294B RID: 10571
		void $__Stripped4_GetNames();

		// Token: 0x0600294C RID: 10572
		void $__Stripped5_GetRefTypeOfImplType();

		// Token: 0x0600294D RID: 10573
		void $__Stripped6_GetImplTypeFlags();

		// Token: 0x0600294E RID: 10574
		void $__Stripped7_GetIDsOfNames();

		// Token: 0x0600294F RID: 10575
		void $__Stripped8_Invoke();

		// Token: 0x06002950 RID: 10576
		void $__Stripped9_GetDocumentation();

		// Token: 0x06002951 RID: 10577
		void $__Stripped10_GetDllEntry();

		// Token: 0x06002952 RID: 10578
		void $__Stripped11_GetRefTypeInfo();

		// Token: 0x06002953 RID: 10579
		void $__Stripped12_AddressOfMember();

		// Token: 0x06002954 RID: 10580
		void $__Stripped13_CreateInstance();

		// Token: 0x06002955 RID: 10581
		void $__Stripped14_GetMops();

		// Token: 0x06002956 RID: 10582
		void $__Stripped15_GetContainingTypeLib();

		// Token: 0x06002957 RID: 10583
		void $__Stripped16_ReleaseTypeAttr();

		// Token: 0x06002958 RID: 10584
		void $__Stripped17_ReleaseFuncDesc();

		// Token: 0x06002959 RID: 10585
		void $__Stripped18_ReleaseVarDesc();
	}
}
