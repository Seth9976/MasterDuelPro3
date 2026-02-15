using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005C4 RID: 1476
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum ExpressionType
	{
		// Token: 0x04001527 RID: 5415
		Unknown,
		// Token: 0x04001528 RID: 5416
		Data,
		// Token: 0x04001529 RID: 5417
		Keyword,
		// Token: 0x0400152A RID: 5418
		Combinator
	}
}
