using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000156 RID: 342
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/PostProcessing/LensFlareDataSRP.cs")]
	[Serializable]
	public enum SRPLensFlareColorType
	{
		// Token: 0x04000682 RID: 1666
		Constant,
		// Token: 0x04000683 RID: 1667
		RadialGradient,
		// Token: 0x04000684 RID: 1668
		AngularGradient
	}
}
