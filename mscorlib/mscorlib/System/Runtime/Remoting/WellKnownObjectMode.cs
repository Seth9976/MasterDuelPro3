using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Defines how well-known objects are activated.</summary>
	// Token: 0x0200042D RID: 1069
	[ComVisible(true)]
	[Serializable]
	public enum WellKnownObjectMode
	{
		/// <summary>Every incoming message is serviced by the same object instance.</summary>
		// Token: 0x0400113B RID: 4411
		Singleton = 1,
		/// <summary>Every incoming message is serviced by a new object instance.</summary>
		// Token: 0x0400113C RID: 4412
		SingleCall
	}
}
