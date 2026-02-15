using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000087 RID: 135
	internal abstract class DecalDrawSystem
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000A641 File Offset: 0x00008841
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000A649 File Offset: 0x00008849
		public Material overrideMaterial { get; set; }

		// Token: 0x06000318 RID: 792 RVA: 0x0000A654 File Offset: 0x00008854
		public DecalDrawSystem(string sampler, DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_WorldToDecals = new Matrix4x4[DecalDrawSystem.MaxBatchSize];
			this.m_NormalToDecals = new Matrix4x4[DecalDrawSystem.MaxBatchSize];
			this.m_DecalLayerMasks = new float[DecalDrawSystem.MaxBatchSize];
			this.m_Sampler = new ProfilingSampler(sampler);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A6AA File Offset: 0x000088AA
		public void Execute(CommandBuffer cmd)
		{
			this.Execute(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A6B8 File Offset: 0x000088B8
		internal void Execute(RasterCommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(cmd, this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A75C File Offset: 0x0000895C
		protected virtual Material GetMaterial(DecalEntityChunk decalEntityChunk)
		{
			return decalEntityChunk.material;
		}

		// Token: 0x0600031C RID: 796
		protected abstract int GetPassIndex(DecalCachedChunk decalCachedChunk);

		// Token: 0x0600031D RID: 797 RVA: 0x0000A764 File Offset: 0x00008964
		private void Execute(RasterCommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			Material material = this.GetMaterial(decalEntityChunk);
			int passIndex = this.GetPassIndex(decalCachedChunk);
			if (count == 0 || passIndex == -1 || material == null)
			{
				return;
			}
			if (SystemInfo.supportsInstancing && material.enableInstancing)
			{
				this.DrawInstanced(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
				return;
			}
			this.Draw(cmd, decalEntityChunk, decalCachedChunk, decalDrawCallChunk, passIndex);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A7D8 File Offset: 0x000089D8
		private void Draw(RasterCommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			Mesh mesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall subCall = decalDrawCallChunk.subCalls[i];
				for (int j = subCall.start; j < subCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					decalCachedChunk.propertyBlock.SetFloat("_DecalLayerMaskFromDecal", decalDrawCallChunk.renderingLayerMasks[j]);
					cmd.DrawMesh(mesh, decalDrawCallChunk.decalToWorlds[j], material, 0, passIndex, decalCachedChunk.propertyBlock);
				}
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A8CC File Offset: 0x00008ACC
		private void DrawInstanced(RasterCommandBuffer cmd, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int passIndex)
		{
			Mesh mesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall subCall = decalDrawCallChunk.subCalls[i];
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<Matrix4x4>(), subCall.start, this.m_WorldToDecals, 0, subCall.count);
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<Matrix4x4>(), subCall.start, this.m_NormalToDecals, 0, subCall.count);
				NativeArray<float>.Copy(decalDrawCallChunk.renderingLayerMasks.Reinterpret<float>(), subCall.start, this.m_DecalLayerMasks, 0, subCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", this.m_NormalToDecals);
				decalCachedChunk.propertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", this.m_DecalLayerMasks);
				cmd.DrawMeshInstanced(mesh, 0, material, passIndex, this.m_WorldToDecals, subCall.end - subCall.start, decalCachedChunk.propertyBlock);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000AA04 File Offset: 0x00008C04
		public void Execute(in CameraData cameraData)
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(in cameraData, this.m_EntityManager.entityChunks[i], this.m_EntityManager.cachedChunks[i], this.m_EntityManager.drawCallChunks[i], this.m_EntityManager.entityChunks[i].count);
				}
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		private void Execute(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk, int count)
		{
			decalCachedChunk.currentJobHandle.Complete();
			decalDrawCallChunk.currentJobHandle.Complete();
			Material material = this.GetMaterial(decalEntityChunk);
			int passIndex = this.GetPassIndex(decalCachedChunk);
			if (count == 0 || passIndex == -1 || material == null)
			{
				return;
			}
			if (SystemInfo.supportsInstancing && material.enableInstancing)
			{
				this.DrawInstanced(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
				return;
			}
			this.Draw(in cameraData, decalEntityChunk, decalCachedChunk, decalDrawCallChunk);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000AB18 File Offset: 0x00008D18
		private unsafe void Draw(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk)
		{
			Mesh mesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall subCall = decalDrawCallChunk.subCalls[i];
				for (int j = subCall.start; j < subCall.end; j++)
				{
					decalCachedChunk.propertyBlock.SetMatrix("_NormalToWorld", decalDrawCallChunk.normalToDecals[j]);
					decalCachedChunk.propertyBlock.SetFloat("_DecalLayerMaskFromDecal", decalDrawCallChunk.renderingLayerMasks[j]);
					Mesh mesh2 = mesh;
					Matrix4x4 matrix4x = decalDrawCallChunk.decalToWorlds[j];
					Material material2 = material;
					int num = decalCachedChunk.layerMasks[j];
					CameraData cameraData2 = cameraData;
					Graphics.DrawMesh(mesh2, matrix4x, material2, num, *cameraData2.camera, 0, decalCachedChunk.propertyBlock);
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000AC04 File Offset: 0x00008E04
		private unsafe void DrawInstanced(in CameraData cameraData, DecalEntityChunk decalEntityChunk, DecalCachedChunk decalCachedChunk, DecalDrawCallChunk decalDrawCallChunk)
		{
			Mesh mesh = this.m_EntityManager.decalProjectorMesh;
			Material material = this.GetMaterial(decalEntityChunk);
			decalCachedChunk.propertyBlock.SetVector("unity_LightData", new Vector4(1f, 1f, 1f, 0f));
			int subCallCount = decalDrawCallChunk.subCallCount;
			for (int i = 0; i < subCallCount; i++)
			{
				DecalSubDrawCall subCall = decalDrawCallChunk.subCalls[i];
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.decalToWorlds.Reinterpret<Matrix4x4>(), subCall.start, this.m_WorldToDecals, 0, subCall.count);
				NativeArray<Matrix4x4>.Copy(decalDrawCallChunk.normalToDecals.Reinterpret<Matrix4x4>(), subCall.start, this.m_NormalToDecals, 0, subCall.count);
				NativeArray<float>.Copy(decalDrawCallChunk.renderingLayerMasks.Reinterpret<float>(), subCall.start, this.m_DecalLayerMasks, 0, subCall.count);
				decalCachedChunk.propertyBlock.SetMatrixArray("_NormalToWorld", this.m_NormalToDecals);
				decalCachedChunk.propertyBlock.SetFloatArray("_DecalLayerMaskFromDecal", this.m_DecalLayerMasks);
				Mesh mesh2 = mesh;
				int num = 0;
				Material material2 = material;
				Matrix4x4[] worldToDecals = this.m_WorldToDecals;
				int count = subCall.count;
				MaterialPropertyBlock propertyBlock = decalCachedChunk.propertyBlock;
				ShadowCastingMode shadowCastingMode = ShadowCastingMode.On;
				bool flag = true;
				int num2 = 0;
				CameraData cameraData2 = cameraData;
				Graphics.DrawMeshInstanced(mesh2, num, material2, worldToDecals, count, propertyBlock, shadowCastingMode, flag, num2, *cameraData2.camera);
			}
		}

		// Token: 0x0400027D RID: 637
		internal static readonly uint MaxBatchSize = 250U;

		// Token: 0x0400027E RID: 638
		protected DecalEntityManager m_EntityManager;

		// Token: 0x0400027F RID: 639
		private Matrix4x4[] m_WorldToDecals;

		// Token: 0x04000280 RID: 640
		private Matrix4x4[] m_NormalToDecals;

		// Token: 0x04000281 RID: 641
		private float[] m_DecalLayerMasks;

		// Token: 0x04000282 RID: 642
		private ProfilingSampler m_Sampler;
	}
}
