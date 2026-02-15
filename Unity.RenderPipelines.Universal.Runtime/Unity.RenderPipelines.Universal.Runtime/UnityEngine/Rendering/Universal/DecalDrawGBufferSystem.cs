using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000096 RID: 150
	internal class DecalDrawGBufferSystem : DecalDrawSystem
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		public DecalDrawGBufferSystem(DecalEntityManager entityManager)
			: base("DecalDrawGBufferSystem.Execute", entityManager)
		{
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000CACF File Offset: 0x0000ACCF
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexGBuffer;
		}
	}
}
