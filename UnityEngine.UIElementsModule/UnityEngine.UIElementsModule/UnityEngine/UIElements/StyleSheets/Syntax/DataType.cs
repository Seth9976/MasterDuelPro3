using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005C5 RID: 1477
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum DataType
	{
		// Token: 0x0400152C RID: 5420
		None,
		// Token: 0x0400152D RID: 5421
		Number,
		// Token: 0x0400152E RID: 5422
		Integer,
		// Token: 0x0400152F RID: 5423
		Length,
		// Token: 0x04001530 RID: 5424
		Percentage,
		// Token: 0x04001531 RID: 5425
		Color,
		// Token: 0x04001532 RID: 5426
		Resource,
		// Token: 0x04001533 RID: 5427
		Url,
		// Token: 0x04001534 RID: 5428
		Time,
		// Token: 0x04001535 RID: 5429
		Angle,
		// Token: 0x04001536 RID: 5430
		CustomIdent
	}
}
