using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a <see cref="T:System.Threading.WaitHandle" /> is signaled or times out.</summary>
	/// <param name="state">An object containing information to be used by the callback method each time it executes. </param>
	/// <param name="timedOut">true if the <see cref="T:System.Threading.WaitHandle" /> timed out; false if it was signaled. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000268 RID: 616
	// (Invoke) Token: 0x060016E8 RID: 5864
	[ComVisible(true)]
	public delegate void WaitOrTimerCallback(object state, bool timedOut);
}
