using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000547 RID: 1351
	[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IErrorInfo
	{
		// Token: 0x06002979 RID: 10617
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = 3)]
		int GetGUID(out Guid pGuid);

		// Token: 0x0600297A RID: 10618
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = 3)]
		int GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

		// Token: 0x0600297B RID: 10619
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = 3)]
		int GetDescription([MarshalAs(UnmanagedType.BStr)] out string pbstrDescription);

		// Token: 0x0600297C RID: 10620
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = 3)]
		int GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		// Token: 0x0600297D RID: 10621
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = 3)]
		int GetHelpContext(out uint pdwHelpContext);
	}
}
