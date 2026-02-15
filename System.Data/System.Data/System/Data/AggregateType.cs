using System;

namespace System.Data
{
	// Token: 0x02000025 RID: 37
	internal enum AggregateType
	{
		// Token: 0x040000E1 RID: 225
		None,
		// Token: 0x040000E2 RID: 226
		Sum = 4,
		// Token: 0x040000E3 RID: 227
		Mean,
		// Token: 0x040000E4 RID: 228
		Min,
		// Token: 0x040000E5 RID: 229
		Max,
		// Token: 0x040000E6 RID: 230
		First,
		// Token: 0x040000E7 RID: 231
		Count,
		// Token: 0x040000E8 RID: 232
		Var,
		// Token: 0x040000E9 RID: 233
		StDev
	}
}
