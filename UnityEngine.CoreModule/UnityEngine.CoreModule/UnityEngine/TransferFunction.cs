using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000112 RID: 274
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum TransferFunction
	{
		// Token: 0x04000343 RID: 835
		Unknown = -1,
		// Token: 0x04000344 RID: 836
		sRGB,
		// Token: 0x04000345 RID: 837
		BT1886,
		// Token: 0x04000346 RID: 838
		PQ,
		// Token: 0x04000347 RID: 839
		Linear,
		// Token: 0x04000348 RID: 840
		Gamma22
	}
}
