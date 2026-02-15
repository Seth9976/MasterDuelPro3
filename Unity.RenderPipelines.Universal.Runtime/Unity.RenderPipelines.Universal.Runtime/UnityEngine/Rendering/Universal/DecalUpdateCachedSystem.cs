using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000091 RID: 145
	internal class DecalUpdateCachedSystem
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public DecalUpdateCachedSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCachedSystem.Execute");
			this.m_SamplerJob = new ProfilingSampler("DecalUpdateCachedSystem.ExecuteJob");
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public void Execute()
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C37C File Offset: 0x0000A57C
		private void Execute(DecalEntityChunk entityChunk, DecalCachedChunk cachedChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			cachedChunk.currentJobHandle.Complete();
			Material material = entityChunk.material;
			if (material.HasProperty("_DrawOrder"))
			{
				cachedChunk.drawOrder = material.GetInt("_DrawOrder");
			}
			if (!cachedChunk.isCreated)
			{
				int passIndexDBuffer = material.FindPass("DBufferProjector");
				cachedChunk.passIndexDBuffer = passIndexDBuffer;
				int passIndexEmissive = material.FindPass("DecalProjectorForwardEmissive");
				cachedChunk.passIndexEmissive = passIndexEmissive;
				int passIndexScreenSpace = material.FindPass("DecalScreenSpaceProjector");
				cachedChunk.passIndexScreenSpace = passIndexScreenSpace;
				int passIndexGBuffer = material.FindPass("DecalGBufferProjector");
				cachedChunk.passIndexGBuffer = passIndexGBuffer;
				cachedChunk.isCreated = true;
			}
			using (new ProfilingScope(this.m_SamplerJob))
			{
				JobHandle handle = new DecalUpdateCachedSystem.UpdateTransformsJob
				{
					positions = cachedChunk.positions,
					rotations = cachedChunk.rotation,
					scales = cachedChunk.scales,
					dirty = cachedChunk.dirty,
					scaleModes = cachedChunk.scaleModes,
					sizeOffsets = cachedChunk.sizeOffsets,
					decalToWorlds = cachedChunk.decalToWorlds,
					normalToWorlds = cachedChunk.normalToWorlds,
					boundingSpheres = cachedChunk.boundingSpheres,
					minDistance = float.Epsilon
				}.Schedule(entityChunk.transformAccessArray, default(JobHandle));
				cachedChunk.currentJobHandle = handle;
			}
		}

		// Token: 0x040002C0 RID: 704
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002C1 RID: 705
		private ProfilingSampler m_Sampler;

		// Token: 0x040002C2 RID: 706
		private ProfilingSampler m_SamplerJob;

		// Token: 0x02000092 RID: 146
		[BurstCompile]
		public struct UpdateTransformsJob : IJobParallelForTransform
		{
			// Token: 0x0600034B RID: 843 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
			private float DistanceBetweenQuaternions(quaternion a, quaternion b)
			{
				return math.distancesq(a.value, b.value);
			}

			// Token: 0x0600034C RID: 844 RVA: 0x0000C50C File Offset: 0x0000A70C
			public void Execute(int index, TransformAccess transform)
			{
				bool flag = math.distancesq(transform.position, this.positions[index]) > this.minDistance;
				if (flag)
				{
					this.positions[index] = transform.position;
				}
				bool rotationChanged = this.DistanceBetweenQuaternions(transform.rotation, this.rotations[index]) > this.minDistance;
				if (rotationChanged)
				{
					this.rotations[index] = transform.rotation;
				}
				bool scaleChanged = math.distancesq(transform.localScale, this.scales[index]) > this.minDistance;
				if (scaleChanged)
				{
					this.scales[index] = transform.localScale;
				}
				if (!flag && !rotationChanged && !scaleChanged && !this.dirty[index])
				{
					return;
				}
				float4x4 localToWorld;
				if (this.scaleModes[index] == DecalScaleMode.InheritFromHierarchy)
				{
					localToWorld = transform.localToWorldMatrix;
					localToWorld = math.mul(localToWorld, new float4x4(DecalUpdateCachedSystem.UpdateTransformsJob.k_MinusYtoZRotation, float3.zero));
				}
				else
				{
					quaternion rotation = math.mul(transform.rotation, DecalUpdateCachedSystem.UpdateTransformsJob.k_MinusYtoZRotation);
					localToWorld = float4x4.TRS(this.positions[index], rotation, new float3(1f, 1f, 1f));
				}
				float4x4 decalRotation = localToWorld;
				float4 temp = decalRotation.c1;
				decalRotation.c1 = decalRotation.c2;
				decalRotation.c2 = temp;
				this.normalToWorlds[index] = decalRotation;
				float4x4 sizeOffset = this.sizeOffsets[index];
				float4x4 decalToWorld = math.mul(localToWorld, sizeOffset);
				this.decalToWorlds[index] = decalToWorld;
				this.boundingSpheres[index] = this.GetDecalProjectBoundingSphere(decalToWorld);
				this.dirty[index] = false;
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
			private BoundingSphere GetDecalProjectBoundingSphere(Matrix4x4 decalToWorld)
			{
				float4 min = new float4(-0.5f, -0.5f, -0.5f, 1f);
				float4 max = new float4(0.5f, 0.5f, 0.5f, 1f);
				min = math.mul(decalToWorld, min);
				max = math.mul(decalToWorld, max);
				float3 position = ((max + min) / 2f).xyz;
				float radius = math.length(max - min) / 2f;
				return new BoundingSphere
				{
					position = position,
					radius = radius
				};
			}

			// Token: 0x040002C3 RID: 707
			private static readonly quaternion k_MinusYtoZRotation = quaternion.EulerXYZ(-1.5707964f, 0f, 0f);

			// Token: 0x040002C4 RID: 708
			public NativeArray<float3> positions;

			// Token: 0x040002C5 RID: 709
			public NativeArray<quaternion> rotations;

			// Token: 0x040002C6 RID: 710
			public NativeArray<float3> scales;

			// Token: 0x040002C7 RID: 711
			public NativeArray<bool> dirty;

			// Token: 0x040002C8 RID: 712
			[ReadOnly]
			public NativeArray<DecalScaleMode> scaleModes;

			// Token: 0x040002C9 RID: 713
			[ReadOnly]
			public NativeArray<float4x4> sizeOffsets;

			// Token: 0x040002CA RID: 714
			[WriteOnly]
			public NativeArray<float4x4> decalToWorlds;

			// Token: 0x040002CB RID: 715
			[WriteOnly]
			public NativeArray<float4x4> normalToWorlds;

			// Token: 0x040002CC RID: 716
			[WriteOnly]
			public NativeArray<BoundingSphere> boundingSpheres;

			// Token: 0x040002CD RID: 717
			public float minDistance;
		}
	}
}
