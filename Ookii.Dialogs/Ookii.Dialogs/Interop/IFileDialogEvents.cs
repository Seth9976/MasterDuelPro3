using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000060 RID: 96
	[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogEvents
	{
		// Token: 0x060002AE RID: 686
		[MethodImpl(4224, MethodCodeType = 3)]
		HRESULT OnFileOk([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x060002AF RID: 687
		[MethodImpl(4224, MethodCodeType = 3)]
		HRESULT OnFolderChanging([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psiFolder);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnFolderChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnSelectionChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnShareViolation([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psi);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnTypeChange([MarshalAs(28)] [In] IFileDialog pfd);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnOverwrite([MarshalAs(28)] [In] IFileDialog pfd, [MarshalAs(28)] [In] IShellItem psi);
	}
}
