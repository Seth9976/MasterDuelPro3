using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F1 RID: 1265
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum VisualTreeUpdatePhase
	{
		// Token: 0x04001020 RID: 4128
		Bindings,
		// Token: 0x04001021 RID: 4129
		DataBinding,
		// Token: 0x04001022 RID: 4130
		Animation,
		// Token: 0x04001023 RID: 4131
		Styles,
		// Token: 0x04001024 RID: 4132
		Layout,
		// Token: 0x04001025 RID: 4133
		TransformClip,
		// Token: 0x04001026 RID: 4134
		Repaint,
		// Token: 0x04001027 RID: 4135
		Count
	}
}
