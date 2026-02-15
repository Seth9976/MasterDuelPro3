using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000046 RID: 70
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal enum TextOverflowMode
	{
		// Token: 0x040002D6 RID: 726
		Overflow,
		// Token: 0x040002D7 RID: 727
		Ellipsis,
		// Token: 0x040002D8 RID: 728
		Masking,
		// Token: 0x040002D9 RID: 729
		Truncate,
		// Token: 0x040002DA RID: 730
		ScrollRect,
		// Token: 0x040002DB RID: 731
		Page,
		// Token: 0x040002DC RID: 732
		Linked
	}
}
