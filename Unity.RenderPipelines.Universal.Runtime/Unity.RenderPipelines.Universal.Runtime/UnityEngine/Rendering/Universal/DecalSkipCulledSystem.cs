using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200008F RID: 143
	internal class DecalSkipCulledSystem
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000BE99 File Offset: 0x0000A099
		public DecalSkipCulledSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalSkipCulledSystem.Execute");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public void Execute(Camera camera)
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				this.m_Camera = camera;
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BF3C File Offset: 0x0000A13C
		private void Execute(DecalCulledChunk culledChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			culledChunk.currentJobHandle.Complete();
			for (int i = 0; i < count; i++)
			{
				culledChunk.visibleDecalIndices[i] = i;
			}
			culledChunk.visibleDecalCount = count;
			culledChunk.cameraPosition = this.m_Camera.transform.position;
			culledChunk.cullingMask = this.m_Camera.cullingMask;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		internal static ulong GetSceneCullingMaskFromCamera(Camera camera)
		{
			return 0UL;
		}

		// Token: 0x040002A5 RID: 677
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002A6 RID: 678
		private ProfilingSampler m_Sampler;

		// Token: 0x040002A7 RID: 679
		private Camera m_Camera;
	}
}
