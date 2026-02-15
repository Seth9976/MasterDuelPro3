using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000439 RID: 1081
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StyleValueType
	{
		// Token: 0x04000DC3 RID: 3523
		Invalid,
		// Token: 0x04000DC4 RID: 3524
		Keyword,
		// Token: 0x04000DC5 RID: 3525
		Float,
		// Token: 0x04000DC6 RID: 3526
		Dimension,
		// Token: 0x04000DC7 RID: 3527
		Color,
		// Token: 0x04000DC8 RID: 3528
		ResourcePath,
		// Token: 0x04000DC9 RID: 3529
		AssetReference,
		// Token: 0x04000DCA RID: 3530
		Enum,
		// Token: 0x04000DCB RID: 3531
		Variable,
		// Token: 0x04000DCC RID: 3532
		String,
		// Token: 0x04000DCD RID: 3533
		Function,
		// Token: 0x04000DCE RID: 3534
		CommaSeparator,
		// Token: 0x04000DCF RID: 3535
		ScalableImage,
		// Token: 0x04000DD0 RID: 3536
		MissingAssetReference
	}
}
