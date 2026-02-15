using System;

namespace UnityEngine
{
	// Token: 0x02000165 RID: 357
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x00020224 File Offset: 0x0001E424
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x04000600 RID: 1536
		public readonly bool showAlpha = true;

		// Token: 0x04000601 RID: 1537
		public readonly bool hdr = false;

		// Token: 0x04000602 RID: 1538
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minBrightness = 0f;

		// Token: 0x04000603 RID: 1539
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxBrightness = 8f;

		// Token: 0x04000604 RID: 1540
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minExposureValue = 0.125f;

		// Token: 0x04000605 RID: 1541
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxExposureValue = 3f;
	}
}
