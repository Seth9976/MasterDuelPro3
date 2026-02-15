using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A6 RID: 422
	[BurstCompile(FloatMode = FloatMode.Default, DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct TilingJob : IJobFor
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public void Execute(int jobIndex)
		{
			int index = jobIndex % this.itemsPerTile;
			this.m_ViewIndex = jobIndex / this.itemsPerTile;
			this.m_Offset = jobIndex * this.rangesPerItem;
			this.m_TileYRange = new InclusiveRange(short.MaxValue, short.MinValue);
			for (int i = 0; i < this.rangesPerItem; i++)
			{
				this.tileRanges[this.m_Offset + i] = new InclusiveRange(short.MaxValue, short.MinValue);
			}
			if (index >= this.lights.Length)
			{
				this.TileReflectionProbe(index);
				return;
			}
			if (this.isOrthographic)
			{
				this.TileLightOrthographic(index);
				return;
			}
			this.TileLight(index);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002AB50 File Offset: 0x00028D50
		private void TileLight(int lightIndex)
		{
			TilingJob.<>c__DisplayClass19_0 CS$<>8__locals1;
			CS$<>8__locals1.light = this.lights[lightIndex];
			if (CS$<>8__locals1.light.lightType != LightType.Point && CS$<>8__locals1.light.lightType != LightType.Spot)
			{
				return;
			}
			float4x4 lightToWorld = CS$<>8__locals1.light.localToWorldMatrix;
			CS$<>8__locals1.lightPositionVS = math.mul(this.worldToViews[this.m_ViewIndex], math.float4(lightToWorld.c3.xyz, 1f)).xyz;
			CS$<>8__locals1.lightPositionVS.z = CS$<>8__locals1.lightPositionVS.z * -1f;
			if (CS$<>8__locals1.lightPositionVS.z >= this.near)
			{
				this.ExpandY(CS$<>8__locals1.lightPositionVS);
			}
			CS$<>8__locals1.lightDirectionVS = math.normalize(math.mul(this.worldToViews[this.m_ViewIndex], math.float4(lightToWorld.c2.xyz, 0f)).xyz);
			CS$<>8__locals1.lightDirectionVS.z = CS$<>8__locals1.lightDirectionVS.z * -1f;
			float halfAngle = math.radians(CS$<>8__locals1.light.spotAngle * 0.5f);
			float range = CS$<>8__locals1.light.range;
			float num = TilingJob.square(range);
			CS$<>8__locals1.cosHalfAngle = math.cos(halfAngle);
			CS$<>8__locals1.coneHeight = CS$<>8__locals1.cosHalfAngle * range;
			float sphereClipRadius = math.sqrt(num - TilingJob.square(this.near - CS$<>8__locals1.lightPositionVS.z));
			float2 sphereBoundYZ0;
			float2 sphereBoundYZ;
			TilingJob.GetSphereHorizon(CS$<>8__locals1.lightPositionVS.yz, range, this.near, sphereClipRadius, out sphereBoundYZ0, out sphereBoundYZ);
			float3 sphereBoundY0 = math.float3(CS$<>8__locals1.lightPositionVS.x, sphereBoundYZ0);
			float3 sphereBoundY = math.float3(CS$<>8__locals1.lightPositionVS.x, sphereBoundYZ);
			if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereBoundY0, ref CS$<>8__locals1))
			{
				this.ExpandY(sphereBoundY0);
			}
			if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereBoundY, ref CS$<>8__locals1))
			{
				this.ExpandY(sphereBoundY);
			}
			float2 sphereBoundXZ0;
			float2 sphereBoundXZ;
			TilingJob.GetSphereHorizon(CS$<>8__locals1.lightPositionVS.xz, range, this.near, sphereClipRadius, out sphereBoundXZ0, out sphereBoundXZ);
			float3 sphereBoundX0 = math.float3(sphereBoundXZ0.x, CS$<>8__locals1.lightPositionVS.y, sphereBoundXZ0.y);
			float3 sphereBoundX = math.float3(sphereBoundXZ.x, CS$<>8__locals1.lightPositionVS.y, sphereBoundXZ.y);
			if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereBoundX0, ref CS$<>8__locals1))
			{
				this.ExpandY(sphereBoundX0);
			}
			if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereBoundX, ref CS$<>8__locals1))
			{
				this.ExpandY(sphereBoundX);
			}
			if (CS$<>8__locals1.light.lightType == LightType.Spot)
			{
				float baseRadius = math.sqrt(range * range - CS$<>8__locals1.coneHeight * CS$<>8__locals1.coneHeight);
				float3 baseCenter = CS$<>8__locals1.lightPositionVS + CS$<>8__locals1.lightDirectionVS * CS$<>8__locals1.coneHeight;
				float3 baseUY = ((math.abs(math.abs(CS$<>8__locals1.lightDirectionVS.x) - 1f) < 1E-06f) ? math.float3(0f, 1f, 0f) : math.normalize(math.cross(CS$<>8__locals1.lightDirectionVS, math.float3(1f, 0f, 0f))));
				float3 baseVY = math.cross(CS$<>8__locals1.lightDirectionVS, baseUY);
				float2 baseY1UV;
				float2 baseY2UV;
				TilingJob.GetProjectedCircleHorizon(baseCenter.yz, baseRadius, baseUY.yz, baseVY.yz, out baseY1UV, out baseY2UV);
				float3 baseY = baseCenter + baseY1UV.x * baseUY + baseY1UV.y * baseVY;
				float3 baseY2 = baseCenter + baseY2UV.x * baseUY + baseY2UV.y * baseVY;
				if (baseY.z >= this.near)
				{
					this.ExpandY(baseY);
				}
				if (baseY2.z >= this.near)
				{
					this.ExpandY(baseY2);
				}
				float3 baseUX = ((math.abs(math.abs(CS$<>8__locals1.lightDirectionVS.y) - 1f) < 1E-06f) ? math.float3(1f, 0f, 0f) : math.normalize(math.cross(CS$<>8__locals1.lightDirectionVS, math.float3(0f, 1f, 0f))));
				float3 baseVX = math.cross(CS$<>8__locals1.lightDirectionVS, baseUX);
				float2 baseX1UV;
				float2 baseX2UV;
				TilingJob.GetProjectedCircleHorizon(baseCenter.xz, baseRadius, baseUX.xz, baseVX.xz, out baseX1UV, out baseX2UV);
				float3 baseX = baseCenter + baseX1UV.x * baseUX + baseX1UV.y * baseVX;
				float3 baseX2 = baseCenter + baseX2UV.x * baseUX + baseX2UV.y * baseVX;
				if (baseX.z >= this.near)
				{
					this.ExpandY(baseX);
				}
				if (baseX2.z >= this.near)
				{
					this.ExpandY(baseX2);
				}
				float3 baseClip0;
				float3 baseClip;
				if (TilingJob.GetCircleClipPoints(baseCenter, CS$<>8__locals1.lightDirectionVS, baseRadius, this.near, out baseClip0, out baseClip))
				{
					this.ExpandY(baseClip0);
					this.ExpandY(baseClip);
				}
				float baseExtentZ = baseRadius * math.sqrt(1f - TilingJob.square(CS$<>8__locals1.lightDirectionVS.z));
				bool coneIsClipping = this.near >= math.min(baseCenter.z - baseExtentZ, CS$<>8__locals1.lightPositionVS.z) && this.near <= math.max(baseCenter.z + baseExtentZ, CS$<>8__locals1.lightPositionVS.z);
				float3 coneU = math.cross(CS$<>8__locals1.lightDirectionVS, CS$<>8__locals1.lightPositionVS);
				coneU = ((math.csum(coneU) != 0f) ? math.normalize(coneU) : math.float3(1f, 0f, 0f));
				float3 coneV = math.cross(CS$<>8__locals1.lightDirectionVS, coneU);
				if (coneIsClipping)
				{
					float r = baseRadius / CS$<>8__locals1.coneHeight;
					float2 thetaY = TilingJob.FindNearConicTangentTheta(CS$<>8__locals1.lightPositionVS.yz, CS$<>8__locals1.lightDirectionVS.yz, r, coneU.yz, coneV.yz);
					float3 p0Y = TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r, coneU, coneV, thetaY.x);
					float3 p1Y = TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r, coneU, coneV, thetaY.y);
					if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p0Y, ref CS$<>8__locals1))
					{
						this.ExpandY(p0Y);
					}
					if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p1Y, ref CS$<>8__locals1))
					{
						this.ExpandY(p1Y);
					}
					float2 thetaX = TilingJob.FindNearConicTangentTheta(CS$<>8__locals1.lightPositionVS.xz, CS$<>8__locals1.lightDirectionVS.xz, r, coneU.xz, coneV.xz);
					float3 p0X = TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r, coneU, coneV, thetaX.x);
					float3 p1X = TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r, coneU, coneV, thetaX.y);
					if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p0X, ref CS$<>8__locals1))
					{
						this.ExpandY(p0X);
					}
					if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p1X, ref CS$<>8__locals1))
					{
						this.ExpandY(p1X);
					}
				}
				float3 l;
				float3 l2;
				TilingJob.GetConeSideTangentPoints(CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, CS$<>8__locals1.cosHalfAngle, baseRadius, CS$<>8__locals1.coneHeight, range, coneU, coneV, out l, out l2);
				float3 planeNormal = math.float3(0f, 1f, this.viewPlaneBottoms[this.m_ViewIndex]);
				float l1t = math.dot(-CS$<>8__locals1.lightPositionVS, planeNormal) / math.dot(l, planeNormal);
				float3 l1x = CS$<>8__locals1.lightPositionVS + l * l1t;
				if (l1t >= 0f && l1t <= 1f && l1x.z >= this.near)
				{
					this.ExpandY(l1x);
				}
				float3 planeNormal2 = math.float3(0f, 1f, this.viewPlaneTops[this.m_ViewIndex]);
				float l1t2 = math.dot(-CS$<>8__locals1.lightPositionVS, planeNormal2) / math.dot(l, planeNormal2);
				float3 l1x2 = CS$<>8__locals1.lightPositionVS + l * l1t2;
				if (l1t2 >= 0f && l1t2 <= 1f && l1x2.z >= this.near)
				{
					this.ExpandY(l1x2);
				}
				this.m_TileYRange.Clamp(0, (short)(this.tileCount.y - 1));
				for (int planeIndex = (int)(this.m_TileYRange.start + 1); planeIndex <= (int)this.m_TileYRange.end; planeIndex++)
				{
					InclusiveRange planeRange = InclusiveRange.empty;
					float planeY = math.lerp(this.viewPlaneBottoms[this.m_ViewIndex], this.viewPlaneTops[this.m_ViewIndex], (float)planeIndex * this.tileScaleInv.y);
					float3 planeNormal3 = math.float3(0f, 1f, -planeY);
					float l1t3 = math.dot(-CS$<>8__locals1.lightPositionVS, planeNormal3) / math.dot(l, planeNormal3);
					float3 l1x3 = CS$<>8__locals1.lightPositionVS + l * l1t3;
					if (l1t3 >= 0f && l1t3 <= 1f && l1x3.z >= this.near)
					{
						planeRange.Expand((short)this.ViewToTileSpace(l1x3).x);
					}
					float l2t = math.dot(-CS$<>8__locals1.lightPositionVS, planeNormal3) / math.dot(l2, planeNormal3);
					float3 l2x = CS$<>8__locals1.lightPositionVS + l2 * l2t;
					if (l2t >= 0f && l2t <= 1f && l2x.z >= this.near)
					{
						planeRange.Expand((short)this.ViewToTileSpace(l2x).x);
					}
					float3 circleTile0;
					float3 circleTile;
					if (TilingJob.IntersectCircleYPlane(planeY, baseCenter, CS$<>8__locals1.lightDirectionVS, baseUY, baseVY, baseRadius, out circleTile0, out circleTile))
					{
						if (circleTile0.z >= this.near)
						{
							planeRange.Expand((short)this.ViewToTileSpace(circleTile0).x);
						}
						if (circleTile.z >= this.near)
						{
							planeRange.Expand((short)this.ViewToTileSpace(circleTile).x);
						}
					}
					if (coneIsClipping)
					{
						float y = planeY * this.near;
						float r2 = baseRadius / CS$<>8__locals1.coneHeight;
						float2 theta = TilingJob.FindNearConicYTheta(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r2, coneU, coneV, y);
						float3 p0 = math.float3(TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r2, coneU, coneV, theta.x).x, y, this.near);
						float3 p = math.float3(TilingJob.EvaluateNearConic(this.near, CS$<>8__locals1.lightPositionVS, CS$<>8__locals1.lightDirectionVS, r2, coneU, coneV, theta.y).x, y, this.near);
						if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p0, ref CS$<>8__locals1))
						{
							planeRange.Expand((short)this.ViewToTileSpace(p0).x);
						}
						if (TilingJob.<TileLight>g__ConicPointIsValid|19_1(p, ref CS$<>8__locals1))
						{
							planeRange.Expand((short)this.ViewToTileSpace(p).x);
						}
					}
					if ((planeRange.start >= 0 || planeRange.end >= 0) && ((int)planeRange.start <= this.tileCount.x - 1 || (int)planeRange.end <= this.tileCount.x - 1))
					{
						int tileIndex = this.m_Offset + 1 + planeIndex;
						planeRange.Clamp(0, (short)(this.tileCount.x - 1));
						this.tileRanges[tileIndex] = InclusiveRange.Merge(this.tileRanges[tileIndex], planeRange);
						this.tileRanges[tileIndex - 1] = InclusiveRange.Merge(this.tileRanges[tileIndex - 1], planeRange);
					}
				}
			}
			this.m_TileYRange.Clamp(0, (short)(this.tileCount.y - 1));
			for (int planeIndex2 = (int)(this.m_TileYRange.start + 1); planeIndex2 <= (int)this.m_TileYRange.end; planeIndex2++)
			{
				InclusiveRange planeRange2 = InclusiveRange.empty;
				float planeY2 = math.lerp(this.viewPlaneBottoms[this.m_ViewIndex], this.viewPlaneTops[this.m_ViewIndex], (float)planeIndex2 * this.tileScaleInv.y);
				float3 sphereTile0;
				float3 sphereTile;
				TilingJob.GetSphereYPlaneHorizon(CS$<>8__locals1.lightPositionVS, range, this.near, sphereClipRadius, planeY2, out sphereTile0, out sphereTile);
				if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereTile0, ref CS$<>8__locals1))
				{
					planeRange2.Expand((short)math.clamp(this.ViewToTileSpace(sphereTile0).x, 0f, (float)(this.tileCount.x - 1)));
				}
				if (TilingJob.<TileLight>g__SpherePointIsValid|19_0(sphereTile, ref CS$<>8__locals1))
				{
					planeRange2.Expand((short)math.clamp(this.ViewToTileSpace(sphereTile).x, 0f, (float)(this.tileCount.x - 1)));
				}
				int tileIndex2 = this.m_Offset + 1 + planeIndex2;
				this.tileRanges[tileIndex2] = InclusiveRange.Merge(this.tileRanges[tileIndex2], planeRange2);
				this.tileRanges[tileIndex2 - 1] = InclusiveRange.Merge(this.tileRanges[tileIndex2 - 1], planeRange2);
			}
			this.tileRanges[this.m_Offset] = this.m_TileYRange;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002B864 File Offset: 0x00029A64
		private void TileLightOrthographic(int lightIndex)
		{
			TilingJob.<>c__DisplayClass20_0 CS$<>8__locals1;
			CS$<>8__locals1.light = this.lights[lightIndex];
			float4x4 lightToWorld = CS$<>8__locals1.light.localToWorldMatrix;
			CS$<>8__locals1.lightPosVS = math.mul(this.worldToViews[this.m_ViewIndex], math.float4(lightToWorld.c3.xyz, 1f)).xyz;
			CS$<>8__locals1.lightPosVS.z = CS$<>8__locals1.lightPosVS.z * -1f;
			this.ExpandOrthographic(CS$<>8__locals1.lightPosVS);
			CS$<>8__locals1.lightDirVS = math.mul(this.worldToViews[this.m_ViewIndex], math.float4(lightToWorld.c2.xyz, 0f)).xyz;
			CS$<>8__locals1.lightDirVS.z = CS$<>8__locals1.lightDirVS.z * -1f;
			CS$<>8__locals1.lightDirVS = math.normalize(CS$<>8__locals1.lightDirVS);
			float halfAngle = math.radians(CS$<>8__locals1.light.spotAngle * 0.5f);
			float range = CS$<>8__locals1.light.range;
			float rangeSq = TilingJob.square(range);
			CS$<>8__locals1.cosHalfAngle = math.cos(halfAngle);
			float coneHeight = CS$<>8__locals1.cosHalfAngle * range;
			float coneHeightSq = TilingJob.square(coneHeight);
			float coneHeightInv = 1f / coneHeight;
			float coneHeightInvSq = TilingJob.square(coneHeightInv);
			float3 sphereBoundY0 = CS$<>8__locals1.lightPosVS - math.float3(0f, range, 0f);
			float3 sphereBoundY = CS$<>8__locals1.lightPosVS + math.float3(0f, range, 0f);
			float3 sphereBoundX0 = CS$<>8__locals1.lightPosVS - math.float3(range, 0f, 0f);
			float3 sphereBoundX = CS$<>8__locals1.lightPosVS + math.float3(range, 0f, 0f);
			if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereBoundY0, ref CS$<>8__locals1))
			{
				this.ExpandOrthographic(sphereBoundY0);
			}
			if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereBoundY, ref CS$<>8__locals1))
			{
				this.ExpandOrthographic(sphereBoundY);
			}
			if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereBoundX0, ref CS$<>8__locals1))
			{
				this.ExpandOrthographic(sphereBoundX0);
			}
			if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereBoundX, ref CS$<>8__locals1))
			{
				this.ExpandOrthographic(sphereBoundX);
			}
			float3 circleCenter = CS$<>8__locals1.lightPosVS + CS$<>8__locals1.lightDirVS * coneHeight;
			float circleRadius = math.sqrt(rangeSq - coneHeightSq);
			float circleRadiusSq = TilingJob.square(circleRadius);
			float3 circleUp = math.normalize(math.float3(0f, 1f, 0f) - CS$<>8__locals1.lightDirVS * CS$<>8__locals1.lightDirVS.y);
			float3 circleRight = math.normalize(math.float3(1f, 0f, 0f) - CS$<>8__locals1.lightDirVS * CS$<>8__locals1.lightDirVS.x);
			float3 circleBoundY0 = circleCenter - circleUp * circleRadius;
			float3 circleBoundY = circleCenter + circleUp * circleRadius;
			if (CS$<>8__locals1.light.lightType == LightType.Spot)
			{
				float3 circleBoundX0 = circleCenter - circleRight * circleRadius;
				float3 circleBoundX = circleCenter + circleRight * circleRadius;
				this.ExpandOrthographic(circleBoundY0);
				this.ExpandOrthographic(circleBoundY);
				this.ExpandOrthographic(circleBoundX0);
				this.ExpandOrthographic(circleBoundX);
			}
			this.m_TileYRange.Clamp(0, (short)(this.tileCount.y - 1));
			float coneDir0X = 0f;
			float coneDir0YInv = 0f;
			float coneDir1X = 0f;
			float coneDir1YInv = 0f;
			if (CS$<>8__locals1.light.lightType == LightType.Spot)
			{
				float sphereDistance = coneHeight + circleRadiusSq * coneHeightInv;
				float num = math.sqrt(TilingJob.square(circleRadiusSq) * coneHeightInvSq + circleRadiusSq);
				float directionXYSqInv = math.rcp(math.lengthsq(CS$<>8__locals1.lightDirVS.xy));
				float2 polarIntersection = -circleRadiusSq * coneHeightInv * directionXYSqInv * CS$<>8__locals1.lightDirVS.xy;
				float2 polarDir = math.sqrt((TilingJob.square(num) - math.lengthsq(polarIntersection)) * directionXYSqInv) * math.float2(CS$<>8__locals1.lightDirVS.y, -CS$<>8__locals1.lightDirVS.x);
				float2 @float = CS$<>8__locals1.lightPosVS.xy + sphereDistance * CS$<>8__locals1.lightDirVS.xy + polarIntersection;
				float2 coneP0 = @float - polarDir;
				float2 float2 = @float + polarDir;
				coneDir0X = coneP0.x - CS$<>8__locals1.lightPosVS.x;
				coneDir0YInv = math.rcp(coneP0.y - CS$<>8__locals1.lightPosVS.y);
				coneDir1X = float2.x - CS$<>8__locals1.lightPosVS.x;
				coneDir1YInv = math.rcp(float2.y - CS$<>8__locals1.lightPosVS.y);
			}
			for (int planeIndex = (int)(this.m_TileYRange.start + 1); planeIndex <= (int)this.m_TileYRange.end; planeIndex++)
			{
				InclusiveRange planeRange = InclusiveRange.empty;
				float planeY = math.lerp(this.viewPlaneBottoms[this.m_ViewIndex], this.viewPlaneTops[this.m_ViewIndex], (float)planeIndex * this.tileScaleInv.y);
				float sphereX = math.sqrt(rangeSq - TilingJob.square(planeY - CS$<>8__locals1.lightPosVS.y));
				float3 sphereX2 = math.float3(CS$<>8__locals1.lightPosVS.x - sphereX, planeY, CS$<>8__locals1.lightPosVS.z);
				float3 sphereX3 = math.float3(CS$<>8__locals1.lightPosVS.x + sphereX, planeY, CS$<>8__locals1.lightPosVS.z);
				if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereX2, ref CS$<>8__locals1))
				{
					this.ExpandRangeOrthographic(ref planeRange, sphereX2.x);
				}
				if (TilingJob.<TileLightOrthographic>g__SpherePointIsValid|20_0(sphereX3, ref CS$<>8__locals1))
				{
					this.ExpandRangeOrthographic(ref planeRange, sphereX3.x);
				}
				if (CS$<>8__locals1.light.lightType == LightType.Spot)
				{
					if (planeY >= circleBoundY0.y && planeY <= circleBoundY.y)
					{
						float intersectionDistance = (planeY - circleCenter.y) / circleUp.y;
						float num2 = circleCenter.x + intersectionDistance * circleUp.x;
						float intersectionDirX = -CS$<>8__locals1.lightDirVS.z / math.length(math.float3(-CS$<>8__locals1.lightDirVS.z, 0f, CS$<>8__locals1.lightDirVS.x));
						float sideDistance = math.sqrt(TilingJob.square(circleRadius) - TilingJob.square(intersectionDistance));
						float circleX0 = num2 - sideDistance * intersectionDirX;
						float circleX = num2 + sideDistance * intersectionDirX;
						this.ExpandRangeOrthographic(ref planeRange, circleX0);
						this.ExpandRangeOrthographic(ref planeRange, circleX);
					}
					float num3 = planeY - CS$<>8__locals1.lightPosVS.y;
					float coneT0 = num3 * coneDir0YInv;
					float coneT = num3 * coneDir1YInv;
					if (coneT0 >= 0f && coneT0 <= 1f)
					{
						this.ExpandRangeOrthographic(ref planeRange, CS$<>8__locals1.lightPosVS.x + coneT0 * coneDir0X);
					}
					if (coneT >= 0f && coneT <= 1f)
					{
						this.ExpandRangeOrthographic(ref planeRange, CS$<>8__locals1.lightPosVS.x + coneT * coneDir1X);
					}
				}
				int tileIndex = this.m_Offset + 1 + planeIndex;
				this.tileRanges[tileIndex] = InclusiveRange.Merge(this.tileRanges[tileIndex], planeRange);
				this.tileRanges[tileIndex - 1] = InclusiveRange.Merge(this.tileRanges[tileIndex - 1], planeRange);
			}
			this.tileRanges[this.m_Offset] = this.m_TileYRange;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002BF8C File Offset: 0x0002A18C
		private void TileReflectionProbe(int index)
		{
			VisibleReflectionProbe reflectionProbe = this.reflectionProbes[index - this.lights.Length];
			float3 centerWS = reflectionProbe.bounds.center;
			float3 extentsWS = reflectionProbe.bounds.extents;
			NativeArray<float3> points = new NativeArray<float3>(TilingJob.k_CubePoints.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<float2> clippedPoints = new NativeArray<float2>(TilingJob.k_CubePoints.Length + TilingJob.k_CubeLineIndices.Length * 3, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int clippedPointsCount = 0;
			int leftmostIndex = 0;
			for (int i = 0; i < TilingJob.k_CubePoints.Length; i++)
			{
				float3 point = math.mul(this.worldToViews[this.m_ViewIndex], math.float4(centerWS + extentsWS * TilingJob.k_CubePoints[i], 1f)).xyz;
				point.z *= -1f;
				points[i] = point;
				if (point.z >= this.near)
				{
					float2 clippedPoint = (this.isOrthographic ? point.xy : (point.xy / point.z));
					int clippedIndex = clippedPointsCount++;
					clippedPoints[clippedIndex] = clippedPoint;
					if (clippedPoint.x < clippedPoints[leftmostIndex].x)
					{
						leftmostIndex = clippedIndex;
					}
				}
			}
			for (int j = 0; j < TilingJob.k_CubeLineIndices.Length; j++)
			{
				int4 indices = TilingJob.k_CubeLineIndices[j];
				float3 p0 = points[indices.x];
				for (int k = 0; k < 3; k++)
				{
					float3 p = points[indices[k + 1]];
					if ((p0.z >= this.near || p.z >= this.near) && (p0.z < this.near || p.z < this.near))
					{
						float d = (this.near - p0.z) / (p.z - p0.z);
						float3 p2 = math.lerp(p0, p, d);
						float2 clippedPoint2 = (this.isOrthographic ? p2.xy : (p2.xy / p2.z));
						int clippedIndex2 = clippedPointsCount++;
						clippedPoints[clippedIndex2] = clippedPoint2;
						if (clippedPoint2.x < clippedPoints[leftmostIndex].x)
						{
							leftmostIndex = clippedIndex2;
						}
					}
				}
			}
			NativeArray<float2> hullPoints = new NativeArray<float2>(clippedPointsCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int hullPointsCount = 0;
			if (clippedPointsCount > 0)
			{
				int hullPointIndex = leftmostIndex;
				do
				{
					float2 hullPoint = clippedPoints[hullPointIndex];
					this.ExpandY(math.float3(hullPoint, 1f));
					hullPoints[hullPointsCount++] = hullPoint;
					int endpointIndex = 0;
					float2 endpointLine = clippedPoints[endpointIndex] - hullPoint;
					for (int l = 0; l < clippedPointsCount; l++)
					{
						float2 candidateLine = clippedPoints[l] - hullPoint;
						float det = math.determinant(math.float2x2(endpointLine, candidateLine));
						if (endpointIndex == hullPointIndex || det > 0f || (det == 0f && math.lengthsq(candidateLine) > math.lengthsq(endpointLine)))
						{
							endpointIndex = l;
							endpointLine = candidateLine;
						}
					}
					hullPointIndex = endpointIndex;
				}
				while (hullPointIndex != leftmostIndex && hullPointsCount < clippedPointsCount);
				this.m_TileYRange.Clamp(0, (short)(this.tileCount.y - 1));
				for (int planeIndex = (int)(this.m_TileYRange.start + 1); planeIndex <= (int)this.m_TileYRange.end; planeIndex++)
				{
					InclusiveRange planeRange = InclusiveRange.empty;
					float planeY = math.lerp(this.viewPlaneBottoms[this.m_ViewIndex], this.viewPlaneTops[this.m_ViewIndex], (float)planeIndex * this.tileScaleInv.y);
					for (int m = 0; m < hullPointsCount; m++)
					{
						float2 hp0 = hullPoints[m];
						float2 hp = hullPoints[(m + 1) % hullPointsCount];
						float t = (planeY - hp0.y) / (hp.y - hp0.y);
						if (t >= 0f && t <= 1f)
						{
							float3 p3 = math.float3(math.lerp(hp0.x, hp.x, t), planeY, 1f);
							float2 pTS = (this.isOrthographic ? this.ViewToTileSpaceOrthographic(p3) : this.ViewToTileSpace(p3));
							planeRange.Expand((short)math.clamp(pTS.x, 0f, (float)(this.tileCount.x - 1)));
						}
					}
					int tileIndex = this.m_Offset + 1 + planeIndex;
					this.tileRanges[tileIndex] = InclusiveRange.Merge(this.tileRanges[tileIndex], planeRange);
					this.tileRanges[tileIndex - 1] = InclusiveRange.Merge(this.tileRanges[tileIndex - 1], planeRange);
				}
				this.tileRanges[this.m_Offset] = this.m_TileYRange;
			}
			hullPoints.Dispose();
			clippedPoints.Dispose();
			points.Dispose();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
		private float2 ViewToTileSpace(float3 positionVS)
		{
			return (positionVS.xy / positionVS.z * this.viewToViewportScaleBiases[this.m_ViewIndex].xy + this.viewToViewportScaleBiases[this.m_ViewIndex].zw) * this.tileScale;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002C518 File Offset: 0x0002A718
		private float2 ViewToTileSpaceOrthographic(float3 positionVS)
		{
			return (positionVS.xy * this.viewToViewportScaleBiases[this.m_ViewIndex].xy + this.viewToViewportScaleBiases[this.m_ViewIndex].zw) * this.tileScale;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002C574 File Offset: 0x0002A774
		private void ExpandY(float3 positionVS)
		{
			float2 @float = this.ViewToTileSpace(positionVS);
			int tileY = (int)@float.y;
			int tileX = (int)@float.x;
			this.m_TileYRange.Expand((short)math.clamp(tileY, 0, this.tileCount.y - 1));
			if (tileY >= 0 && tileY < this.tileCount.y && tileX >= 0 && tileX < this.tileCount.x)
			{
				InclusiveRange rowXRange = this.tileRanges[this.m_Offset + 1 + tileY];
				rowXRange.Expand((short)tileX);
				this.tileRanges[this.m_Offset + 1 + tileY] = rowXRange;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002C610 File Offset: 0x0002A810
		private void ExpandOrthographic(float3 positionVS)
		{
			float2 @float = this.ViewToTileSpaceOrthographic(positionVS);
			int tileY = (int)@float.y;
			int tileX = (int)@float.x;
			this.m_TileYRange.Expand((short)math.clamp(tileY, 0, this.tileCount.y - 1));
			if (tileY >= 0 && tileY < this.tileCount.y && tileX >= 0 && tileX < this.tileCount.x)
			{
				InclusiveRange rowXRange = this.tileRanges[this.m_Offset + 1 + tileY];
				rowXRange.Expand((short)tileX);
				this.tileRanges[this.m_Offset + 1 + tileY] = rowXRange;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002C6AC File Offset: 0x0002A8AC
		private void ExpandRangeOrthographic(ref InclusiveRange range, float xVS)
		{
			range.Expand((short)math.clamp(this.ViewToTileSpaceOrthographic(xVS).x, 0f, (float)(this.tileCount.x - 1)));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002C6DE File Offset: 0x0002A8DE
		private static float square(float x)
		{
			return x * x;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		private static void GetSphereHorizon(float2 center, float radius, float near, float clipRadius, out float2 p0, out float2 p1)
		{
			float2 direction = math.normalize(center);
			float d = math.length(center);
			float i = math.sqrt(d * d - radius * radius);
			float h = i * radius / d;
			float2 @float = direction * (i * h / radius);
			p0 = math.float2(float.MinValue, 1f);
			p1 = math.float2(float.MaxValue, 1f);
			if (center.y - radius < near)
			{
				p0 = math.float2(center.x + clipRadius, near);
				p1 = math.float2(center.x - clipRadius, near);
			}
			float2 c0 = @float + math.float2(-direction.y, direction.x) * h;
			if (TilingJob.square(d) >= TilingJob.square(radius) && c0.y >= near)
			{
				if (c0.x > p0.x)
				{
					p0 = c0;
				}
				if (c0.x < p1.x)
				{
					p1 = c0;
				}
			}
			float2 c = @float + math.float2(direction.y, -direction.x) * h;
			if (TilingJob.square(d) >= TilingJob.square(radius) && c.y >= near)
			{
				if (c.x > p0.x)
				{
					p0 = c;
				}
				if (c.x < p1.x)
				{
					p1 = c;
				}
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002C854 File Offset: 0x0002AA54
		private static void GetSphereYPlaneHorizon(float3 center, float sphereRadius, float near, float clipRadius, float y, out float3 left, out float3 right)
		{
			float yNear = y * near;
			float clipHalfWidth = math.sqrt(TilingJob.square(clipRadius) - TilingJob.square(yNear - center.y));
			left = math.float3(center.x - clipHalfWidth, yNear, near);
			right = math.float3(center.x + clipHalfWidth, yNear, near);
			float3 planeU = math.normalize(math.float3(0f, y, 1f));
			float3 planeV = math.float3(1f, 0f, 0f);
			float distanceToPlane = math.abs(math.dot(math.normalize(math.float3(0f, 1f, -y)), center));
			float2 @float = math.float2(math.dot(center, planeU), math.dot(center, planeV));
			float distanceInPlane = math.length(@float);
			float2 directionPS = @float / distanceInPlane;
			float circleRadius = math.sqrt(TilingJob.square(sphereRadius) - TilingJob.square(distanceToPlane));
			if (TilingJob.square(distanceToPlane) <= TilingJob.square(sphereRadius) && TilingJob.square(circleRadius) <= TilingJob.square(distanceInPlane))
			{
				float i = math.sqrt(TilingJob.square(distanceInPlane) - TilingJob.square(circleRadius));
				float h = i * circleRadius / distanceInPlane;
				float2 float2 = directionPS * (i * h / circleRadius);
				float2 leftOnPlane = float2 + math.float2(directionPS.y, -directionPS.x) * h;
				float2 rightOnPlane = float2 + math.float2(-directionPS.y, directionPS.x) * h;
				float3 leftCandidate = leftOnPlane.x * planeU + leftOnPlane.y * planeV;
				if (leftCandidate.z >= near)
				{
					left = leftCandidate;
				}
				float3 rightCandidate = rightOnPlane.x * planeU + rightOnPlane.y * planeV;
				if (rightCandidate.z >= near)
				{
					right = rightCandidate;
				}
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002CA38 File Offset: 0x0002AC38
		private static bool GetCircleClipPoints(float3 circleCenter, float3 circleNormal, float circleRadius, float near, out float3 p0, out float3 p1)
		{
			float3 lineDirection = math.normalize(math.cross(circleNormal, math.float3(0f, 0f, 1f)));
			float3 nearestDirection = math.cross(lineDirection, circleNormal);
			float distance = (near - circleCenter.z) / nearestDirection.z;
			float3 nearestPoint = circleCenter + nearestDirection * distance;
			float chordHalfLength = math.sqrt(TilingJob.square(circleRadius) - TilingJob.square(distance));
			p0 = nearestPoint + lineDirection * chordHalfLength;
			p1 = nearestPoint - lineDirection * chordHalfLength;
			return math.abs(distance) <= circleRadius;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		private static ValueTuple<float, float> IntersectEllipseLine(float a, float b, float3 line)
		{
			float div = math.rcp(TilingJob.square(line.y) * TilingJob.square(b));
			float qa = 1f / TilingJob.square(a) + TilingJob.square(line.x) * div;
			float num = 2f * line.x * line.z * div;
			float qc = TilingJob.square(line.z) * div - 1f;
			float sqrtD = math.sqrt(num * num - 4f * qa * qc);
			float x = (-num + sqrtD) / (2f * qa);
			float x2 = (-num - sqrtD) / (2f * qa);
			return new ValueTuple<float, float>(x, x2);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002CB78 File Offset: 0x0002AD78
		private static void GetProjectedCircleHorizon(float2 center, float radius, float2 U, float2 V, out float2 uv1, out float2 uv2)
		{
			float vl = math.length(V);
			if (vl < 1E-06f)
			{
				uv1 = math.float2(radius, 0f);
				uv2 = math.float2(-radius, 0f);
				return;
			}
			float num = math.length(U);
			float ulinv = math.rcp(num);
			float vlinv = math.rcp(vl);
			float2 u = U * ulinv;
			float2 v = V * vlinv;
			float a = num * radius;
			float b = vl * radius;
			float2 cameraUV = math.float2(math.dot(-center, u), math.dot(-center, v));
			float3 polar = math.float3(cameraUV.x / TilingJob.square(a), cameraUV.y / TilingJob.square(b), -1f);
			ValueTuple<float, float> valueTuple = TilingJob.IntersectEllipseLine(a, b, polar);
			float t = valueTuple.Item1;
			float t2 = valueTuple.Item2;
			uv1 = math.float2(t * ulinv, (-polar.x / polar.y * t - polar.z / polar.y) * vlinv);
			uv2 = math.float2(t2 * ulinv, (-polar.x / polar.y * t2 - polar.z / polar.y) * vlinv);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		private static bool IntersectCircleYPlane(float y, float3 circleCenter, float3 circleNormal, float3 circleU, float3 circleV, float circleRadius, out float3 p1, out float3 p2)
		{
			p1 = (p2 = 0);
			float CdotN = math.dot(circleCenter, circleNormal);
			float3 h1v = math.float3(1f, y, 1f) * CdotN / math.dot(math.float3(1f, y, 1f), circleNormal) - circleCenter;
			float2 h = math.float2(math.dot(h1v, circleU), math.dot(h1v, circleV));
			float3 h2v = math.float3(-1f, y, 1f) * CdotN / math.dot(math.float3(-1f, y, 1f), circleNormal) - circleCenter;
			float2 lineDirection = math.normalize(math.float2(math.dot(h2v, circleU), math.dot(h2v, circleV)) - h);
			float2 lineNormal = math.float2(lineDirection.y, -lineDirection.x);
			float distToLine = math.dot(h, lineNormal);
			float2 lineCenter = lineNormal * distToLine;
			if (distToLine > circleRadius)
			{
				return false;
			}
			float i = math.sqrt(circleRadius * circleRadius - distToLine * distToLine);
			float2 x = lineCenter + i * lineDirection;
			float2 x2 = lineCenter - i * lineDirection;
			p1 = circleCenter + x.x * circleU + x.y * circleV;
			p2 = circleCenter + x2.x * circleU + x2.y * circleV;
			return true;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002CE50 File Offset: 0x0002B050
		private static void GetConeSideTangentPoints(float3 vertex, float3 axis, float cosHalfAngle, float circleRadius, float coneHeight, float range, float3 circleU, float3 circleV, out float3 l1, out float3 l2)
		{
			l1 = (l2 = 0);
			if (math.dot(math.normalize(-vertex), axis) >= cosHalfAngle)
			{
				return;
			}
			float d = -math.dot(vertex, axis);
			if (d == 0f)
			{
				d = 1E-06f;
			}
			float sign = ((d < 0f) ? (-1f) : 1f);
			float3 origin = vertex + axis * d;
			float radius = math.abs(d) * circleRadius / coneHeight;
			float3 polar = math.float3(math.float2(math.dot(circleU, -origin), math.dot(circleV, -origin)), -TilingJob.square(radius));
			float2 p = math.float2(-1f, -polar.x / polar.y * -1f - polar.z / polar.y);
			float2 lineDirection = math.normalize(math.float2(1f, -polar.x / polar.y * 1f - polar.z / polar.y) - p);
			float2 lineNormal = math.float2(lineDirection.y, -lineDirection.x);
			float distToLine = math.dot(p, lineNormal);
			float2 @float = lineNormal * distToLine;
			float i = math.sqrt(radius * radius - distToLine * distToLine);
			float2 x1UV = @float + i * lineDirection;
			float2 x2UV = @float - i * lineDirection;
			float3 dir = math.normalize(origin + x1UV.x * circleU + x1UV.y * circleV - vertex) * sign;
			float3 dir2 = math.normalize(origin + x2UV.x * circleU + x2UV.y * circleV - vertex) * sign;
			l1 = dir * range;
			l2 = dir2 * range;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002D060 File Offset: 0x0002B260
		private static float3 EvaluateNearConic(float near, float3 o, float3 d, float r, float3 u, float3 v, float theta)
		{
			float h = (near - o.z) / (d.z + r * u.z * math.cos(theta) + r * v.z * math.sin(theta));
			return math.float3(o.xy + h * (d.xy + r * u.xy * math.cos(theta) + r * v.xy * math.sin(theta)), near);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002D0FC File Offset: 0x0002B2FC
		private static float2 FindNearConicTangentTheta(float2 o, float2 d, float r, float2 u, float2 v)
		{
			float sqrt = math.sqrt(TilingJob.square(d.x) * TilingJob.square(u.y) + TilingJob.square(d.x) * TilingJob.square(v.y) - 2f * d.x * d.y * u.x * u.y - 2f * d.x * d.y * v.x * v.y + TilingJob.square(d.y) * TilingJob.square(u.x) + TilingJob.square(d.y) * TilingJob.square(v.x) - TilingJob.square(r) * TilingJob.square(u.x) * TilingJob.square(v.y) + 2f * TilingJob.square(r) * u.x * u.y * v.x * v.y - TilingJob.square(r) * TilingJob.square(u.y) * TilingJob.square(v.x));
			float denom = d.x * v.y - d.y * v.x - r * u.x * v.y + r * u.y * v.x;
			return 2f * math.atan((-d.x * u.y + d.y * u.x + math.float2(1f, -1f) * sqrt) / denom);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		private static float2 FindNearConicYTheta(float near, float3 o, float3 d, float r, float3 u, float3 v, float y)
		{
			float sqrt = math.sqrt(-TilingJob.square(d.y) * TilingJob.square(o.z) + 2f * TilingJob.square(d.y) * o.z * near - TilingJob.square(d.y) * TilingJob.square(near) + 2f * d.y * d.z * o.y * o.z - 2f * d.y * d.z * o.y * near - 2f * d.y * d.z * o.z * y + 2f * d.y * d.z * y * near - TilingJob.square(d.z) * TilingJob.square(o.y) + 2f * TilingJob.square(d.z) * o.y * y - TilingJob.square(d.z) * TilingJob.square(y) + TilingJob.square(o.y) * TilingJob.square(r) * TilingJob.square(u.z) + TilingJob.square(o.y) * TilingJob.square(r) * TilingJob.square(v.z) - 2f * o.y * o.z * TilingJob.square(r) * u.y * u.z - 2f * o.y * o.z * TilingJob.square(r) * v.y * v.z - 2f * o.y * y * TilingJob.square(r) * TilingJob.square(u.z) - 2f * o.y * y * TilingJob.square(r) * TilingJob.square(v.z) + 2f * o.y * TilingJob.square(r) * u.y * u.z * near + 2f * o.y * TilingJob.square(r) * v.y * v.z * near + TilingJob.square(o.z) * TilingJob.square(r) * TilingJob.square(u.y) + TilingJob.square(o.z) * TilingJob.square(r) * TilingJob.square(v.y) + 2f * o.z * y * TilingJob.square(r) * u.y * u.z + 2f * o.z * y * TilingJob.square(r) * v.y * v.z - 2f * o.z * TilingJob.square(r) * TilingJob.square(u.y) * near - 2f * o.z * TilingJob.square(r) * TilingJob.square(v.y) * near + TilingJob.square(y) * TilingJob.square(r) * TilingJob.square(u.z) + TilingJob.square(y) * TilingJob.square(r) * TilingJob.square(v.z) - 2f * y * TilingJob.square(r) * u.y * u.z * near - 2f * y * TilingJob.square(r) * v.y * v.z * near + TilingJob.square(r) * TilingJob.square(u.y) * TilingJob.square(near) + TilingJob.square(r) * TilingJob.square(v.y) * TilingJob.square(near));
			float denom = d.y * o.z - d.y * near - d.z * o.y + d.z * y + o.y * r * u.z - o.z * r * u.y - y * r * u.z + r * u.y * near;
			return 2f * math.atan((r * (o.y * v.z - o.z * v.y - y * v.z + v.y * near) + math.float2(1f, -1f) * sqrt) / denom);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002D87B File Offset: 0x0002BA7B
		[CompilerGenerated]
		internal static bool <TileLight>g__SpherePointIsValid|19_0(float3 p, ref TilingJob.<>c__DisplayClass19_0 A_1)
		{
			return A_1.light.lightType == LightType.Point || math.dot(math.normalize(p - A_1.lightPositionVS), A_1.lightDirectionVS) >= A_1.cosHalfAngle;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		[CompilerGenerated]
		internal static bool <TileLight>g__ConicPointIsValid|19_1(float3 p, ref TilingJob.<>c__DisplayClass19_0 A_1)
		{
			return math.dot(math.normalize(p - A_1.lightPositionVS), A_1.lightDirectionVS) >= 0f && math.dot(p - A_1.lightPositionVS, A_1.lightDirectionVS) <= A_1.coneHeight;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002D908 File Offset: 0x0002BB08
		[CompilerGenerated]
		internal static bool <TileLightOrthographic>g__SpherePointIsValid|20_0(float3 p, ref TilingJob.<>c__DisplayClass20_0 A_1)
		{
			return A_1.light.lightType == LightType.Point || math.dot(math.normalize(p - A_1.lightPosVS), A_1.lightDirVS) >= A_1.cosHalfAngle;
		}

		// Token: 0x04000934 RID: 2356
		[ReadOnly]
		public NativeArray<VisibleLight> lights;

		// Token: 0x04000935 RID: 2357
		[ReadOnly]
		public NativeArray<VisibleReflectionProbe> reflectionProbes;

		// Token: 0x04000936 RID: 2358
		[NativeDisableParallelForRestriction]
		public NativeArray<InclusiveRange> tileRanges;

		// Token: 0x04000937 RID: 2359
		public int itemsPerTile;

		// Token: 0x04000938 RID: 2360
		public int rangesPerItem;

		// Token: 0x04000939 RID: 2361
		public Fixed2<float4x4> worldToViews;

		// Token: 0x0400093A RID: 2362
		public float2 tileScale;

		// Token: 0x0400093B RID: 2363
		public float2 tileScaleInv;

		// Token: 0x0400093C RID: 2364
		public Fixed2<float> viewPlaneBottoms;

		// Token: 0x0400093D RID: 2365
		public Fixed2<float> viewPlaneTops;

		// Token: 0x0400093E RID: 2366
		public Fixed2<float4> viewToViewportScaleBiases;

		// Token: 0x0400093F RID: 2367
		public int2 tileCount;

		// Token: 0x04000940 RID: 2368
		public float near;

		// Token: 0x04000941 RID: 2369
		public bool isOrthographic;

		// Token: 0x04000942 RID: 2370
		private InclusiveRange m_TileYRange;

		// Token: 0x04000943 RID: 2371
		private int m_Offset;

		// Token: 0x04000944 RID: 2372
		private int m_ViewIndex;

		// Token: 0x04000945 RID: 2373
		private float2 m_CenterOffset;

		// Token: 0x04000946 RID: 2374
		private static readonly float3[] k_CubePoints = new float3[]
		{
			new float3(-1f, -1f, -1f),
			new float3(-1f, -1f, 1f),
			new float3(-1f, 1f, -1f),
			new float3(-1f, 1f, 1f),
			new float3(1f, -1f, -1f),
			new float3(1f, -1f, 1f),
			new float3(1f, 1f, -1f),
			new float3(1f, 1f, 1f)
		};

		// Token: 0x04000947 RID: 2375
		private static readonly int4[] k_CubeLineIndices = new int4[]
		{
			new int4(0, 4, 2, 1),
			new int4(3, 7, 1, 2),
			new int4(5, 1, 7, 4),
			new int4(6, 2, 4, 7)
		};
	}
}
