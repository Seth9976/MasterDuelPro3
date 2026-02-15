using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000141 RID: 321
	[VolumeComponentMenu("Lighting/Adaptive Probe Volumes Options")]
	[SupportedOnRenderPipeline(new Type[] { })]
	[Serializable]
	public sealed class ProbeVolumesOptions : VolumeComponent
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00020D58 File Offset: 0x0001EF58
		private ProbeVolumesOptions()
		{
			base.displayName = "Adaptive Probe Volumes Options";
		}

		// Token: 0x040005F2 RID: 1522
		[Tooltip("The overridden normal bias to be applied to the world position when sampling the Adaptive Probe Volumes data structure. Unit is meters.")]
		public ClampedFloatParameter normalBias = new ClampedFloatParameter(0.05f, 0f, 2f, false);

		// Token: 0x040005F3 RID: 1523
		[Tooltip("A bias alongside the view vector to be applied to the world position when sampling the Adaptive Probe Volumes data structure. Unit is meters.")]
		public ClampedFloatParameter viewBias = new ClampedFloatParameter(0.1f, 0f, 2f, false);

		// Token: 0x040005F4 RID: 1524
		[Tooltip("Whether to scale the bias for Adaptive Probe Volumes by the minimum distance between probes.")]
		public BoolParameter scaleBiasWithMinProbeDistance = new BoolParameter(false, false);

		// Token: 0x040005F5 RID: 1525
		[Tooltip("Noise to be applied to the sampling position. It can hide seams issues between subdivision levels, but introduces noise.")]
		public ClampedFloatParameter samplingNoise = new ClampedFloatParameter(0.1f, 0f, 1f, false);

		// Token: 0x040005F6 RID: 1526
		[Tooltip("Whether to animate the noise when TAA is enabled. It can potentially remove the visible noise patterns.")]
		public BoolParameter animateSamplingNoise = new BoolParameter(true, false);

		// Token: 0x040005F7 RID: 1527
		[Tooltip("Method used to reduce leaks. Currently available modes are crude, but cheap methods.")]
		public APVLeakReductionModeParameter leakReductionMode = new APVLeakReductionModeParameter(APVLeakReductionMode.Quality, false);

		// Token: 0x040005F8 RID: 1528
		[Obsolete("This parameter isn't used anymore.")]
		public ClampedFloatParameter minValidDotProductValue = new ClampedFloatParameter(0.1f, -1f, 0.33f, false);

		// Token: 0x040005F9 RID: 1529
		[Tooltip("When enabled, reflection probe normalization can only decrease the reflection intensity.")]
		public BoolParameter occlusionOnlyReflectionNormalization = new BoolParameter(true, false);

		// Token: 0x040005FA RID: 1530
		[AdditionalProperty]
		[Tooltip("Global probe volumes weight. Allows for fading out probe volumes influence falling back to ambient probe.")]
		public ClampedFloatParameter intensityMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040005FB RID: 1531
		[AdditionalProperty]
		[Tooltip("Multiplier applied on the sky lighting when using sky occlusion.")]
		public ClampedFloatParameter skyOcclusionIntensityMultiplier = new ClampedFloatParameter(1f, 0f, 5f, false);

		// Token: 0x040005FC RID: 1532
		[AdditionalProperty]
		[Tooltip("Offset applied at runtime to probe positions in world space.\nThis is not considered while baking.")]
		public Vector3Parameter worldOffset = new Vector3Parameter(Vector3.zero, false);
	}
}
