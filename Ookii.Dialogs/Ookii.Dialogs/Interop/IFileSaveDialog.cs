using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200005F RID: 95
	[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileSaveDialog : IFileDialog, IModalWindow
	{
		// Token: 0x06000291 RID: 657
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);

		// Token: 0x06000292 RID: 658
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypes([In] uint cFileTypes, [In] ref NativeMethods.COMDLG_FILTERSPEC rgFilterSpec);

		// Token: 0x06000293 RID: 659
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileTypeIndex([In] uint iFileType);

		// Token: 0x06000294 RID: 660
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileTypeIndex(out uint piFileType);

		// Token: 0x06000295 RID: 661
		[MethodImpl(4096, MethodCodeType = 3)]
		void Advise([MarshalAs(28)] [In] IFileDialogEvents pfde, out uint pdwCookie);

		// Token: 0x06000296 RID: 662
		[MethodImpl(4096, MethodCodeType = 3)]
		void Unadvise([In] uint dwCookie);

		// Token: 0x06000297 RID: 663
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOptions([In] NativeMethods.FOS fos);

		// Token: 0x06000298 RID: 664
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetOptions(out NativeMethods.FOS pfos);

		// Token: 0x06000299 RID: 665
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x0600029A RID: 666
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFolder([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x0600029B RID: 667
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFolder([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600029C RID: 668
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCurrentSelection([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x0600029D RID: 669
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileName([MarshalAs(21)] [In] string pszName);

		// Token: 0x0600029E RID: 670
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetFileName([MarshalAs(21)] out string pszName);

		// Token: 0x0600029F RID: 671
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetTitle([MarshalAs(21)] [In] string pszTitle);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOkButtonLabel([MarshalAs(21)] [In] string pszText);

		// Token: 0x060002A1 RID: 673
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFileNameLabel([MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetResult([MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPlace([MarshalAs(28)] [In] IShellItem psi, NativeMethods.FDAP fdap);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetDefaultExtension([MarshalAs(21)] [In] string pszDefaultExtension);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(4096, MethodCodeType = 3)]
		void Close([MarshalAs(45)] int hr);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetClientGuid([In] ref Guid guid);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(4096, MethodCodeType = 3)]
		void ClearClientData();

		// Token: 0x060002A8 RID: 680
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetFilter([MarshalAs(28)] IntPtr pFilter);

		// Token: 0x060002A9 RID: 681
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSaveAsItem([MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x060002AA RID: 682
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetProperties([MarshalAs(28)] [In] IntPtr pStore);

		// Token: 0x060002AB RID: 683
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCollectedProperties([MarshalAs(28)] [In] IntPtr pList, [In] int fAppendDefault);

		// Token: 0x060002AC RID: 684
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetProperties([MarshalAs(28)] out IntPtr ppStore);

		// Token: 0x060002AD RID: 685
		[MethodImpl(4096, MethodCodeType = 3)]
		void ApplyProperties([MarshalAs(28)] [In] IShellItem psi, [MarshalAs(28)] [In] IntPtr pStore, [ComAliasName("Interop.wireHWND")] [In] ref IntPtr hwnd, [MarshalAs(28)] [In] IntPtr pSink);
	}
}
