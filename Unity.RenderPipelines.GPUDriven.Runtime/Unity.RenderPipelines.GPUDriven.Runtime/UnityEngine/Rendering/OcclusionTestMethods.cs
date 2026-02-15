using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002B RID: 43
	public static class OcclusionTestMethods
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000591D File Offset: 0x00003B1D
		public static uint GetBatchLayerMask(this OcclusionTest occlusionTest)
		{
			if (occlusionTest != OcclusionTest.TestCulled)
			{
				return uint.MaxValue;
			}
			return 268435456U;
		}
	}
}
