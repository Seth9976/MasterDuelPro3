using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000072 RID: 114
	internal class DecalDrawDBufferSystem : DecalDrawSystem
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00008A34 File Offset: 0x00006C34
		public DecalDrawDBufferSystem(DecalEntityManager entityManager)
			: base("DecalDrawIntoDBufferSystem.Execute", entityManager)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008A42 File Offset: 0x00006C42
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexDBuffer;
		}
	}
}
