using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides an interface to expose Win32 HWND handles.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CE RID: 206
	[ComVisible(true)]
	[Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IWin32Window
	{
		/// <summary>Gets the handle to the window represented by the implementer.</summary>
		/// <returns>A handle to the window represented by the implementer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007C4 RID: 1988
		IntPtr Handle { get; }
	}
}
