using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	internal class ScreenSpaceAmbientOcclusionSettings
	{
		// Token: 0x0400086C RID: 2156
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.AOMethodOptions AOMethod;

		// Token: 0x0400086D RID: 2157
		[SerializeField]
		internal bool Downsample;

		// Token: 0x0400086E RID: 2158
		[SerializeField]
		internal bool AfterOpaque;

		// Token: 0x0400086F RID: 2159
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.DepthSource Source = ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;

		// Token: 0x04000870 RID: 2160
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.NormalQuality NormalSamples = ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium;

		// Token: 0x04000871 RID: 2161
		[SerializeField]
		internal float Intensity = 3f;

		// Token: 0x04000872 RID: 2162
		[SerializeField]
		internal float DirectLightingStrength = 0.25f;

		// Token: 0x04000873 RID: 2163
		[SerializeField]
		internal float Radius = 0.035f;

		// Token: 0x04000874 RID: 2164
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.AOSampleOption Samples = ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;

		// Token: 0x04000875 RID: 2165
		[SerializeField]
		internal ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions BlurQuality;

		// Token: 0x04000876 RID: 2166
		[SerializeField]
		internal float Falloff = 100f;

		// Token: 0x04000877 RID: 2167
		[SerializeField]
		internal int SampleCount = -1;

		// Token: 0x02000172 RID: 370
		internal enum DepthSource
		{
			// Token: 0x04000879 RID: 2169
			Depth,
			// Token: 0x0400087A RID: 2170
			DepthNormals
		}

		// Token: 0x02000173 RID: 371
		internal enum NormalQuality
		{
			// Token: 0x0400087C RID: 2172
			Low,
			// Token: 0x0400087D RID: 2173
			Medium,
			// Token: 0x0400087E RID: 2174
			High
		}

		// Token: 0x02000174 RID: 372
		internal enum AOSampleOption
		{
			// Token: 0x04000880 RID: 2176
			High,
			// Token: 0x04000881 RID: 2177
			Medium,
			// Token: 0x04000882 RID: 2178
			Low
		}

		// Token: 0x02000175 RID: 373
		internal enum AOMethodOptions
		{
			// Token: 0x04000884 RID: 2180
			BlueNoise,
			// Token: 0x04000885 RID: 2181
			InterleavedGradient
		}

		// Token: 0x02000176 RID: 374
		internal enum BlurQualityOptions
		{
			// Token: 0x04000887 RID: 2183
			High,
			// Token: 0x04000888 RID: 2184
			Medium,
			// Token: 0x04000889 RID: 2185
			Low
		}
	}
}
