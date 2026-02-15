using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000146 RID: 326
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0001A695 File Offset: 0x00018895
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x04000578 RID: 1400
		public float alpha;

		// Token: 0x04000579 RID: 1401
		public float time;
	}
}
