using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal struct AABB
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CB File Offset: 0x000002CB
		public float3 min
		{
			get
			{
				return this.center - this.extents;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public float3 max
		{
			get
			{
				return this.center + this.extents;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F1 File Offset: 0x000002F1
		public override string ToString()
		{
			return string.Format("AABB(Center:{0}, Extents:{1}", this.center, this.extents);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002113 File Offset: 0x00000313
		private static float3 RotateExtents(float3 extents, float3 m0, float3 m1, float3 m2)
		{
			return math.abs(m0 * extents.x) + math.abs(m1 * extents.y) + math.abs(m2 * extents.z);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002154 File Offset: 0x00000354
		public static AABB Transform(float4x4 transform, AABB localBounds)
		{
			AABB transformed;
			transformed.extents = AABB.RotateExtents(localBounds.extents, transform.c0.xyz, transform.c1.xyz, transform.c2.xyz);
			transformed.center = math.transform(transform, localBounds.center);
			return transformed;
		}

		// Token: 0x04000006 RID: 6
		public float3 center;

		// Token: 0x04000007 RID: 7
		public float3 extents;
	}
}
