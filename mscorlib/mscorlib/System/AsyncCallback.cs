using System;

namespace System
{
	/// <summary>References a method to be called when a corresponding asynchronous operation completes.</summary>
	/// <param name="ar">The result of the asynchronous operation. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C5 RID: 197
	// (Invoke) Token: 0x060004D6 RID: 1238
	[Serializable]
	public delegate void AsyncCallback(IAsyncResult ar);
}
