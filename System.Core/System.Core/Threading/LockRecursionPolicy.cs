using System;

namespace System.Threading
{
	/// <summary>Specifies whether a lock can be entered multiple times by the same thread.</summary>
	// Token: 0x0200015F RID: 351
	public enum LockRecursionPolicy
	{
		/// <summary>If a thread tries to enter a lock recursively, an exception is thrown. Some classes may allow certain recursions when this setting is in effect. </summary>
		// Token: 0x04000389 RID: 905
		NoRecursion,
		/// <summary>A thread can enter a lock recursively. Some classes may restrict this capability. </summary>
		// Token: 0x0400038A RID: 906
		SupportsRecursion
	}
}
