using System;

namespace System.ComponentModel
{
	/// <summary>Provides support for rolling back the changes</summary>
	// Token: 0x020002AC RID: 684
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		/// <summary>Resets the object’s state to unchanged by rejecting the modifications.</summary>
		// Token: 0x06001052 RID: 4178
		void RejectChanges();
	}
}
