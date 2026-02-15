using System;

namespace System.Threading
{
	/// <summary>Notifies a waiting thread that an event has occurred. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000219 RID: 537
	public sealed class AutoResetEvent : EventWaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AutoResetEvent" /> class with a Boolean value indicating whether to set the initial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to non-signaled. </param>
		// Token: 0x0600145F RID: 5215 RVA: 0x00053424 File Offset: 0x00051624
		public AutoResetEvent(bool initialState)
			: base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
