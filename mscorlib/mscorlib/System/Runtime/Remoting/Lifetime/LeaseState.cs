using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Indicates the possible lease states of a lifetime lease.</summary>
	// Token: 0x0200043B RID: 1083
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		/// <summary>The lease is not initialized.</summary>
		// Token: 0x0400115D RID: 4445
		Null,
		/// <summary>The lease has been created, but is not yet active.</summary>
		// Token: 0x0400115E RID: 4446
		Initial,
		/// <summary>The lease is active and has not expired.</summary>
		// Token: 0x0400115F RID: 4447
		Active,
		/// <summary>The lease has expired and is seeking sponsorship.</summary>
		// Token: 0x04001160 RID: 4448
		Renewing,
		/// <summary>The lease has expired and cannot be renewed.</summary>
		// Token: 0x04001161 RID: 4449
		Expired
	}
}
