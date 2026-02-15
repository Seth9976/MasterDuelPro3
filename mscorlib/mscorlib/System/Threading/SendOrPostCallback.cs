using System;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a message is to be dispatched to a synchronization context.  </summary>
	/// <param name="state">The object passed to the delegate.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000221 RID: 545
	// (Invoke) Token: 0x06001479 RID: 5241
	public delegate void SendOrPostCallback(object state);
}
