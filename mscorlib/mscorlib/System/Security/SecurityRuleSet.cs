using System;

namespace System.Security
{
	/// <summary>Identifies the set of security rules the common language runtime should enforce for an assembly.  </summary>
	// Token: 0x0200031A RID: 794
	public enum SecurityRuleSet : byte
	{
		/// <summary>Unsupported. Using this value results in a <see cref="T:System.IO.FileLoadException" /> being thrown.</summary>
		// Token: 0x04000D0D RID: 3341
		None,
		/// <summary>Indicates that the runtime will enforce level 1 (.NET Framework version 2.0) transparency rules.</summary>
		// Token: 0x04000D0E RID: 3342
		Level1,
		/// <summary>Indicates that the runtime will enforce level 2 transparency rules.</summary>
		// Token: 0x04000D0F RID: 3343
		Level2
	}
}
