using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000145 RID: 325
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x0001A684 File Offset: 0x00018884
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x04000576 RID: 1398
		public Color color;

		// Token: 0x04000577 RID: 1399
		public float time;
	}
}
