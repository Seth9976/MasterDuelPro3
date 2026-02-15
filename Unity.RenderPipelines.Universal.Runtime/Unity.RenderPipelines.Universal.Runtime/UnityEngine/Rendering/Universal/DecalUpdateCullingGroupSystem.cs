using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000095 RID: 149
	internal class DecalUpdateCullingGroupSystem
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000C944 File Offset: 0x0000AB44
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000C94E File Offset: 0x0000AB4E
		public float boundingDistance
		{
			get
			{
				return this.m_BoundingDistance[0];
			}
			set
			{
				this.m_BoundingDistance[0] = value;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C959 File Offset: 0x0000AB59
		public DecalUpdateCullingGroupSystem(DecalEntityManager entityManager, float drawDistance)
		{
			this.m_EntityManager = entityManager;
			this.m_BoundingDistance[0] = drawDistance;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCullingGroupsSystem.Execute");
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C990 File Offset: 0x0000AB90
		public void Execute(Camera camera)
		{
			using (new ProfilingScope(this.m_Sampler))
			{
				this.m_Camera = camera;
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.cachedChunks[i], this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public void Execute(DecalCachedChunk cachedChunk, DecalCulledChunk culledChunk, int count)
		{
			cachedChunk.currentJobHandle.Complete();
			CullingGroup cullingGroups = culledChunk.cullingGroups;
			cullingGroups.targetCamera = this.m_Camera;
			cullingGroups.SetDistanceReferencePoint(this.m_Camera.transform.position);
			cullingGroups.SetBoundingDistances(this.m_BoundingDistance);
			cachedChunk.boundingSpheres.CopyTo(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSpheres(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSphereCount(count);
			culledChunk.cameraPosition = this.m_Camera.transform.position;
			culledChunk.cullingMask = this.m_Camera.cullingMask;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		internal static ulong GetSceneCullingMaskFromCamera(Camera camera)
		{
			return 0UL;
		}

		// Token: 0x040002D7 RID: 727
		private float[] m_BoundingDistance = new float[1];

		// Token: 0x040002D8 RID: 728
		private Camera m_Camera;

		// Token: 0x040002D9 RID: 729
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002DA RID: 730
		private ProfilingSampler m_Sampler;
	}
}
