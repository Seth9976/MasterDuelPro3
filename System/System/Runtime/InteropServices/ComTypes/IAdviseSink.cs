using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides a managed definition of the IAdviseSink interface.</summary>
	// Token: 0x02000116 RID: 278
	[Guid("0000010F-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAdviseSink
	{
		// Token: 0x06000562 RID: 1378
		void $__Stripped0_OnClose();

		// Token: 0x06000563 RID: 1379
		void $__Stripped1_OnDataChange();

		// Token: 0x06000564 RID: 1380
		void $__Stripped2_OnRename();

		// Token: 0x06000565 RID: 1381
		void $__Stripped3_OnSave();

		// Token: 0x06000566 RID: 1382
		void $__Stripped4_OnViewChange();
	}
}
