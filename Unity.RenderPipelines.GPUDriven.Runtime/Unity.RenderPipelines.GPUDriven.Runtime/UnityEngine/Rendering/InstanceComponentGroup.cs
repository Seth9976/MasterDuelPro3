using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CB RID: 203
	[Flags]
	internal enum InstanceComponentGroup : uint
	{
		// Token: 0x04000407 RID: 1031
		Default = 1U,
		// Token: 0x04000408 RID: 1032
		Wind = 2U,
		// Token: 0x04000409 RID: 1033
		LightProbe = 4U,
		// Token: 0x0400040A RID: 1034
		Lightmap = 8U,
		// Token: 0x0400040B RID: 1035
		DefaultWind = 3U,
		// Token: 0x0400040C RID: 1036
		DefaultLightProbe = 5U,
		// Token: 0x0400040D RID: 1037
		DefaultLightmap = 9U,
		// Token: 0x0400040E RID: 1038
		DefaultWindLightProbe = 7U,
		// Token: 0x0400040F RID: 1039
		DefaultWindLightmap = 11U
	}
}
