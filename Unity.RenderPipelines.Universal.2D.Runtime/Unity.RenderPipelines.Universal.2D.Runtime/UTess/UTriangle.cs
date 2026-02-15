using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000AA RID: 170
	internal struct UTriangle
	{
		// Token: 0x0400031B RID: 795
		public float2 va;

		// Token: 0x0400031C RID: 796
		public float2 vb;

		// Token: 0x0400031D RID: 797
		public float2 vc;

		// Token: 0x0400031E RID: 798
		public UCircle c;

		// Token: 0x0400031F RID: 799
		public float area;

		// Token: 0x04000320 RID: 800
		public int3 indices;
	}
}
