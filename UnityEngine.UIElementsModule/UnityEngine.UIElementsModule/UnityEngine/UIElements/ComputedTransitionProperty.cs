using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C6 RID: 710
	internal struct ComputedTransitionProperty
	{
		// Token: 0x04000B0D RID: 2829
		public StylePropertyId id;

		// Token: 0x04000B0E RID: 2830
		public int durationMs;

		// Token: 0x04000B0F RID: 2831
		public int delayMs;

		// Token: 0x04000B10 RID: 2832
		public Func<float, float> easingCurve;
	}
}
