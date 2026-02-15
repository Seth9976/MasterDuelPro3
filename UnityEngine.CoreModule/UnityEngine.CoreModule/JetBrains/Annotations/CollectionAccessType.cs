using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000080 RID: 128
	[Flags]
	public enum CollectionAccessType
	{
		// Token: 0x0400011A RID: 282
		None = 0,
		// Token: 0x0400011B RID: 283
		Read = 1,
		// Token: 0x0400011C RID: 284
		ModifyExistingContent = 2,
		// Token: 0x0400011D RID: 285
		UpdatedContent = 6
	}
}
