using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000037 RID: 55
	internal struct PerLight2D
	{
		// Token: 0x04000112 RID: 274
		internal float4x4 InvMatrix;

		// Token: 0x04000113 RID: 275
		internal float4 Color;

		// Token: 0x04000114 RID: 276
		internal float4 Position;

		// Token: 0x04000115 RID: 277
		internal float FalloffIntensity;

		// Token: 0x04000116 RID: 278
		internal float FalloffDistance;

		// Token: 0x04000117 RID: 279
		internal float OuterAngle;

		// Token: 0x04000118 RID: 280
		internal float InnerAngle;

		// Token: 0x04000119 RID: 281
		internal float InnerRadiusMult;

		// Token: 0x0400011A RID: 282
		internal float VolumeOpacity;

		// Token: 0x0400011B RID: 283
		internal float ShadowIntensity;

		// Token: 0x0400011C RID: 284
		internal int LightType;
	}
}
