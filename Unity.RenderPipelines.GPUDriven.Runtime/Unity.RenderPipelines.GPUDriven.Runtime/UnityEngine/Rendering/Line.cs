using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000015 RID: 21
	internal struct Line
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003768 File Offset: 0x00001968
		internal static Line LineOfPlaneIntersectingPlane(float4 a, float4 b)
		{
			return new Line
			{
				m = a.w * b.xyz - b.w * a.xyz,
				t = math.cross(a.xyz, b.xyz)
			};
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000037C8 File Offset: 0x000019C8
		internal static float4 PlaneContainingLineAndPoint(Line a, float3 b)
		{
			return new float4(a.m + math.cross(a.t, b), -math.dot(a.m, b));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000037F3 File Offset: 0x000019F3
		internal static float4 PlaneContainingLineWithNormalPerpendicularToVector(Line a, float3 b)
		{
			return new float4(math.cross(a.t, b), -math.dot(a.m, b));
		}

		// Token: 0x04000034 RID: 52
		public float3 m;

		// Token: 0x04000035 RID: 53
		public float3 t;
	}
}
