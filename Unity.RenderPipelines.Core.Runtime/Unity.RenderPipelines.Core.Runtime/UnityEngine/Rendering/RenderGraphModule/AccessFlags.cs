using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200023D RID: 573
	[Flags]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public enum AccessFlags
	{
		// Token: 0x04000A0F RID: 2575
		None = 0,
		// Token: 0x04000A10 RID: 2576
		Read = 1,
		// Token: 0x04000A11 RID: 2577
		Write = 2,
		// Token: 0x04000A12 RID: 2578
		Discard = 4,
		// Token: 0x04000A13 RID: 2579
		WriteAll = 6,
		// Token: 0x04000A14 RID: 2580
		ReadWrite = 3
	}
}
