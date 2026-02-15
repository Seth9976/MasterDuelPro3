using System;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal enum EventSource
	{
		// Token: 0x04000046 RID: 70
		Unspecified,
		// Token: 0x04000047 RID: 71
		Keyboard,
		// Token: 0x04000048 RID: 72
		Gamepad,
		// Token: 0x04000049 RID: 73
		Mouse,
		// Token: 0x0400004A RID: 74
		Pen,
		// Token: 0x0400004B RID: 75
		Touch
	}
}
