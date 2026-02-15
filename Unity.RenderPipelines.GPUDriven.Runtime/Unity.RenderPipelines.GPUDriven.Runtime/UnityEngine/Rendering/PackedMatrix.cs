using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000073 RID: 115
	internal struct PackedMatrix
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000D22C File Offset: 0x0000B42C
		public static PackedMatrix FromMatrix4x4(in Matrix4x4 m)
		{
			return new PackedMatrix
			{
				packed0 = new float4(m.m00, m.m10, m.m20, m.m01),
				packed1 = new float4(m.m11, m.m21, m.m02, m.m12),
				packed2 = new float4(m.m22, m.m03, m.m13, m.m23)
			};
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		public static PackedMatrix FromFloat4x4(in float4x4 m)
		{
			return new PackedMatrix
			{
				packed0 = new float4(m.c0.x, m.c0.y, m.c0.z, m.c1.x),
				packed1 = new float4(m.c1.y, m.c1.z, m.c2.x, m.c2.y),
				packed2 = new float4(m.c2.z, m.c3.x, m.c3.y, m.c3.z)
			};
		}

		// Token: 0x0400022F RID: 559
		public float4 packed0;

		// Token: 0x04000230 RID: 560
		public float4 packed1;

		// Token: 0x04000231 RID: 561
		public float4 packed2;
	}
}
