using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000063 RID: 99
	[Guid("38521333-6A87-46A7-AE10-0F16706816C3")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IKnownFolder
	{
		// Token: 0x060002C1 RID: 705
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetId(out Guid pkfid);

		// Token: 0x060002C2 RID: 706
		void spacer1();

		// Token: 0x060002C3 RID: 707
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetShellItem([In] uint dwFlags, ref Guid riid, out IShellItem ppv);

		// Token: 0x060002C4 RID: 708
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPath([In] uint dwFlags, [MarshalAs(21)] out string ppszPath);

		// Token: 0x060002C5 RID: 709
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetPath([In] uint dwFlags, [MarshalAs(21)] [In] string pszPath);

		// Token: 0x060002C6 RID: 710
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetLocation([In] uint dwFlags, [ComAliasName("Interop.wirePIDL")] [Out] IntPtr ppidl);

		// Token: 0x060002C7 RID: 711
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderType(out Guid pftid);

		// Token: 0x060002C8 RID: 712
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetRedirectionCapabilities(out uint pCapabilities);

		// Token: 0x060002C9 RID: 713
		void spacer2();
	}
}
