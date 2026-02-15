using System;

namespace UnityEngine
{
	// Token: 0x02000120 RID: 288
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		// Token: 0x040003E6 RID: 998
		None = 0,
		// Token: 0x040003E7 RID: 999
		RealtimeEmissive = 1,
		// Token: 0x040003E8 RID: 1000
		BakedEmissive = 2,
		// Token: 0x040003E9 RID: 1001
		EmissiveIsBlack = 4,
		// Token: 0x040003EA RID: 1002
		AnyEmissive = 3
	}
}
