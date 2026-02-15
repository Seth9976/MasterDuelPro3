using System;

namespace System.Xml.Schema
{
	// Token: 0x02000299 RID: 665
	internal enum AttributeMatchState
	{
		// Token: 0x04000D17 RID: 3351
		AttributeFound,
		// Token: 0x04000D18 RID: 3352
		AnyIdAttributeFound,
		// Token: 0x04000D19 RID: 3353
		UndeclaredElementAndAttribute,
		// Token: 0x04000D1A RID: 3354
		UndeclaredAttribute,
		// Token: 0x04000D1B RID: 3355
		AnyAttributeLax,
		// Token: 0x04000D1C RID: 3356
		AnyAttributeSkip,
		// Token: 0x04000D1D RID: 3357
		ProhibitedAnyAttribute,
		// Token: 0x04000D1E RID: 3358
		ProhibitedAttribute,
		// Token: 0x04000D1F RID: 3359
		AttributeNameMismatch,
		// Token: 0x04000D20 RID: 3360
		ValidateAttributeInvalidCall
	}
}
