using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000067 RID: 103
	[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IPropertyStore
	{
		// Token: 0x060002F2 RID: 754
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCount(out uint cProps);

		// Token: 0x060002F3 RID: 755
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetAt([In] uint iProp, out NativeMethods.PROPERTYKEY pkey);

		// Token: 0x060002F4 RID: 756
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetValue([In] ref NativeMethods.PROPERTYKEY key, out object pv);

		// Token: 0x060002F5 RID: 757
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetValue([In] ref NativeMethods.PROPERTYKEY key, [In] ref object pv);

		// Token: 0x060002F6 RID: 758
		[MethodImpl(4096, MethodCodeType = 3)]
		void Commit();
	}
}
