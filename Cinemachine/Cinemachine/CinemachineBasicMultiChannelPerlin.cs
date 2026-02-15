using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000048 RID: 72
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineBasicMultiChannelPerlin : CinemachineComponentBase
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000C78A File Offset: 0x0000A98A
		public override bool IsValid
		{
			get
			{
				return base.enabled && this.m_NoiseProfile != null;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000C7A2 File Offset: 0x0000A9A2
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Noise;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (!this.IsValid || deltaTime < 0f)
			{
				this.mInitialized = false;
				return;
			}
			if (!this.mInitialized)
			{
				this.Initialize();
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Playback && TargetPositionCache.HasCurrentTime)
			{
				this.mNoiseTime = TargetPositionCache.CurrentTime * this.m_FrequencyGain;
			}
			else
			{
				this.mNoiseTime += deltaTime * this.m_FrequencyGain;
			}
			curState.PositionCorrection += curState.CorrectedOrientation * NoiseSettings.GetCombinedFilterResults(this.m_NoiseProfile.PositionNoise, this.mNoiseTime, this.mNoiseOffsets) * this.m_AmplitudeGain;
			Quaternion rotNoise = Quaternion.Euler(NoiseSettings.GetCombinedFilterResults(this.m_NoiseProfile.OrientationNoise, this.mNoiseTime, this.mNoiseOffsets) * this.m_AmplitudeGain);
			if (this.m_PivotOffset != Vector3.zero)
			{
				Matrix4x4 i = Matrix4x4.Translate(-this.m_PivotOffset);
				i = Matrix4x4.Rotate(rotNoise) * i;
				i = Matrix4x4.Translate(this.m_PivotOffset) * i;
				curState.PositionCorrection += curState.CorrectedOrientation * i.MultiplyPoint(Vector3.zero);
			}
			curState.OrientationCorrection *= rotNoise;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C909 File Offset: 0x0000AB09
		public void ReSeed()
		{
			this.mNoiseOffsets = new Vector3(global::UnityEngine.Random.Range(-1000f, 1000f), global::UnityEngine.Random.Range(-1000f, 1000f), global::UnityEngine.Random.Range(-1000f, 1000f));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C943 File Offset: 0x0000AB43
		private void Initialize()
		{
			this.mInitialized = true;
			this.mNoiseTime = CinemachineCore.CurrentTime * this.m_FrequencyGain;
			if (this.mNoiseOffsets == Vector3.zero)
			{
				this.ReSeed();
			}
		}

		// Token: 0x04000170 RID: 368
		[Tooltip("The asset containing the Noise Profile.  Define the frequencies and amplitudes there to make a characteristic noise profile.  Make your own or just use one of the many presets.")]
		[FormerlySerializedAs("m_Definition")]
		[NoiseSettingsProperty]
		public NoiseSettings m_NoiseProfile;

		// Token: 0x04000171 RID: 369
		[Tooltip("When rotating the camera, offset the camera's pivot position by this much (camera space)")]
		public Vector3 m_PivotOffset = Vector3.zero;

		// Token: 0x04000172 RID: 370
		[Tooltip("Gain to apply to the amplitudes defined in the NoiseSettings asset.  1 is normal.  Setting this to 0 completely mutes the noise.")]
		public float m_AmplitudeGain = 1f;

		// Token: 0x04000173 RID: 371
		[Tooltip("Scale factor to apply to the frequencies defined in the NoiseSettings asset.  1 is normal.  Larger magnitudes will make the noise shake more rapidly.")]
		public float m_FrequencyGain = 1f;

		// Token: 0x04000174 RID: 372
		private bool mInitialized;

		// Token: 0x04000175 RID: 373
		private float mNoiseTime;

		// Token: 0x04000176 RID: 374
		[SerializeField]
		[HideInInspector]
		private Vector3 mNoiseOffsets = Vector3.zero;
	}
}
