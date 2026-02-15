using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A1 RID: 417
	[BurstCompile]
	internal struct LightMinMaxZJob : IJobFor
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0002A58C File Offset: 0x0002878C
		public void Execute(int index)
		{
			int lightIndex = index % this.lights.Length;
			VisibleLight light = this.lights[lightIndex];
			float4x4 lightToWorld = light.localToWorldMatrix;
			float3 originWS = lightToWorld.c3.xyz;
			int viewIndex = index / this.lights.Length;
			float4x4 worldToView = this.worldToViews[viewIndex];
			float3 originVS = math.mul(worldToView, math.float4(originWS, 1f)).xyz;
			originVS.z *= -1f;
			float2 minMax = math.float2(originVS.z - light.range, originVS.z + light.range);
			if (light.lightType == LightType.Spot)
			{
				float angleA = math.radians(light.spotAngle) * 0.5f;
				float cosAngleA = math.cos(angleA);
				float coneHeight = light.range * cosAngleA;
				float3 spotDirectionWS = lightToWorld.c2.xyz;
				float3 endPointWS = originWS + spotDirectionWS * coneHeight;
				float3 endPointVS = math.mul(worldToView, math.float4(endPointWS, 1f)).xyz;
				endPointVS.z *= -1f;
				float angleB = 1.5707964f - angleA;
				float coneRadius = light.range * cosAngleA * math.sin(angleA) / math.sin(angleB);
				float3 a = endPointVS - originVS;
				float e = math.sqrt(1f - a.z * a.z / math.dot(a, a));
				if (-a.z < coneHeight * cosAngleA)
				{
					minMax.x = math.min(originVS.z, endPointVS.z - e * coneRadius);
				}
				if (a.z < coneHeight * cosAngleA)
				{
					minMax.y = math.max(originVS.z, endPointVS.z + e * coneRadius);
				}
			}
			else if (light.lightType != LightType.Point)
			{
				minMax.x = float.MaxValue;
				minMax.y = float.MinValue;
			}
			minMax.x = math.max(minMax.x, 0f);
			minMax.y = math.max(minMax.y, 0f);
			this.minMaxZs[index] = minMax;
		}

		// Token: 0x04000923 RID: 2339
		public Fixed2<float4x4> worldToViews;

		// Token: 0x04000924 RID: 2340
		[ReadOnly]
		public NativeArray<VisibleLight> lights;

		// Token: 0x04000925 RID: 2341
		public NativeArray<float2> minMaxZs;
	}
}
