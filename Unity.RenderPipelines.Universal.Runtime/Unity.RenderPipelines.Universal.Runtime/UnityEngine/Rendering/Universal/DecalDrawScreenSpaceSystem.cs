using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009A RID: 154
	internal class DecalDrawScreenSpaceSystem : DecalDrawSystem
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000D081 File Offset: 0x0000B281
		public DecalDrawScreenSpaceSystem(DecalEntityManager entityManager)
			: base("DecalDrawScreenSpaceSystem.Execute", entityManager)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D08F File Offset: 0x0000B28F
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexScreenSpace;
		}
	}
}
