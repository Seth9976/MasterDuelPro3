using System;

namespace AssetsTools.NET
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum TypeTreeNodeFlags
	{
		// Token: 0x040001A5 RID: 421
		None = 0,
		// Token: 0x040001A6 RID: 422
		Array = 1,
		// Token: 0x040001A7 RID: 423
		Ref = 2,
		// Token: 0x040001A8 RID: 424
		Registry = 4,
		// Token: 0x040001A9 RID: 425
		ArrayOfRefs = 8
	}
}
