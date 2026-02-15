using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000093 RID: 147
	internal class DecalUpdateCulledSystem
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		public DecalUpdateCulledSystem(DecalEntityManager entityManager)
		{
			this.m_EntityManager = entityManager;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCulledSystem.Execute");
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		public void Execute()
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C844 File Offset: 0x0000AA44
		private void Execute(DecalCulledChunk culledChunk, int count)
		{
			if (count == 0)
			{
				return;
			}
			culledChunk.currentJobHandle.Complete();
			CullingGroup cullingGroup = culledChunk.cullingGroups;
			culledChunk.visibleDecalCount = cullingGroup.QueryIndices(true, culledChunk.visibleDecalIndexArray, 0);
			culledChunk.visibleDecalIndices.CopyFrom(culledChunk.visibleDecalIndexArray);
		}

		// Token: 0x040002CE RID: 718
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002CF RID: 719
		private ProfilingSampler m_Sampler;
	}
}
