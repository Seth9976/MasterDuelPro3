using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000EF RID: 239
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Probe Adjustment Volume")]
	public class ProbeAdjustmentVolume : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00012920 File Offset: 0x00010B20
		private void Awake()
		{
			if (this.version == ProbeAdjustmentVolume.Version.Count)
			{
				return;
			}
			if (this.version == ProbeAdjustmentVolume.Version.Initial)
			{
				if (this.invalidateProbes)
				{
					this.mode = ProbeAdjustmentVolume.Mode.InvalidateProbes;
				}
				else if (this.overrideDilationThreshold)
				{
					this.mode = ProbeAdjustmentVolume.Mode.OverrideValidityThreshold;
				}
				this.version++;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001296D File Offset: 0x00010B6D
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (this.version == ProbeAdjustmentVolume.Version.Count)
			{
				this.version = ProbeAdjustmentVolume.Version.Mode;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001297F File Offset: 0x00010B7F
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.version == ProbeAdjustmentVolume.Version.Count)
			{
				this.version = ProbeAdjustmentVolume.Version.Initial;
			}
		}

		// Token: 0x040002E9 RID: 745
		[Tooltip("Select the shape used for this Probe Adjustment Volume.")]
		public ProbeAdjustmentVolume.Shape shape;

		// Token: 0x040002EA RID: 746
		[Min(0f)]
		[Tooltip("Modify the size of this Probe Adjustment Volume. This is unaffected by the GameObject's Transform's Scale property.")]
		public Vector3 size = new Vector3(1f, 1f, 1f);

		// Token: 0x040002EB RID: 747
		[Min(0f)]
		[Tooltip("Modify the radius of this Probe Adjustment Volume. This is unaffected by the GameObject's Transform's Scale property.")]
		public float radius = 1f;

		// Token: 0x040002EC RID: 748
		public ProbeAdjustmentVolume.Mode mode;

		// Token: 0x040002ED RID: 749
		[Range(0.0001f, 2f)]
		[Tooltip("A multiplier applied to the intensity of probes covered by this Probe Adjustment Volume.")]
		public float intensityScale = 1f;

		// Token: 0x040002EE RID: 750
		[Range(0f, 0.95f)]
		public float overriddenDilationThreshold = 0.75f;

		// Token: 0x040002EF RID: 751
		public Vector3 virtualOffsetRotation = Vector3.zero;

		// Token: 0x040002F0 RID: 752
		[Min(0f)]
		public float virtualOffsetDistance = 1f;

		// Token: 0x040002F1 RID: 753
		[Range(0f, 1f)]
		[Tooltip("Determines how far Unity pushes a probe out of geometry after a ray hit.")]
		public float geometryBias = 0.01f;

		// Token: 0x040002F2 RID: 754
		[Range(0f, 0.95f)]
		public float virtualOffsetThreshold = 0.75f;

		// Token: 0x040002F3 RID: 755
		[Range(-0.05f, 0f)]
		[Tooltip("Distance from the probe position used to determine the origin of the sampling ray.")]
		public float rayOriginBias = -0.001f;

		// Token: 0x040002F4 RID: 756
		[Tooltip("The direction for sampling the ambient probe in worldspace when using the Sky Visibility feature.")]
		public Vector3 skyDirection = Vector3.zero;

		// Token: 0x040002F5 RID: 757
		internal Vector3 skyShadingDirectionRotation = Vector3.zero;

		// Token: 0x040002F6 RID: 758
		[Logarithmic(1, 1024)]
		[Tooltip("Number of samples for direct lighting computations.")]
		public int directSampleCount = 32;

		// Token: 0x040002F7 RID: 759
		[Logarithmic(1, 8192)]
		[Tooltip("Number of samples for indirect lighting computations. This includes environment samples.")]
		public int indirectSampleCount = 512;

		// Token: 0x040002F8 RID: 760
		[Min(0f)]
		[Tooltip("Multiplier for the number of samples specified above.")]
		public int sampleCountMultiplier = 4;

		// Token: 0x040002F9 RID: 761
		[Min(0f)]
		[Tooltip("Maximum number of bounces for indirect lighting.")]
		public int maxBounces = 2;

		// Token: 0x040002FA RID: 762
		[Logarithmic(1, 8192)]
		public int skyOcclusionSampleCount = 2048;

		// Token: 0x040002FB RID: 763
		[Range(0f, 5f)]
		public int skyOcclusionMaxBounces = 2;

		// Token: 0x040002FC RID: 764
		public ProbeAdjustmentVolume.RenderingLayerMaskOperation renderingLayerMaskOperation;

		// Token: 0x040002FD RID: 765
		public byte renderingLayerMask;

		// Token: 0x040002FE RID: 766
		[SerializeField]
		private ProbeAdjustmentVolume.Version version = ProbeAdjustmentVolume.Version.Count;

		// Token: 0x040002FF RID: 767
		[Obsolete("Use mode")]
		public bool invalidateProbes;

		// Token: 0x04000300 RID: 768
		[Obsolete("Use mode")]
		public bool overrideDilationThreshold;

		// Token: 0x020000F0 RID: 240
		public enum Shape
		{
			// Token: 0x04000302 RID: 770
			Box,
			// Token: 0x04000303 RID: 771
			Sphere
		}

		// Token: 0x020000F1 RID: 241
		public enum Mode
		{
			// Token: 0x04000305 RID: 773
			InvalidateProbes,
			// Token: 0x04000306 RID: 774
			OverrideValidityThreshold,
			// Token: 0x04000307 RID: 775
			ApplyVirtualOffset,
			// Token: 0x04000308 RID: 776
			OverrideVirtualOffsetSettings,
			// Token: 0x04000309 RID: 777
			OverrideSkyDirection,
			// Token: 0x0400030A RID: 778
			OverrideSampleCount,
			// Token: 0x0400030B RID: 779
			OverrideRenderingLayerMask,
			// Token: 0x0400030C RID: 780
			IntensityScale = 99
		}

		// Token: 0x020000F2 RID: 242
		public enum RenderingLayerMaskOperation
		{
			// Token: 0x0400030E RID: 782
			Override,
			// Token: 0x0400030F RID: 783
			Add,
			// Token: 0x04000310 RID: 784
			Remove
		}

		// Token: 0x020000F3 RID: 243
		private enum Version
		{
			// Token: 0x04000312 RID: 786
			Initial,
			// Token: 0x04000313 RID: 787
			Mode,
			// Token: 0x04000314 RID: 788
			Count
		}
	}
}
