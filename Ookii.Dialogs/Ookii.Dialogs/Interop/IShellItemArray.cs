using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000062 RID: 98
	[Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IShellItemArray
	{
		// Token: 0x060002BA RID: 698
		[MethodImpl(4096, MethodCodeType = 3)]
		void BindToHandler([MarshalAs(28)] [In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

		// Token: 0x060002BB RID: 699
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060002BC RID: 700
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetPropertyDescriptionList([In] ref NativeMethods.PROPERTYKEY keyType, [In] ref Guid riid, out IntPtr ppv);

		// Token: 0x060002BD RID: 701
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAttributes([In] NativeMethods.SIATTRIBFLAGS dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

		// Token: 0x060002BE RID: 702
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCount(out uint pdwNumItems);

		// Token: 0x060002BF RID: 703
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetItemAt([In] uint dwIndex, [MarshalAs(28)] out IShellItem ppsi);

		// Token: 0x060002C0 RID: 704
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnumItems([MarshalAs(28)] out IntPtr ppenumShellItems);
	}
}
