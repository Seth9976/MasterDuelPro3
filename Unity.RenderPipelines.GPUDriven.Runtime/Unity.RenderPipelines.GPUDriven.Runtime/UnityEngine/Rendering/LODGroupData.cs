using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A0 RID: 160
	internal struct LODGroupData
	{
		// Token: 0x0400034D RID: 845
		public const int k_MaxLODLevelsCount = 8;

		// Token: 0x0400034E RID: 846
		public bool valid;

		// Token: 0x0400034F RID: 847
		public int lodCount;

		// Token: 0x04000350 RID: 848
		public int rendererCount;

		// Token: 0x04000351 RID: 849
		[FixedBuffer(typeof(float), 8)]
		public LODGroupData.<screenRelativeTransitionHeights>e__FixedBuffer screenRelativeTransitionHeights;

		// Token: 0x04000352 RID: 850
		[FixedBuffer(typeof(float), 8)]
		public LODGroupData.<fadeTransitionWidth>e__FixedBuffer fadeTransitionWidth;

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <fadeTransitionWidth>e__FixedBuffer
		{
			// Token: 0x04000353 RID: 851
			public float FixedElementField;
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <screenRelativeTransitionHeights>e__FixedBuffer
		{
			// Token: 0x04000354 RID: 852
			public float FixedElementField;
		}
	}
}
