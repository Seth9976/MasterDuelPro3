using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000064 RID: 100
	[Guid("44BEAAEC-24F4-4E90-B3F0-23D258FBB146")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IKnownFolderManager
	{
		// Token: 0x060002CA RID: 714
		[MethodImpl(4096, MethodCodeType = 3)]
		void FolderIdFromCsidl([In] int nCsidl, out Guid pfid);

		// Token: 0x060002CB RID: 715
		[MethodImpl(4096, MethodCodeType = 3)]
		void FolderIdToCsidl([In] ref Guid rfid, out int pnCsidl);

		// Token: 0x060002CC RID: 716
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderIds([Out] IntPtr ppKFId, [In] [Out] ref uint pCount);

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([In] ref Guid rfid, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolderByName([MarshalAs(21)] [In] string pszCanonicalName, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002CF RID: 719
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterFolder([In] ref Guid rfid, [In] ref NativeMethods.KNOWNFOLDER_DEFINITION pKFD);

		// Token: 0x060002D0 RID: 720
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnregisterFolder([In] ref Guid rfid);

		// Token: 0x060002D1 RID: 721
		[MethodImpl(4096, MethodCodeType = 3)]
		void FindFolderFromPath([MarshalAs(21)] [In] string pszPath, [In] NativeMethods.FFFP_MODE mode, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002D2 RID: 722
		[MethodImpl(4096, MethodCodeType = 3)]
		void FindFolderFromIDList([In] IntPtr pidl, [MarshalAs(28)] out IKnownFolder ppkf);

		// Token: 0x060002D3 RID: 723
		[MethodImpl(4096, MethodCodeType = 3)]
		void Redirect([In] ref Guid rfid, [In] IntPtr hwnd, [In] uint Flags, [MarshalAs(21)] [In] string pszTargetPath, [In] uint cFolders, [In] ref Guid pExclusion, [MarshalAs(21)] out string ppszError);
	}
}
