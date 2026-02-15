using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000142 RID: 322
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ShaderVariablesProbeVolumes.cs")]
	internal class APVDefinitions
	{
		// Token: 0x040005FD RID: 1533
		public static int probeIndexChunkSize = 243;

		// Token: 0x040005FE RID: 1534
		public const float probeValidityThreshold = 0.05f;

		// Token: 0x040005FF RID: 1535
		public static int probeMaxRegionCount = 4;

		// Token: 0x04000600 RID: 1536
		public static Color32[] layerMaskColors = new Color32[]
		{
			new Color32(230, 159, 0, byte.MaxValue),
			new Color32(0, 158, 115, byte.MaxValue),
			new Color32(0, 114, 178, byte.MaxValue),
			new Color32(204, 121, 167, byte.MaxValue)
		};

		// Token: 0x04000601 RID: 1537
		public static Color debugEmptyColor = new Color(0.388f, 0.812f, 0.804f, 1f);
	}
}
