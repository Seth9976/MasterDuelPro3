using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000085 RID: 133
	internal class DecalCreateDrawCallSystem
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000A1D7 File Offset: 0x000083D7
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000A1DF File Offset: 0x000083DF
		public float maxDrawDistance
		{
			get
			{
				return this.m_MaxDrawDistance;
			}
			set
			{
				this.m_MaxDrawDistance = value;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public DecalCreateDrawCallSystem(DecalEntityManager entityManager, float maxDrawDistance)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalCreateDrawCallSystem.Execute");
			this.m_MaxDrawDistance = maxDrawDistance;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A210 File Offset: 0x00008410
		public void Execute()
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.cachedChunks[i], this.m_EntityManager.culledChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.cachedChunks[i].count);
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A2B0 File Offset: 0x000084B0
		private void Execute(DecalCachedChunk cachedChunk, DecalCulledChunk culledChunk, DecalDrawCallChunk drawCallChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			JobHandle handle = new DecalCreateDrawCallSystem.DrawCallJob
			{
				decalToWorlds = cachedChunk.decalToWorlds,
				normalToWorlds = cachedChunk.normalToWorlds,
				sizeOffsets = cachedChunk.sizeOffsets,
				drawDistances = cachedChunk.drawDistances,
				angleFades = cachedChunk.angleFades,
				uvScaleBiases = cachedChunk.uvScaleBias,
				layerMasks = cachedChunk.layerMasks,
				sceneLayerMasks = cachedChunk.sceneLayerMasks,
				fadeFactors = cachedChunk.fadeFactors,
				boundingSpheres = cachedChunk.boundingSpheres,
				renderingLayerMasks = cachedChunk.renderingLayerMasks,
				cameraPosition = culledChunk.cameraPosition,
				sceneCullingMask = culledChunk.sceneCullingMask,
				cullingMask = culledChunk.cullingMask,
				visibleDecalIndices = culledChunk.visibleDecalIndices,
				visibleDecalCount = culledChunk.visibleDecalCount,
				maxDrawDistance = this.m_MaxDrawDistance,
				decalToWorldsDraw = drawCallChunk.decalToWorlds,
				normalToDecalsDraw = drawCallChunk.normalToDecals,
				renderingLayerMasksDraw = drawCallChunk.renderingLayerMasks,
				subCalls = drawCallChunk.subCalls,
				subCallCount = drawCallChunk.subCallCounts
			}.Schedule(cachedChunk.currentJobHandle);
			drawCallChunk.currentJobHandle = handle;
			cachedChunk.currentJobHandle = handle;
		}

		// Token: 0x04000264 RID: 612
		private DecalEntityManager m_EntityManager;

		// Token: 0x04000265 RID: 613
		private ProfilingSampler m_Sampler;

		// Token: 0x04000266 RID: 614
		private float m_MaxDrawDistance;

		// Token: 0x02000086 RID: 134
		[BurstCompile]
		private struct DrawCallJob : IJob
		{
			// Token: 0x06000315 RID: 789 RVA: 0x0000A404 File Offset: 0x00008604
			public void Execute()
			{
				int subCallIndex = 0;
				int instanceIndex = 0;
				int instanceStart = 0;
				for (int i = 0; i < this.visibleDecalCount; i++)
				{
					int decalIndex = this.visibleDecalIndices[i];
					int decalMask = 1 << this.layerMasks[decalIndex];
					if ((this.cullingMask & decalMask) != 0)
					{
						BoundingSphere boundingSphere = this.boundingSpheres[decalIndex];
						float2 drawDistance = this.drawDistances[decalIndex];
						float distanceToDecal = (this.cameraPosition - boundingSphere.position).magnitude;
						float cullDistance = math.min(drawDistance.x, this.maxDrawDistance) + boundingSphere.radius;
						if (distanceToDecal <= cullDistance)
						{
							this.decalToWorldsDraw[instanceIndex] = this.decalToWorlds[decalIndex];
							float num = this.fadeFactors[decalIndex];
							float2 angleFade = this.angleFades[decalIndex];
							float4 uvScaleBias = this.uvScaleBiases[decalIndex];
							float4x4 normalToDecals = this.normalToWorlds[decalIndex];
							float fadeFactor = num * math.clamp((cullDistance - distanceToDecal) / (cullDistance * (1f - drawDistance.y)), 0f, 1f);
							normalToDecals.c0.w = uvScaleBias.x;
							normalToDecals.c1.w = uvScaleBias.y;
							normalToDecals.c2.w = uvScaleBias.z;
							normalToDecals.c3 = new float4(fadeFactor * 1f, angleFade.x, angleFade.y, uvScaleBias.w);
							this.normalToDecalsDraw[instanceIndex] = normalToDecals;
							this.renderingLayerMasksDraw[instanceIndex] = this.renderingLayerMasks[decalIndex];
							instanceIndex++;
							if ((long)(instanceIndex - instanceStart) >= (long)((ulong)DecalDrawSystem.MaxBatchSize))
							{
								this.subCalls[subCallIndex++] = new DecalSubDrawCall
								{
									start = instanceStart,
									end = instanceIndex
								};
								instanceStart = instanceIndex;
							}
						}
					}
				}
				if (instanceIndex - instanceStart != 0)
				{
					this.subCalls[subCallIndex++] = new DecalSubDrawCall
					{
						start = instanceStart,
						end = instanceIndex
					};
				}
				this.subCallCount[0] = subCallIndex;
			}

			// Token: 0x04000267 RID: 615
			[ReadOnly]
			public NativeArray<float4x4> decalToWorlds;

			// Token: 0x04000268 RID: 616
			[ReadOnly]
			public NativeArray<float4x4> normalToWorlds;

			// Token: 0x04000269 RID: 617
			[ReadOnly]
			public NativeArray<float4x4> sizeOffsets;

			// Token: 0x0400026A RID: 618
			[ReadOnly]
			public NativeArray<float2> drawDistances;

			// Token: 0x0400026B RID: 619
			[ReadOnly]
			public NativeArray<float2> angleFades;

			// Token: 0x0400026C RID: 620
			[ReadOnly]
			public NativeArray<float4> uvScaleBiases;

			// Token: 0x0400026D RID: 621
			[ReadOnly]
			public NativeArray<int> layerMasks;

			// Token: 0x0400026E RID: 622
			[ReadOnly]
			public NativeArray<ulong> sceneLayerMasks;

			// Token: 0x0400026F RID: 623
			[ReadOnly]
			public NativeArray<float> fadeFactors;

			// Token: 0x04000270 RID: 624
			[ReadOnly]
			public NativeArray<BoundingSphere> boundingSpheres;

			// Token: 0x04000271 RID: 625
			[ReadOnly]
			public NativeArray<uint> renderingLayerMasks;

			// Token: 0x04000272 RID: 626
			public Vector3 cameraPosition;

			// Token: 0x04000273 RID: 627
			public ulong sceneCullingMask;

			// Token: 0x04000274 RID: 628
			public int cullingMask;

			// Token: 0x04000275 RID: 629
			[ReadOnly]
			public NativeArray<int> visibleDecalIndices;

			// Token: 0x04000276 RID: 630
			public int visibleDecalCount;

			// Token: 0x04000277 RID: 631
			public float maxDrawDistance;

			// Token: 0x04000278 RID: 632
			[WriteOnly]
			public NativeArray<float4x4> decalToWorldsDraw;

			// Token: 0x04000279 RID: 633
			[WriteOnly]
			public NativeArray<float4x4> normalToDecalsDraw;

			// Token: 0x0400027A RID: 634
			[WriteOnly]
			public NativeArray<float> renderingLayerMasksDraw;

			// Token: 0x0400027B RID: 635
			[WriteOnly]
			public NativeArray<DecalSubDrawCall> subCalls;

			// Token: 0x0400027C RID: 636
			[WriteOnly]
			public NativeArray<int> subCallCount;
		}
	}
}
