using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000065 RID: 101
	[Guid("e6fdd21a-163f-4975-9c8c-a69f1ba37034")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IFileDialogCustomize
	{
		// Token: 0x060002D4 RID: 724
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnableOpenDropDown([In] int dwIDCtl);

		// Token: 0x060002D5 RID: 725
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddMenu([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002D6 RID: 726
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddPushButton([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002D7 RID: 727
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddComboBox([In] int dwIDCtl);

		// Token: 0x060002D8 RID: 728
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddRadioButtonList([In] int dwIDCtl);

		// Token: 0x060002D9 RID: 729
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddCheckButton([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel, [In] bool bChecked);

		// Token: 0x060002DA RID: 730
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddEditBox([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002DB RID: 731
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddSeparator([In] int dwIDCtl);

		// Token: 0x060002DC RID: 732
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddText([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002DD RID: 733
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlLabel([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002DE RID: 734
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetControlState([In] int dwIDCtl, out NativeMethods.CDCONTROLSTATE pdwState);

		// Token: 0x060002DF RID: 735
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlState([In] int dwIDCtl, [In] NativeMethods.CDCONTROLSTATE dwState);

		// Token: 0x060002E0 RID: 736
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetEditBoxText([In] int dwIDCtl, [Out] IntPtr ppszText);

		// Token: 0x060002E1 RID: 737
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetEditBoxText([In] int dwIDCtl, [MarshalAs(21)] [In] string pszText);

		// Token: 0x060002E2 RID: 738
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCheckButtonState([In] int dwIDCtl, out bool pbChecked);

		// Token: 0x060002E3 RID: 739
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCheckButtonState([In] int dwIDCtl, [In] bool bChecked);

		// Token: 0x060002E4 RID: 740
		[MethodImpl(4096, MethodCodeType = 3)]
		void AddControlItem([In] int dwIDCtl, [In] int dwIDItem, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002E5 RID: 741
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveControlItem([In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002E6 RID: 742
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveAllControlItems([In] int dwIDCtl);

		// Token: 0x060002E7 RID: 743
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetControlItemState([In] int dwIDCtl, [In] int dwIDItem, out NativeMethods.CDCONTROLSTATE pdwState);

		// Token: 0x060002E8 RID: 744
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetControlItemState([In] int dwIDCtl, [In] int dwIDItem, [In] NativeMethods.CDCONTROLSTATE dwState);

		// Token: 0x060002E9 RID: 745
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetSelectedControlItem([In] int dwIDCtl, out int pdwIDItem);

		// Token: 0x060002EA RID: 746
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetSelectedControlItem([In] int dwIDCtl, [In] int dwIDItem);

		// Token: 0x060002EB RID: 747
		[MethodImpl(4096, MethodCodeType = 3)]
		void StartVisualGroup([In] int dwIDCtl, [MarshalAs(21)] [In] string pszLabel);

		// Token: 0x060002EC RID: 748
		[MethodImpl(4096, MethodCodeType = 3)]
		void EndVisualGroup();

		// Token: 0x060002ED RID: 749
		[MethodImpl(4096, MethodCodeType = 3)]
		void MakeProminent([In] int dwIDCtl);
	}
}
