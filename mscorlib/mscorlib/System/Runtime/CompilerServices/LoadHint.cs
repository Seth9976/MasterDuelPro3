using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the preferred default binding for a dependent assembly.</summary>
	// Token: 0x020005B1 RID: 1457
	[Serializable]
	public enum LoadHint
	{
		/// <summary>No preference specified.</summary>
		// Token: 0x04001601 RID: 5633
		Default,
		/// <summary>The dependency is always loaded.</summary>
		// Token: 0x04001602 RID: 5634
		Always,
		/// <summary>The dependency is sometimes loaded.</summary>
		// Token: 0x04001603 RID: 5635
		Sometimes
	}
}
