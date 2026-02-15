using System;

namespace System.Security
{
	/// <summary>Identifies the source for the security context.</summary>
	// Token: 0x0200031C RID: 796
	public enum SecurityContextSource
	{
		/// <summary>The current application domain is the source for the security context.</summary>
		// Token: 0x04000D12 RID: 3346
		CurrentAppDomain,
		/// <summary>The current assembly is the source for the security context.</summary>
		// Token: 0x04000D13 RID: 3347
		CurrentAssembly
	}
}
