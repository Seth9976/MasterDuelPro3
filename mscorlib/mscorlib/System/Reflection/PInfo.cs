using System;

namespace System.Reflection
{
	// Token: 0x02000647 RID: 1607
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04001888 RID: 6280
		Attributes = 1,
		// Token: 0x04001889 RID: 6281
		GetMethod = 2,
		// Token: 0x0400188A RID: 6282
		SetMethod = 4,
		// Token: 0x0400188B RID: 6283
		ReflectedType = 8,
		// Token: 0x0400188C RID: 6284
		DeclaringType = 16,
		// Token: 0x0400188D RID: 6285
		Name = 32
	}
}
