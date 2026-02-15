using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000145 RID: 325
	[Flags]
	public enum ScriptableRenderPassInput
	{
		// Token: 0x04000790 RID: 1936
		None = 0,
		// Token: 0x04000791 RID: 1937
		Depth = 1,
		// Token: 0x04000792 RID: 1938
		Normal = 2,
		// Token: 0x04000793 RID: 1939
		Color = 4,
		// Token: 0x04000794 RID: 1940
		Motion = 8
	}
}
