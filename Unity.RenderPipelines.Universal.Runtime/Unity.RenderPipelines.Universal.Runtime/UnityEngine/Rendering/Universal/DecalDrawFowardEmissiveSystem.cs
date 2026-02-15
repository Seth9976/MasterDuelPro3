using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000076 RID: 118
	internal class DecalDrawFowardEmissiveSystem : DecalDrawSystem
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000946B File Offset: 0x0000766B
		public DecalDrawFowardEmissiveSystem(DecalEntityManager entityManager)
			: base("DecalDrawFowardEmissiveSystem.Execute", entityManager)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009479 File Offset: 0x00007679
		protected override int GetPassIndex(DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexEmissive;
		}
	}
}
