using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A2 RID: 418
	[BurstCompile]
	internal struct ReflectionProbeMinMaxZJob : IJobFor
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0002A7D8 File Offset: 0x000289D8
		public void Execute(int index)
		{
			float2 minMax = math.float2(float.MaxValue, float.MinValue);
			int reflectionProbeIndex = index % this.reflectionProbes.Length;
			VisibleReflectionProbe reflectionProbe = this.reflectionProbes[reflectionProbeIndex];
			int viewIndex = index / this.reflectionProbes.Length;
			float4x4 worldToView = this.worldToViews[viewIndex];
			float3 centerWS = reflectionProbe.bounds.center;
			float3 extentsWS = reflectionProbe.bounds.extents;
			for (int i = 0; i < 8; i++)
			{
				int x = ((i << 1) & 2) - 1;
				int y = (i & 2) - 1;
				int z = ((i >> 1) & 2) - 1;
				float4 cornerVS = math.mul(worldToView, math.float4(centerWS + extentsWS * math.float3((float)x, (float)y, (float)z), 1f));
				cornerVS.z *= -1f;
				minMax.x = math.min(minMax.x, cornerVS.z);
				minMax.y = math.max(minMax.y, cornerVS.z);
			}
			this.minMaxZs[index] = minMax;
		}

		// Token: 0x04000926 RID: 2342
		public Fixed2<float4x4> worldToViews;

		// Token: 0x04000927 RID: 2343
		[ReadOnly]
		public NativeArray<VisibleReflectionProbe> reflectionProbes;

		// Token: 0x04000928 RID: 2344
		public NativeArray<float2> minMaxZs;
	}
}
