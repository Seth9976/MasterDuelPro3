using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000078 RID: 120
	internal struct ShadowEdge
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00016863 File Offset: 0x00014A63
		public ShadowEdge(int indexA, int indexB)
		{
			this.v0 = indexA;
			this.v1 = indexB;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00016874 File Offset: 0x00014A74
		public void Reverse()
		{
			int tmp = this.v0;
			this.v0 = this.v1;
			this.v1 = tmp;
		}

		// Token: 0x040002A1 RID: 673
		public int v0;

		// Token: 0x040002A2 RID: 674
		public int v1;
	}
}
