using System;

namespace AssetsTools.NET
{
	// Token: 0x02000068 RID: 104
	[Flags]
	public enum ClassFileTypeFlags : byte
	{
		// Token: 0x0400023E RID: 574
		None = 0,
		// Token: 0x0400023F RID: 575
		IsAbstract = 1,
		// Token: 0x04000240 RID: 576
		IsSealed = 2,
		// Token: 0x04000241 RID: 577
		IsEditorOnly = 4,
		// Token: 0x04000242 RID: 578
		IsReleaseOnly = 8,
		// Token: 0x04000243 RID: 579
		IsStripped = 16,
		// Token: 0x04000244 RID: 580
		Reserved = 32,
		// Token: 0x04000245 RID: 581
		HasEditorRootNode = 64,
		// Token: 0x04000246 RID: 582
		HasReleaseRootNode = 128
	}
}
