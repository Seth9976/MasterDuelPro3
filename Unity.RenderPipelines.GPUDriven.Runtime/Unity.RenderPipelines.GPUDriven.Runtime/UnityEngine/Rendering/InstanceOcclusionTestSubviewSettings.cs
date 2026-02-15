using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009C RID: 156
	internal struct InstanceOcclusionTestSubviewSettings
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00010D4C File Offset: 0x0000EF4C
		public unsafe static InstanceOcclusionTestSubviewSettings FromSpan(ReadOnlySpan<SubviewOcclusionTest> subviewOcclusionTests)
		{
			InstanceOcclusionTestSubviewSettings settings = default(InstanceOcclusionTestSubviewSettings);
			for (int testIndex = 0; testIndex < subviewOcclusionTests.Length; testIndex++)
			{
				SubviewOcclusionTest subviewTest = *subviewOcclusionTests[testIndex];
				settings.occluderSubviewIndices |= subviewTest.occluderSubviewIndex << 4 * testIndex;
				settings.occluderSubviewMask |= 1 << subviewTest.occluderSubviewIndex;
				settings.cullingSplitIndices |= subviewTest.cullingSplitIndex << 4 * testIndex;
				settings.cullingSplitMask |= 1 << subviewTest.cullingSplitIndex;
			}
			settings.testCount = subviewOcclusionTests.Length;
			return settings;
		}

		// Token: 0x0400032D RID: 813
		public int testCount;

		// Token: 0x0400032E RID: 814
		public int occluderSubviewIndices;

		// Token: 0x0400032F RID: 815
		public int occluderSubviewMask;

		// Token: 0x04000330 RID: 816
		public int cullingSplitIndices;

		// Token: 0x04000331 RID: 817
		public int cullingSplitMask;
	}
}
