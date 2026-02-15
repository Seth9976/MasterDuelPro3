using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000016 RID: 22
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[NativeHeader("Modules/TextCoreTextEngine/Native/ATGMeshInfo.h")]
	internal struct ATGMeshInfo
	{
		// Token: 0x0400006D RID: 109
		public NativeTextElementInfo[] textElementInfos;

		// Token: 0x0400006E RID: 110
		public int fontAssetId;

		// Token: 0x0400006F RID: 111
		public int textElementCount;

		// Token: 0x04000070 RID: 112
		[Ignore]
		public FontAsset fontAsset;
	}
}
