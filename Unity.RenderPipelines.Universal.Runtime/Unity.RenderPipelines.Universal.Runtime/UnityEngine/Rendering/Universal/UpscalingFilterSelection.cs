using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000026 RID: 38
	public enum UpscalingFilterSelection
	{
		// Token: 0x040000EE RID: 238
		[InspectorName("Automatic")]
		[Tooltip("Unity selects a filtering option automatically based on the Render Scale value and the current screen resolution.")]
		Auto,
		// Token: 0x040000EF RID: 239
		[InspectorName("Bilinear")]
		Linear,
		// Token: 0x040000F0 RID: 240
		[InspectorName("Nearest-Neighbor")]
		Point,
		// Token: 0x040000F1 RID: 241
		[InspectorName("FidelityFX Super Resolution 1.0")]
		[Tooltip("If the target device does not support Unity shader model 4.5, Unity falls back to the Automatic option.")]
		FSR,
		// Token: 0x040000F2 RID: 242
		[InspectorName("Spatial-Temporal Post-Processing")]
		[Tooltip("If the target device does not support compute shaders or is running GLES, Unity falls back to the Automatic option.")]
		STP
	}
}
