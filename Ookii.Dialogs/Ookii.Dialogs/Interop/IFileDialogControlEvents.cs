using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000066 RID: 102
	[Guid("36116642-D713-4b97-9B83-7484A9D00433")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogControlEvents
	{
		// Token: 0x060002EE RID: 750
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnItemSelected([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002EF RID: 751
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnButtonClicked([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);

		// Token: 0x060002F0 RID: 752
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnCheckButtonToggled([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] bool bChecked);

		// Token: 0x060002F1 RID: 753
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnControlActivating([MarshalAs(28)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);
	}
}
