using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000099 RID: 153
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineNoiseProfiles.html")]
	public sealed class NoiseSettings : SignalSourceAsset
	{
		// Token: 0x060003AB RID: 939 RVA: 0x000158E8 File Offset: 0x00013AE8
		public static Vector3 GetCombinedFilterResults(NoiseSettings.TransformNoiseParams[] noiseParams, float time, Vector3 timeOffsets)
		{
			Vector3 pos = Vector3.zero;
			if (noiseParams != null)
			{
				for (int i = 0; i < noiseParams.Length; i++)
				{
					pos += noiseParams[i].GetValueAt(time, timeOffsets);
				}
			}
			return pos;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public override float SignalDuration
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015922 File Offset: 0x00013B22
		public override void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot)
		{
			pos = NoiseSettings.GetCombinedFilterResults(this.PositionNoise, timeSinceSignalStart, Vector3.zero);
			rot = Quaternion.Euler(NoiseSettings.GetCombinedFilterResults(this.OrientationNoise, timeSinceSignalStart, Vector3.zero));
		}

		// Token: 0x04000344 RID: 836
		[Tooltip("These are the noise channels for the virtual camera's position. Convincing noise setups typically mix low, medium and high frequencies together, so start with a size of 3")]
		[FormerlySerializedAs("m_Position")]
		public NoiseSettings.TransformNoiseParams[] PositionNoise = Array.Empty<NoiseSettings.TransformNoiseParams>();

		// Token: 0x04000345 RID: 837
		[Tooltip("These are the noise channels for the virtual camera's orientation. Convincing noise setups typically mix low, medium and high frequencies together, so start with a size of 3")]
		[FormerlySerializedAs("m_Orientation")]
		public NoiseSettings.TransformNoiseParams[] OrientationNoise = Array.Empty<NoiseSettings.TransformNoiseParams>();

		// Token: 0x0200009A RID: 154
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct NoiseParams
		{
			// Token: 0x060003AF RID: 943 RVA: 0x00015978 File Offset: 0x00013B78
			public float GetValueAt(float time, float timeOffset)
			{
				float t = this.Frequency * time + timeOffset;
				if (this.Constant)
				{
					return Mathf.Cos(t * 2f * 3.1415927f) * this.Amplitude * 0.5f;
				}
				return (Mathf.PerlinNoise(t, 0f) - 0.5f) * this.Amplitude;
			}

			// Token: 0x04000346 RID: 838
			[Tooltip("The frequency of noise for this channel.  Higher magnitudes vibrate faster.")]
			public float Frequency;

			// Token: 0x04000347 RID: 839
			[Tooltip("The amplitude of the noise for this channel.  Larger numbers vibrate higher.")]
			public float Amplitude;

			// Token: 0x04000348 RID: 840
			[Tooltip("If checked, then the amplitude and frequency will not be randomized.")]
			public bool Constant;
		}

		// Token: 0x0200009B RID: 155
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct TransformNoiseParams
		{
			// Token: 0x060003B0 RID: 944 RVA: 0x000159D0 File Offset: 0x00013BD0
			public Vector3 GetValueAt(float time, Vector3 timeOffsets)
			{
				return new Vector3(this.X.GetValueAt(time, timeOffsets.x), this.Y.GetValueAt(time, timeOffsets.y), this.Z.GetValueAt(time, timeOffsets.z));
			}

			// Token: 0x04000349 RID: 841
			[Tooltip("Noise definition for X-axis")]
			public NoiseSettings.NoiseParams X;

			// Token: 0x0400034A RID: 842
			[Tooltip("Noise definition for Y-axis")]
			public NoiseSettings.NoiseParams Y;

			// Token: 0x0400034B RID: 843
			[Tooltip("Noise definition for Z-axis")]
			public NoiseSettings.NoiseParams Z;
		}
	}
}
