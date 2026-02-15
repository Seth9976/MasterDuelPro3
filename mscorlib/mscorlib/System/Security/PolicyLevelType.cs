using System;

namespace System.Security
{
	/// <summary>Specifies the type of a managed code policy level.</summary>
	// Token: 0x02000314 RID: 788
	public enum PolicyLevelType
	{
		/// <summary>Security policy for all managed code in an application.</summary>
		// Token: 0x04000D01 RID: 3329
		AppDomain = 3,
		/// <summary>Security policy for all managed code in an enterprise.</summary>
		// Token: 0x04000D02 RID: 3330
		Enterprise = 2,
		/// <summary>Security policy for all managed code that is run on the computer.</summary>
		// Token: 0x04000D03 RID: 3331
		Machine = 1,
		/// <summary>Security policy for all managed code that is run by the user.</summary>
		// Token: 0x04000D04 RID: 3332
		User = 0
	}
}
