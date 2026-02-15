using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Creates a COM object.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> object that represents the IUnknown interface of the COM object.</returns>
	/// <param name="aggregator">A pointer to the managed object's IUnknown interface. </param>
	// Token: 0x02000540 RID: 1344
	// (Invoke) Token: 0x06002935 RID: 10549
	[ComVisible(true)]
	public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
