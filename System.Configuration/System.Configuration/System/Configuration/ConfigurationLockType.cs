using System;

namespace System.Configuration
{
	// Token: 0x02000017 RID: 23
	[Flags]
	internal enum ConfigurationLockType
	{
		// Token: 0x04000059 RID: 89
		Attribute = 1,
		// Token: 0x0400005A RID: 90
		Element = 2,
		// Token: 0x0400005B RID: 91
		Exclude = 16
	}
}
