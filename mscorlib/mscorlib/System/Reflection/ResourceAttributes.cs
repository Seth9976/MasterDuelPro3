using System;

namespace System.Reflection
{
	/// <summary>Specifies the attributes for a manifest resource.</summary>
	// Token: 0x0200061A RID: 1562
	[Flags]
	public enum ResourceAttributes
	{
		/// <summary>A mask used to retrieve public manifest resources.</summary>
		// Token: 0x04001787 RID: 6023
		Public = 1,
		/// <summary>A mask used to retrieve private manifest resources.</summary>
		// Token: 0x04001788 RID: 6024
		Private = 2
	}
}
