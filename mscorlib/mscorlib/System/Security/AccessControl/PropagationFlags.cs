using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies how Access Control Entries (ACEs) are propagated to child objects.  These flags are significant only if inheritance flags are present. </summary>
	// Token: 0x02000404 RID: 1028
	[Flags]
	public enum PropagationFlags
	{
		/// <summary>Specifies that no inheritance flags are set.</summary>
		// Token: 0x040010BB RID: 4283
		None = 0,
		/// <summary>Specifies that the ACE is not propagated to child objects.</summary>
		// Token: 0x040010BC RID: 4284
		NoPropagateInherit = 1,
		/// <summary>Specifies that the ACE is propagated only to child objects. This includes both container and leaf child objects. </summary>
		// Token: 0x040010BD RID: 4285
		InheritOnly = 2
	}
}
