using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001ED RID: 493
	internal static class DeferredConfig
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0003938C File Offset: 0x0003758C
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00039393 File Offset: 0x00037593
		internal static bool IsOpenGL { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0003939B File Offset: 0x0003759B
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x000393A2 File Offset: 0x000375A2
		internal static bool IsDX10 { get; set; }
	}
}
