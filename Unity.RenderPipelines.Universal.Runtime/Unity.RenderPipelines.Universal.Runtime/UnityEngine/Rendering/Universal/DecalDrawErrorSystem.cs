using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007A RID: 122
	internal class DecalDrawErrorSystem : DecalDrawSystem
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00009719 File Offset: 0x00007919
		public DecalDrawErrorSystem(DecalEntityManager entityManager, DecalTechnique technique)
			: base("DecalDrawErrorSystem.Execute", entityManager)
		{
			this.m_Technique = technique;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009730 File Offset: 0x00007930
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			switch (this.m_Technique)
			{
			case DecalTechnique.Invalid:
				return 0;
			case DecalTechnique.DBuffer:
				if (decalCachedChunk.passIndexDBuffer != -1 || decalCachedChunk.passIndexEmissive != -1)
				{
					return -1;
				}
				return 0;
			case DecalTechnique.ScreenSpace:
				if (decalCachedChunk.passIndexScreenSpace != -1)
				{
					return -1;
				}
				return 0;
			case DecalTechnique.GBuffer:
				if (decalCachedChunk.passIndexGBuffer != -1)
				{
					return -1;
				}
				return 0;
			default:
				return 0;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000978F File Offset: 0x0000798F
		protected override Material GetMaterial(DecalEntityChunk decalEntityChunk)
		{
			return this.m_EntityManager.errorMaterial;
		}

		// Token: 0x0400022E RID: 558
		private DecalTechnique m_Technique;
	}
}
