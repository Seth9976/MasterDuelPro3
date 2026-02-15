using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000048 RID: 72
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal enum TextWrappingMode
	{
		// Token: 0x040002E3 RID: 739
		NoWrap,
		// Token: 0x040002E4 RID: 740
		Normal,
		// Token: 0x040002E5 RID: 741
		PreserveWhitespace,
		// Token: 0x040002E6 RID: 742
		PreserveWhitespaceNoWrap
	}
}
